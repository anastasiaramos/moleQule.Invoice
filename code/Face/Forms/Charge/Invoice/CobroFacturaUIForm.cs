using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Csla;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Face.Common;

namespace moleQule.Face.Invoice
{
    public partial class CobroFacturaUIForm : Skin01.InputSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

        public const string ID = "CobroFacturaUIForm";
        public static Type Type { get { return typeof(CobroFacturaUIForm); } }

        protected Charge _entity;
        protected Cliente _cliente;
        protected TPVInfo _tpv = null;
        protected List<CobroFactura> _cobros = new List<CobroFactura>();
        protected decimal _no_asignado = 0;

        public Charge Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }

        private OutputInvoiceInfo FacturaActual { get { return Datos_Facturas.Current as OutputInvoiceInfo; } }
        protected decimal NoAsignado { get { return _no_asignado; } }

        #endregion

        #region Factory Methods

        protected CobroFacturaUIForm()
            : this(null, null) { }

        public CobroFacturaUIForm(Cliente cliente, Form parent)
            : this(true, cliente, parent) { }

        public CobroFacturaUIForm(bool is_modal, Cliente cliente, Form parent)
            : base(is_modal, parent)
        {
            InitializeComponent();
            _cliente = cliente;
        }

        protected bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            // do the save
            try
            {
                _cliente.ApplyEdit();
                _cliente.Save();
                _cliente.BeginEdit();

                return true;
            }
            catch (Exception ex)
            {
                PgMng.ShowInfoException(Face.Resources.Messages.OPERATION_ERROR + Environment.NewLine + ex.Message);
                return false;
            }
            finally
            {
                this.Datos.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Layout

        public override void FitColumns()
        {
            /*List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            cols.Clear();
            DescripcionCol.Tag = 0.6;
            ObservacionesCol.Tag = 0.4;

            cols.Add(DescripcionCol);
            cols.Add(ObservacionesCol);

            ControlsMng.MaximizeColumns(Lineas_DGW, cols);*/
        }

        public override void FormatControls()
        {
            base.FormatControls();

            MaximizeForm(new Size(this.Width, 0));

            Facturas_DGW.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
        }

        protected virtual void SetGridColors(string grid_name)
        {
            if (grid_name == Facturas_DGW.Name)
            {
                OutputInvoiceInfo item;

                foreach (DataGridViewRow row in Facturas_DGW.Rows)
                {
                    item = row.DataBoundItem as OutputInvoiceInfo;
                    if (item == null) continue;

                    // Si ya estaba asignado entonces lo marcamos como asignado
                    if (_entity.CobroFacturas.GetItemByFactura(item.Oid) == null)
                        MarkAsActiva(row);
                    else
                        MarkAsNoActiva(row);
                }
            }
        }

        public void SetReadOnly()
        {
            Source_GB.Enabled = false;
            Facturas_GB.Enabled = false;
            Submit_BT.Enabled = false;

            Datos_Facturas.DataSource = OutputInvoiceList.GetByCobroList(_entity.Oid, true);
        }

        protected void MarkAsNoActiva(DataGridViewRow row)
        {
            OutputInvoiceInfo item = row.DataBoundItem as OutputInvoiceInfo;
            item.Vinculado = Library.Invoice.Resources.Labels.RESET_COBRO;
            row.Cells[Asignacion.Index].Style.BackColor = Color.LightGreen;
        }

        protected void MarkAsActiva(DataGridViewRow row)
        {
            OutputInvoiceInfo item = row.DataBoundItem as OutputInvoiceInfo;
            item.Vinculado = Library.Invoice.Resources.Labels.SET_COBRO;
            row.Cells[Asignacion.Index].Style.BackColor = row.Cells[Pendiente.Index].Style.BackColor;
        }

        private void MarkControl(Control ctl)
        {
            if (ctl.Name == NoAsignado_TB.Name)
            {
                if (_entity.Importe > 0)
                    NoAsignado_TB.BackColor = (NoAsignado == 0) ? Color.LightGray : (NoAsignado > 0) ? Color.LightGreen : Color.Red;
                else
                    NoAsignado_TB.BackColor = (NoAsignado == 0) ? Color.LightGray : (NoAsignado < 0) ? Color.LightGreen : Color.Red;
            }
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            Fecha_DTP.Value = _entity.Fecha;
            Vencimiento_DTP.Value = _entity.Vencimiento;

        }

        protected virtual void SetUnlinkedGridValues(string grid_name)
        {
            SortedBindingList<OutputInvoiceInfo> sorted_facturas = Datos_Facturas.DataSource as SortedBindingList<OutputInvoiceInfo>;
            if (sorted_facturas != null)
            {
                OutputInvoiceList facturas = OutputInvoiceList.GetList(sorted_facturas);
                facturas.UpdateCobroValues(_entity);
            }
            UpdateAsignado();
        }

        #endregion

        #region Business Methods

        protected bool Asignar()
        {
            if (_entity.EMedioPago == EMedioPago.CompensacionFactura)
            {
                decimal importe = 0;

                Datos_Facturas.MoveFirst();
                foreach (DataGridViewRow row in Facturas_DGW.Rows)
                {
                    OutputInvoiceInfo item = row.DataBoundItem as OutputInvoiceInfo;

                    if (item.Vinculado == Library.Invoice.Resources.Labels.RESET_COBRO)
                        importe += item.Asignado;

                    Datos_Facturas.MoveNext();
                }

                if (importe != 0)
                {
                    PgMng.ShowInfoException("El importe total para este tipo de cobro debe ser 0.");
                    _action_result = DialogResult.Ignore;
                    return false;
                }
            }
            else
            {
                if (_entity.Pendiente == 0) return true;

                if (_entity.Importe < 0)
                {
                    if (NoAsignado < _entity.Pendiente)
                    {
                        PgMng.ShowInfoException(string.Format("La asignación {0:C2} es inferior a la cantidad pendiente en el cobro {1:C2}.", NoAsignado, _entity.Pendiente));
                        _action_result = DialogResult.Ignore;
                        return false;
                    }
                }
                else
                {
                    if (NoAsignado > _entity.Pendiente)
                    {
                        PgMng.ShowInfoException(string.Format("La asignación {0:C2} es superior a la cantidad pendiente en el cobro {1:C2}.", NoAsignado, _entity.Pendiente));
                        _action_result = DialogResult.Ignore;
                        return false;
                    }
                }
            }

            return true;
        }

        protected void UpdateAsignado()
        {
            decimal _asignado = 0;

            SortedBindingList<OutputInvoiceInfo> facturas = Datos_Facturas.DataSource as SortedBindingList<OutputInvoiceInfo>;

            if (facturas != null)
            {

                foreach (OutputInvoiceInfo item in facturas)
                    _asignado += item.Asignado;

                if (_entity.EMedioPago != EMedioPago.CompensacionFactura)
                {
                    _no_asignado = (Entity.Importe - _asignado);

                    if (_entity.Importe > 0)
                        _no_asignado = (_no_asignado < 0) ? 0 : _no_asignado;
                    else if (_entity.Importe < 0)
                        _no_asignado = (_no_asignado > 0) ? 0 : _no_asignado;
                }
                else
                {
                    _no_asignado = -_asignado;
                    _entity.Importe = _asignado;
                }

                NoAsignado_TB.Text = _no_asignado.ToString("N2");
                MarkControl(NoAsignado_TB);
            }

        }

        protected void UpdateImporte()
        {
            decimal _asignado = 0;

            SortedBindingList<OutputInvoiceInfo> facturas = Datos_Facturas.DataSource as SortedBindingList<OutputInvoiceInfo>;

            if (facturas != null)
            {
                foreach (OutputInvoiceInfo item in facturas)
                    _asignado += item.Asignado;

                if (_entity.Importe >= 0)
                    _entity.Importe = (_entity.Importe) > _asignado ? _entity.Importe : _asignado;
                else
                    _entity.Importe = (_entity.Importe) < _asignado ? _entity.Importe : _asignado;

                UpdateAsignado();
            }
        }

        #endregion

        #region Actions

        protected virtual void SetDueDateAction()
        {
            _entity.SetVencimiento(Vencimiento_DTP.Value);
            EstadoCobro_TB.Text = _entity.EstadoCobroLabel;
        }

        protected virtual void SetChargeStatusAction()
        {
            EEstado[] estados = new EEstado[3] { EEstado.Pendiente, EEstado.Charged, EEstado.Devuelto };

            SelectEnumInputForm form = new SelectEnumInputForm(true);
            form.SetDataSource(Library.Common.EnumText<EEstado>.GetList(estados));
            try
            {
                Datos.RaiseListChangedEvents = false;

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    ComboBoxSource item = form.Selected as ComboBoxSource;

                    _entity.EstadoCobro = item.Oid;
                    EstadoCobro_TB.Text = _entity.EstadoCobroLabel;
                }
            }
            finally
            {
                Datos.RaiseListChangedEvents = true;

            }
        }

        protected virtual void SetPaymentMethodAction()
        {
            SelectEnumInputForm form = new SelectEnumInputForm(true);
            form.SetDataSource(Library.Common.EnumText<EMedioPago>.GetList(false));

            try
            {
                Datos.RaiseListChangedEvents = false;

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    ComboBoxSource item = form.Selected as ComboBoxSource;

                    _entity.SetMedioPago(item.Oid);
                    MedioPago_TB.Text = _entity.EMedioPagoLabel;

                    Importe_NTB.Enabled = true;
                    TPV_BT.Enabled = false;
                    Cuenta_BT.Enabled = true;
                    EstadoCobro_BT.Enabled = false;

                    _tpv = (_entity.EMedioPago != EMedioPago.Tarjeta) ? null : _tpv;
                    _entity.OidTPV = 0;
                    _entity.TPV = string.Empty;

                    switch (_entity.EMedioPago)
                    {
                        case EMedioPago.CompensacionFactura:
                            {
                                Importe_NTB.Text = _entity.Importe.ToString("N2");
                                UnlinkAllAction();
                                Importe_NTB.Enabled = false;
                                EstadoCobro_BT.Enabled = false;
                            }
                            break;

                        case EMedioPago.Tarjeta:
                            {
                                TPV_BT.Enabled = true;
                                Cuenta_BT.Enabled = false;
                                EstadoCobro_BT.Enabled = false;
                            }
                            break;

                        case EMedioPago.Pagare:
                            EstadoCobro_BT.Enabled = true;
                            break;
                    }

                    if (Library.Common.EnumFunctions.NeedsCuentaBancaria(_entity.EMedioPago))
                    {
                        if ((_entity.EMedioPago == EMedioPago.Tarjeta) && (_entity.OidTPV == 0))
                            SetTPVAction();
                        else if (_entity.OidCuentaBancaria == 0)
                            SetBankAccountAction();
                    }
                    else
                        Cuenta_TB.Text = string.Empty;
                }
                EstadoCobro_TB.Text = _entity.EstadoCobroLabel;
            }
            finally
            {
                Datos.RaiseListChangedEvents = true;

            }
        }

        protected virtual void SetBankAccountAction()
        {
            BankAccountSelectForm form = new BankAccountSelectForm(this, BankAccountList.GetList(EEstado.Active, false));

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                BankAccountInfo cuenta = form.Selected as BankAccountInfo;

                _entity.OidCuentaBancaria = cuenta.Oid;
                _entity.CuentaBancaria = cuenta.Valor;
                Cuenta_TB.Text = _entity.CuentaBancaria;
            }
        }

        protected virtual void SetTPVAction()
        {
            TPVSelectForm form = new TPVSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _tpv = form.Selected as TPVInfo;

                _entity.OidTPV = _tpv.Oid;
                _entity.TPV = _tpv.Nombre;
                _entity.OidCuentaBancaria = _tpv.OidCuentaBancaria;
                _entity.CuentaBancaria = _tpv.CuentaBancaria;
                _entity.CalculaGastos(_tpv);

                Cuenta_TB.Text = _entity.CuentaBancaria;
                TPV_TB.Text = _entity.TPV;
                GastosBancarios_NTB.Text = _entity.GastosBancarios.ToString("N2");
            }
        }

        protected virtual void LinkInvoiceAction(DataGridViewRow row)
        {
            if (row == null) return;

            OutputInvoiceInfo item = row.DataBoundItem as OutputInvoiceInfo;

            if (item == null) return;

            UpdateAsignado();

            if (_entity.EMedioPago != EMedioPago.CompensacionFactura)
            {
                //No permitimos asociar facturas si no queda asignado y la factura es distinto de cero
                if ((NoAsignado == 0) && (item.Total != 0))
                {
                    UnlinkInvoiceAction(row);
                    return;
                }
            }

            //Se le ha asignado algo a mano
            if (item.Asignado != 0)
            {
                if (_entity.EMedioPago != EMedioPago.CompensacionFactura)
                {
                    //Cobros en positivo
                    if (_entity.Importe >= 0)
                    {
                        if (item.Asignado > NoAsignado) item.Asignado = NoAsignado;
                        if (item.Asignado > item.PendienteVencido) item.Asignado = item.PendienteVencido;
                    }
                    //Cobros en negativo. Abonos
                    else
                    {
                        if (item.Asignado < NoAsignado) item.Asignado = NoAsignado;
                        if (item.Asignado < item.PendienteVencido) item.Asignado = item.PendienteVencido;
                    }
                }
            }
            else
            {
                if (_entity.EMedioPago != EMedioPago.CompensacionFactura)
                {
                    //Cobros en positivo
                    if (_entity.Importe >= 0)
                    {
                        if (item.PendienteVencido <= NoAsignado) item.Asignado = item.PendienteVencido;
                        else item.Asignado = NoAsignado;
                    }
                    //Cobros en negativo. Abonos
                    else
                    {
                        if (item.PendienteVencido >= NoAsignado) item.Asignado = item.PendienteVencido;
                        else item.Asignado = NoAsignado;
                    }
                }
                else
                {
                    item.Asignado = item.PendienteVencido;
                }
            }

            CobroFactura cobro = _entity.CobroFacturas.GetItemByFactura(item.Oid);
            if (cobro == null)
            {
                cobro = _entity.CobroFacturas.NewItem(_entity, item);
                item.FechaAsignacion = DateTime.Now.ToShortDateString();
                item.Cobrado += cobro.Cantidad;
                item.PendienteVencido -= cobro.Cantidad;
            }
            else
            {
                item.Cobrado -= cobro.Cantidad;
                item.PendienteVencido += cobro.Cantidad;
                cobro.CopyFrom(_entity, item);
                item.Cobrado += cobro.Cantidad;
                item.PendienteVencido -= cobro.Cantidad;
                item.FechaAsignacion = DateTime.Now.ToShortDateString();
            }

            UpdateAsignado();

            MarkAsNoActiva(row);
        }

        protected virtual void UnlinkInvoiceAction(DataGridViewRow row)
        {
            if (row == null) return;

            OutputInvoiceInfo item = row.DataBoundItem as OutputInvoiceInfo;

            if (item == null) return;

            UpdateAsignado();

            item.Asignado = 0;

            CobroFactura cobro = _entity.CobroFacturas.GetItemByFactura(item.Oid);
            if (cobro != null)
            {
                _entity.CobroFacturas.Remove(cobro);
                item.Cobrado -= cobro.Cantidad;
                item.PendienteVencido += cobro.Cantidad;
            }

            UpdateAsignado();

            MarkAsActiva(row);
        }

        protected virtual void EditAmountAction(DataGridViewRow row)
        {
            InputDecimalForm form = new InputDecimalForm();
            form.Message = Resources.Labels.IMPORTE_COBRO;

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                OutputInvoiceInfo item = row.DataBoundItem as OutputInvoiceInfo;

                _no_asignado += item.Asignado;

                CobroFactura cobro = _entity.CobroFacturas.GetItemByFactura(item.Oid);
                if (cobro != null) cobro.Cantidad = 0;
                item.Cobrado -= item.Asignado;
                item.PendienteVencido += item.Asignado;

                item.Asignado = form.Value;
                LinkInvoiceAction(row);
                SetUnlinkedGridValues(Facturas_DGW.Name);
                Datos_Facturas.ResetBindings(false);
                SetGridColors(Facturas_DGW.Name);
            }
        }

        protected void UnlinkAllAction()
        {
            UpdateImporte();

            foreach (DataGridViewRow row in Facturas_DGW.Rows)
                UnlinkInvoiceAction(row);

            SetUnlinkedGridValues(Facturas_DGW.Name);
            SetGridColors(Facturas_DGW.Name);
        }

        protected void LinkAllAction()
        {
            UpdateImporte();

            foreach (DataGridViewRow row in Facturas_DGW.Rows)
                LinkInvoiceAction(row);

            SetUnlinkedGridValues(Facturas_DGW.Name);
            SetGridColors(Facturas_DGW.Name);
        }

        #endregion

        #region Buttons

        private void Repartir_BT_Click(object sender, EventArgs e) { LinkAllAction(); }

        private void Liberar_BT_Click(object sender, EventArgs e) { UnlinkAllAction(); }

        private void Cuenta_BT_Click(object sender, EventArgs e) { SetBankAccountAction(); }

        private void TPV_BT_Click(object sender, EventArgs e) { SetTPVAction(); }

        private void EditFactura_TI_Click(object sender, EventArgs e)
        {
            if (Facturas_DGW.CurrentRow == null) return;
            EditAmountAction(Facturas_DGW.CurrentRow);
        }

        private void ViewFactura_TI_Click(object sender, EventArgs e)
        {
            if (FacturaActual == null) return;

            InvoiceViewForm form = new InvoiceViewForm(FacturaActual.Oid, this);
            form.ShowDialog(this);
        }

        #endregion

        #region Events

        protected override void EnableEvents(bool enable)
        {
            if (enable)
            {
                this.Fecha_DTP.ValueChanged += new System.EventHandler(this.Fecha_DTP_ValueChanged);
                this.Vencimiento_DTP.ValueChanged += new System.EventHandler(this.Vencimiento_DTP_ValueChanged);
            }
            else
            {
                this.Fecha_DTP.ValueChanged -= new System.EventHandler(this.Fecha_DTP_ValueChanged);
                this.Vencimiento_DTP.ValueChanged -= new System.EventHandler(this.Vencimiento_DTP_ValueChanged);
            }
        }

        private void CobroFUIForm_Shown(object sender, EventArgs e)
        {
            SetUnlinkedGridValues(Facturas_DGW.Name);
            SetGridColors(Facturas_DGW.Name);
        }

        private void Facturas_DGW_DoubleClick(object sender, EventArgs e)
        {
            if (Facturas_DGW.CurrentRow == null) return;
            EditAmountAction(Facturas_DGW.CurrentRow);
        }

        private void Facturas_DGW_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Facturas_DGW.CurrentRow == null) return;
            if (e.ColumnIndex == -1) return;

            if (Facturas_DGW.Columns[e.ColumnIndex].Name == Vinculado.Name)
            {
                DataGridViewRow row = Facturas_DGW.CurrentRow;
                OutputInvoiceInfo item = row.DataBoundItem as OutputInvoiceInfo;

                if (row.Cells[Vinculado.Index].Value.ToString() == Library.Invoice.Resources.Labels.SET_COBRO)
                    LinkInvoiceAction(row);
                else
                    UnlinkInvoiceAction(row);

                SetUnlinkedGridValues(Facturas_DGW.Name);
                SetGridColors(Facturas_DGW.Name);
            }
        }

        private void Importe_NTB_Validated(object sender, EventArgs e)
        {
            UpdateImporte();
        }

        private void MedioPago_BT_Click(object sender, EventArgs e)
        {
            SetPaymentMethodAction();
        }

        private void Fecha_DTP_ValueChanged(object sender, EventArgs e)
        {
            _entity.SetFechas(Fecha_DTP.Value, _tpv);
            Vencimiento_DTP.Value = _entity.Vencimiento;
        }

        private void Vencimiento_DTP_ValueChanged(object sender, EventArgs e)
        {
            SetDueDateAction();
        }

        private void EstadoCobro_BT_Click(object sender, EventArgs e)
        {
            SetChargeStatusAction();
        }

        #endregion
    }
}