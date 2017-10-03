using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Face;
using moleQule.Face.Common;

namespace moleQule.Face.Invoice
{
    public partial class CashUIForm : CashForm
    {
        #region Attributes & Properties

        public new const string ID = "CashUIForm";
		public new static Type Type { get { return typeof(CashUIForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected Cash _entity;

        public override Cash Entity { get { return _entity; } set { _entity = value; } }
        public override CashInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

		protected bool _linea_libre;

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected CashUIForm() 
			: this(-1) { }

        public CashUIForm(long oid) 
			: this(oid, true, null) { }

        public CashUIForm(long oid, bool ismodal, Form parent)
            : base(oid, ismodal, parent)
        {
            InitializeComponent();

			_linea_libre = Library.Invoice.ModulePrincipal.GetLineaCajaLibreSetting();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            Cash temp = _entity.Clone();
            temp.ApplyEdit();

            // do the save
            try
            {
                _entity = temp.Save();
                _entity.ApplyEdit();					

				return true;
            }
            catch (Exception ex)
            {
				PgMng.FillUp();
                PgMng.ShowInfoException(ex);
                return false;
            }
            finally
            {
                this.Datos.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Layout

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();
            
			Lines_BS.DataSource = _entity.Lines;
            PgMng.Grow();

			base.RefreshMainData();
        }
		
        #endregion

		#region Business Methods

		protected void SetTipo()
		{
            CashLine item = Lines_DGW.CurrentRow.DataBoundItem as CashLine;

			SelectEnumInputForm form = new SelectEnumInputForm(true);

			if (_linea_libre)
			{
				ETipoLineaCaja[] list = { ETipoLineaCaja.SalidaPorIngreso, ETipoLineaCaja.EntradaPorTraspaso, ETipoLineaCaja.EntradaPorTarjetaCredito, ETipoLineaCaja.Otros };
				form.SetDataSource(Library.Invoice.EnumText<ETipoLineaCaja>.GetList(list));
			}
			else
			{
                ETipoLineaCaja[] list = { ETipoLineaCaja.SalidaPorIngreso, ETipoLineaCaja.EntradaPorTraspaso, ETipoLineaCaja.EntradaPorTarjetaCredito };
				form.SetDataSource(Library.Invoice.EnumText<ETipoLineaCaja>.GetList(list));
			}

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource tipo = form.Selected as ComboBoxSource;
				item.ETipoLinea = (ETipoLineaCaja)tipo.Oid;

				SetRowFormat(Lines_DGW.CurrentRow);

                Lines_BS.ResetBindings(false);
			}
		}

		#endregion

		#region Actions

		protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		protected override void AddLineaAction()
		{
            CashLine item = _entity.Lines.NewItem(_entity);
			Lines_BS.DataSource = _entity.Lines;

			Lines_BS.MoveLast();

			if (!_linea_libre)
				item.ETipoLinea = ETipoLineaCaja.SalidaPorIngreso;

			SetTipo();
		}

		protected override void AnulaLineaAction()
		{
            if (Lines_DGW.CurrentRow == null) return;

            CashLine item = Lines_DGW.CurrentRow.DataBoundItem as CashLine;

            if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.NULL_CONFIRM) != DialogResult.Yes)
            {
                return;
            }

			if (item.Locked) return;

			_entity.Lines.Remove(item);

			_entity.UpdateSaldo();
			Datos.ResetBindings(false);

			SetGridFormat(Lines_DGW);
		}

		protected override void SetLineTypeAction()
		{
            CashLine item = Lines_DGW.CurrentRow.DataBoundItem as CashLine;

			if (item.Locked) return;

            if (new ETipoLineaCaja[] 
                    { 
                        ETipoLineaCaja.EntradaPorCobro,
                        ETipoLineaCaja.SalidaPorIngreso,
                        ETipoLineaCaja.EntradaPorTarjetaCredito
                    }.Contains(item.ETipoLinea))
                return;

			SetTipo();
		}

		protected override void SetBankAccountAction()
		{
            if (!ControlsMng.IsCurrentItemValid(Lines_DGW)) return;

            CashLine item = ControlsMng.GetCurrentItem(Lines_DGW) as CashLine;

			if (item.Locked) return;

            if (!new ETipoLineaCaja[] 
                    { 
                        ETipoLineaCaja.EntradaPorTraspaso,
                        ETipoLineaCaja.SalidaPorIngreso
                    }.Contains(item.ETipoLinea))
                return;
			
			BankAccountSelectForm form = new BankAccountSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				BankAccountInfo cuenta = form.Selected as BankAccountInfo;

				item.OidCuentaBancaria = cuenta.Oid;
				item.CuentaBancaria = cuenta.Valor;
			}
		}

        protected override void SetCreditCardAction()
        {
            if (!ControlsMng.IsCurrentItemValid(Lines_DGW)) return;

            CashLine item = ControlsMng.GetCurrentItem(Lines_DGW) as CashLine;

            if (item.Locked) return;

            if (!new ETipoLineaCaja[] 
                    { 
                        ETipoLineaCaja.EntradaPorTarjetaCredito
                    }.Contains(item.ETipoLinea))
                return;

            CreditCardSelectForm form = new CreditCardSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                CreditCardInfo credit_card = form.Selected as CreditCardInfo;
                if (credit_card.ETipoTarjeta != ETipoTarjeta.Credito) return;

                item.OidCreditCard = credit_card.Oid;
                item.CreditCard = credit_card.Nombre;
                item.OidCuentaBancaria = credit_card.OidCuentaBancaria;
                item.CuentaBancaria = credit_card.CuentaBancaria;

                Lines_BS.ResetBindings(false);
            }
        }

		protected override void RefreshAction()
		{
			_entity.UpdateSaldo();
			Datos.ResetBindings(false);
			Lines_BS.DataSource = _entity.Lines;
			Lines_DGW.Refresh();
		}

        public override void PrintObject()
        {
            FormMng.Instance.OpenForm(CashActionForm.ID, new object[1] { _entity });
        }

        #endregion
    }
}