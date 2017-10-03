using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Store;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Invoice.Reports.BankLine;

namespace moleQule.Face.Invoice
{
    public partial class BankLinesActionForm : Skin01.ActionSkinForm
    {
        #region Atributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        public const string ID = "BankLinesActionForm";
        public static Type Type { get { return typeof(BankLinesActionForm); } }

        BankAccountInfo _cuenta;
        ClienteInfo _cliente;
        IAcreedorInfo _acreedor;
        EBankLineType _bank_line_type = EBankLineType.Todos;
        ETipoTitular _tipo_titular = ETipoTitular.Todos;
        EMedioPago _medio_pago = EMedioPago.Todos;

        #endregion

        #region Factory Methods

        public BankLinesActionForm()
            : this(null) {}

        public BankLinesActionForm(Form parent)
            : base(true, parent)
        {
            InitializeComponent();
            SetFormData();
        }

        #endregion

        #region Business Methods

        private string GetFilterValues()
        {
            string filtro = string.Empty;

            filtro += "Tipo " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + TipoMovimiento_CB.Text + "; ";

            if (!TodosCuentas_CkB.Checked)
                filtro += "Cuenta Bancaria " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + _cuenta.Valor + "; ";

            if ((ETipoTitular)(long)TipoTitular_CB.SelectedValue != ETipoTitular.Todos)
            {
                if ((ETipoTitular)(long)TipoTitular_CB.SelectedValue == ETipoTitular.Cliente)
                {
                    if (!TodosCliente_CkB.Checked)
                        filtro += "Titular " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + _cliente.Nombre + "; ";
                    else
                        filtro += "Tipo Titular " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + TipoTitular_CB.Text + "; ";
                }
                else
                {
                    if (!TodosAcreedor_CkB.Checked)
                        filtro += "Titular " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + _acreedor.Nombre + "; ";
                    else
                        filtro += "Tipo Titular " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + TipoTitular_CB.Text + "; ";
                }
            }
            if (FInicial_DTP.Checked)
                filtro += "Fecha " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.GreaterOrEqual) + " " + FInicial_DTP.Value.ToShortDateString() + "; ";

            if (FFinal_DTP.Checked)
                filtro += "Fecha " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.LessOrEqual) + " " + FFinal_DTP.Value.ToShortDateString() + "; ";

            if (_medio_pago != EMedioPago.Todos)
                filtro += "Medio Pago " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + MedioPago_CB.Text + "; ";

            return filtro;
        }

        private bool ValidateInput()
        {
            if ((_bank_line_type == EBankLineType.Cobro) && (!TodosCliente_CkB.Checked) && (_cliente == null))
            {
                MessageBox.Show(Resources.Messages.NO_CLIENT_SELECTED);
                return false;
            }

            if ((_bank_line_type == EBankLineType.PagoFactura) && (!TodosAcreedor_CkB.Checked) && (_acreedor == null))
            {
                MessageBox.Show(Resources.Messages.NO_PROV_SELECTED);
                return false;
            }

            return true;
        }

        #endregion

        #region Layout & Source

        public override void RefreshSecondaryData ()
        {
            Datos_TipoMovimiento.DataSource = Library.Invoice.EnumText<EBankLineType>.GetList(false, true);
            TipoMovimiento_CB.SelectedValue = (long)EBankLineType.Todos;
            PgMng.Grow();

            Datos_TipoTitular.DataSource = Library.Common.EnumText<ETipoTitular>.GetList(false, true);
            TipoTitular_CB.SelectedValue = (long)ETipoTitular.Todos;
            PgMng.Grow();

			Datos_MedioPago.DataSource = Library.Common.EnumText<EMedioPago>.GetList(false, true);
            MedioPago_CB.SelectedValue = (long)EMedioPago.Todos;
            PgMng.Grow();
        }

        #endregion

        #region Actions

        protected override void PrintAction()
        {
            if (!ValidateInput())
            {
                _action_result = DialogResult.Ignore;
                return;
            }

            PgMng.Reset(5, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

            string filtro = GetFilterValues();
            PgMng.Grow();

            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions();
            conditions.TipoMovimientoBanco = (EBankLineType)(long)TipoMovimiento_CB.SelectedValue;
            conditions.TipoTitular = (ETipoTitular)(long)TipoTitular_CB.SelectedValue;
            conditions.CuentaBancaria = TodosCuentas_CkB.Checked ? null : _cuenta;
            conditions.MedioPago = (EMedioPago)(long)MedioPago_CB.SelectedValue;
            conditions.FechaIni = FInicial_DTP.Checked ? FInicial_DTP.Value : DateTime.MinValue;
            conditions.FechaFin = FFinal_DTP.Checked ? FFinal_DTP.Value : DateTime.MaxValue;

            switch (conditions.TipoMovimientoBanco)
            {
                case EBankLineType.Cobro:
                    conditions.Titular = (TodosCliente_CkB.Checked) ? null : (ITitular)_cliente;
                    break;

                case EBankLineType.PagoFactura:
                    conditions.Titular = (TodosAcreedor_CkB.Checked) ? null : (ITitular)_acreedor;
                    break;

                case EBankLineType.Todos:
                case EBankLineType.SalidaCaja:
                    conditions.Titular = null;
                    break;
            }

            BankLineList movs = BankLineList.GetList(conditions, false);
            PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

            BankLineReportMng reportMng = new BankLineReportMng(AppContext.ActiveSchema, this.Text, filtro);
            ReportClass rpt = reportMng.GetListReport(movs);
            PgMng.FillUp();

			ShowReport(rpt);

            _action_result = DialogResult.Ignore;
        }

        #endregion

        #region Events

        private void TipoMovimiento_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            _bank_line_type = (EBankLineType)(long)TipoMovimiento_CB.SelectedValue;

            Cliente_GB.Enabled = (_bank_line_type == EBankLineType.Cobro);
            Acreedor_GB.Enabled = (_bank_line_type == EBankLineType.PagoFactura);
            
            switch (_bank_line_type)
            {
				case EBankLineType.PagoFactura:

                    TodosCliente_CkB.Checked = true;
                    _cliente = null;
                    Cliente_TB.Text = string.Empty;
                    
                    break;

                case EBankLineType.Cobro:

                    TodosAcreedor_CkB.Checked = true;
                    _acreedor = null;
                    Acreedor_TB.Text = string.Empty;
                
                    break;

				default:

					TodosCliente_CkB.Checked = true;
                    _cliente = null;
                    Cliente_TB.Text = string.Empty;

					TodosAcreedor_CkB.Checked = true;
                    _acreedor = null;
                    Acreedor_TB.Text = string.Empty;

					break;
            }
        }

        private void TipoTitular_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ETipoTitular tipoTitular = (ETipoTitular)(long)TipoTitular_CB.SelectedValue;

            Cliente_GB.Enabled = (tipoTitular == ETipoTitular.Todos);
            Acreedor_GB.Enabled = (tipoTitular == ETipoTitular.Todos);

            if (tipoTitular == ETipoTitular.Todos)
            {
                TodosCliente_CkB.Checked = true;
                TodosAcreedor_CkB.Checked = true;
                _cliente = null;
                _acreedor = null;
                Cliente_TB.Text = string.Empty;
                Acreedor_TB.Text = string.Empty;
            }
        }

        private void TodosCuenta_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Cuenta_BT.Enabled = !TodosCuentas_CkB.Checked;
        }

        private void TodosCliente_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Cliente_BT.Enabled = !TodosCliente_CkB.Checked;
        }

        private void TodosAcreedor_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Acreedor_BT.Enabled = !TodosAcreedor_CkB.Checked;
        }

        #endregion

        #region Buttons

        private void Cuenta_BT_Click(object sender, EventArgs e)
        {
			BankAccountSelectForm form = new BankAccountSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _cuenta = form.Selected as BankAccountInfo;
                Cuenta_TB.Text = _cuenta.Valor;
            }
        }

        private void Cliente_BT_Click(object sender, EventArgs e)
        {
            ClientSelectForm form = new ClientSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _cliente = form.Selected as ClienteInfo;
                Cliente_TB.Text = _cliente.Nombre;
            }
        }

        private void Acreedor_BT_Click(object sender, EventArgs e)
        {
            _tipo_titular = (ETipoTitular)(long)TipoTitular_CB.SelectedValue;

            switch (_tipo_titular)
            {
                case ETipoTitular.Despachante:
                    {
						CustomAgentSelectForm form = new CustomAgentSelectForm(this, EEstado.Active);
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            _acreedor = (IAcreedorInfo)form.Selected;
                            Acreedor_TB.Text = _acreedor.Nombre;                            
                        }
                    }
                    break;

                case ETipoTitular.Naviera:
                    {
						ShippingCompanySelectForm form = new ShippingCompanySelectForm(this, EEstado.Active);
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            _acreedor = (IAcreedorInfo)form.Selected;
                            Acreedor_TB.Text = _acreedor.Nombre;                            
                        }
                    }
                    break;

                case ETipoTitular.Proveedor:
                    {
						ProveedorList list = ProveedorList.GetList(EEstado.Active, false);
						SupplierSelectForm form = new SupplierSelectForm(this, list);

                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            _acreedor = (IAcreedorInfo)form.Selected;
                            Acreedor_TB.Text = _acreedor.Nombre;                            
                        }
                    }
                    break;

                case ETipoTitular.TransportistaOrigen:
				case ETipoTitular.TransportistaDestino:
                    {
                        TransporterSelectForm form = new TransporterSelectForm(this, TransporterList.GetList(false));
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            _acreedor = (IAcreedorInfo)form.Selected;
                            Acreedor_TB.Text = _acreedor.Nombre; 
                        }
                    }
                    break;
            }
        }

        #endregion
    }
}

