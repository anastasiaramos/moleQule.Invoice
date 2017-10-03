using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Face;
using moleQule.Face.Common;

namespace moleQule.Face.Invoice
{
    public partial class BankLineUIForm : BankLineForm
    {
        #region Attributes & Properties

        public new const string ID = "BankLineUIForm";
		public new static Type Type { get { return typeof(BankLineUIForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected BankLine _entity;

        public override BankLine Entity { get { return _entity; } set { _entity = value; } }
        public override BankLineInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected BankLineUIForm() 
			: this(null) {}

        public BankLineUIForm(Form parent) 
			: this(-1, true, parent) { }

        public BankLineUIForm(long oid) 
			: this(oid, true, null) { }

        public BankLineUIForm(long oid, bool isModal, Form parent)
            : this(oid, null, isModal, parent) { }

        public BankLineUIForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
			this.Datos.RaiseListChangedEvents = false;

			BankLine temp = _entity.Clone();
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

        public override void FormatControls()
        {
            Estado_BT.Enabled = (_entity.ETipoMovimientoBanco == EBankLineType.Manual || _entity.ETipoMovimientoBanco == EBankLineType.Interes);
            Importe_TB.ReadOnly = (_entity.ETipoMovimientoBanco != EBankLineType.Manual && _entity.ETipoMovimientoBanco != EBankLineType.Interes);
            Cuenta_BT.Enabled = (_entity.ETipoMovimientoBanco == EBankLineType.Manual || _entity.ETipoMovimientoBanco == EBankLineType.Interes);
            Titular_TB.ReadOnly = (_entity.ETipoMovimientoBanco != EBankLineType.Manual && _entity.ETipoMovimientoBanco != EBankLineType.Interes);
            TipoCuenta_BT.Enabled = this is BankLineAddForm;
            Creacion_DTP.Enabled = false;
            
			Observaciones_TB.ReadOnly = (_entity.EEstado == EEstado.Anulado);
            Auditado_CkB.Enabled = (moleQule.Library.AutorizationRulesControler.CanEditObject(moleQule.Library.Invoice.Resources.SecureItems.AUDITAR_MOVIMIENTOS_BANCARIOS));
            Fecha_DTP.Enabled = (!_entity.Auditado && moleQule.Library.AutorizationRulesControler.CanEditObject(moleQule.Library.Invoice.Resources.SecureItems.AUDITAR_MOVIMIENTOS_BANCARIOS));

			base.FormatControls();
        }

		#endregion

		#region Source

		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            Auditado_CkB.Checked = _entity.Auditado;

            base.RefreshMainData();
        }

		#endregion

		#region Business Methods

		protected void ChangeState(EEstado estado)
		{
			if (_entity.EEstado == EEstado.Anulado)
			{
				PgMng.ShowInfoException(Face.Resources.Messages.ITEM_ANULADO_NO_EDIT);
				return;
			}

			switch (estado)
			{
				case EEstado.Anulado:
					{
						if (_entity.EEstado == EEstado.Contabilizado)
						{
							PgMng.ShowInfoException(Library.Common.Resources.Messages.NULL_CONTABILIZADO_NOT_ALLOWED);
							return;
						}

						if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.NULL_CONFIRM) != DialogResult.Yes)
						{
							return;
						}
					}
					break;
			}

			_entity.EEstado = estado;
		}

		protected void SetCuentaBancaria(BankAccountInfo source)
		{
			if (source == null) return;

			_entity.OidCuentaMov = source.Oid;
			_entity.OidCuenta = source.Oid;
			_entity.Cuenta = source.Valor;
			_entity.Entidad = source.Entidad;
		}

        #endregion

		#region Actions

        protected override void SaveAction()
        {
			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }
		
		protected virtual void SelectCuentaAction()
		{
			ETipoCuenta tipo = _entity.ETipoMovimientoBanco == EBankLineType.Manual ? ETipoCuenta.CuentaCorriente : ETipoCuenta.FondoInversion;
			BankAccountSelectForm form = new BankAccountSelectForm(this, BankAccountList.GetList(tipo, EEstado.Active, false));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				BankAccountInfo cuenta = form.Selected as BankAccountInfo;
				SetCuentaBancaria(cuenta);
			}
		}

		protected virtual void SelectEstadoAction()
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);

			EEstado[] list = { EEstado.Anulado, EEstado.Abierto };
			form.SetDataSource(Library.Common.EnumText<EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;
				ChangeState((EEstado)estado.Oid);
			}
		}

		protected override void SelectTipoCuentaAction()
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);

			EBankLineType[] list = { EBankLineType.Manual, EBankLineType.Interes };
			form.SetDataSource(Library.Invoice.EnumText<EBankLineType>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource tipo = form.Selected as ComboBoxSource;
				_entity.ETipoMovimientoBanco = ((EBankLineType)tipo.Oid);
			}
		}

		#endregion

		#region Buttons

		private void Cuenta_BT_Click(object sender, EventArgs e) { SelectCuentaAction(); }
		private void Estado_BT_Click(object sender, EventArgs e) { SelectEstadoAction(); }

		#endregion

        #region Events

        private void Auditado_CkB_CheckedChanged(object sender, EventArgs e)
        {
            if (_entity.Auditado != Auditado_CkB.Checked)
                _entity.AuditarAction(Auditado_CkB.Checked);
        }

        #endregion
    }
}
