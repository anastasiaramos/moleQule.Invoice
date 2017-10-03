using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class FinancialCashUIForm : FinancialCashForm
    {
        #region Attributes & Properties

        public new const string ID = "EffectUIForm";
        public new static Type Type { get { return typeof(FinancialCashUIForm); } }

        protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected FinancialCash _entity;

        public override FinancialCash Entity { get { return _entity; } set { _entity = value; } }
        public override FinancialCashInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected FinancialCashUIForm()
            : this(null) { }

        public FinancialCashUIForm(Form parent)
            : this(-1, true, parent) { }

        public FinancialCashUIForm(long oid)
            : this(oid, true, null) { }

        public FinancialCashUIForm(long oid, bool isModal, Form parent)
            : base(oid, isModal, parent)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            FinancialCash temp = _entity.Clone();
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
			base.FormatControls();

			ChargeDate_DTP.Enabled = _entity.Adelantado;
            ReturnDate_DTP.Enabled = _entity.EEstadoCobro == EEstado.Devuelto;
		}

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            base.RefreshMainData();
        }

        #endregion

        #region Validation & Format

        /// <summary>
        /// Valida datos de entrada
        /// </summary>
        protected override void ValidateInput()
        {
        }

        #endregion

        #region Actions

        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        protected void ChangeStateAction(EEstado estado)
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

        #endregion

        #region Buttons

        private void EstadoCobro_BT_Click(object sender, EventArgs e)
        {
            SelectEnumInputForm form = new SelectEnumInputForm(true);

            EEstado[] list = { EEstado.Pendiente, EEstado.Charged, EEstado.Devuelto };
            form.SetDataSource(Library.Common.EnumText<EEstado>.GetList(list));

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ComboBoxSource estado = form.Selected as ComboBoxSource;
                _entity.EstadoCobro = estado.Oid;
            }

            ReturnDate_DTP.Enabled = _entity.EEstadoCobro == EEstado.Devuelto;
        }

        protected override void SetCuentaAction()
        {
            BankAccountSelectForm form = new BankAccountSelectForm(this, BankAccountList.GetList(ETipoCuenta.CuentaCorriente, EEstado.Active, false));

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                BankAccountInfo cuenta = form.Selected as BankAccountInfo;

                _entity.OidCuentaBancaria = cuenta.Oid;
                _entity.CuentaBancaria = cuenta.Valor;
                _entity.Entidad = cuenta.Entidad;
            }
        }

        #endregion

        #region Events

		private void Negociado_CkB_CheckedChanged(object sender, EventArgs e)
		{
			ChargeDate_DTP.Enabled = Negociado_CkB.Checked;
		}

        #endregion
    }
}
