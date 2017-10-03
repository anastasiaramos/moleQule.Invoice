using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Store;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class LoanUIForm : LoanForm
    {
        #region Attributes & Properties
		
		public new static Type Type { get { return typeof(LoanUIForm); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected Loan _entity;

        public override Loan Entity { get { return _entity; } set { _entity = value; } }
        public override LoanInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }

        #endregion

        #region Factory Methods

		public LoanUIForm()
			: this(-1, null) { }

		public LoanUIForm(long oid, Form parent)
			: this(oid, null, true, parent) { }

        public LoanUIForm(Loan entity, Form parent)
            : this(-1, new object[1] { entity }, true, parent) {}

		public LoanUIForm(long oid, object[] parameters, bool isModal, Form parent)
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

            Loan temp = _entity.Clone();
			temp.ApplyEdit();

			// do the save
			try
			{
				_entity = temp.Save();
				_entity.ApplyEdit();

                //foreach (Payment item in _payments)
                //{
                //    item.OidAgente = _entity.Oid;
                //    foreach (TransactionPayment tr in item.Operations)
                //        tr.OidOperation = _entity.Oid;
                //}

                //_payments.Save();

				return true;
			}
			catch (Exception ex)
			{
				CleanCache();
				PgMng.ShowInfoException(ex);
				return false;
			}
			finally
			{
				this.Datos.RaiseListChangedEvents = true;
			}
        }

        #endregion
	
		#region Source
		
        /// <summary>
        /// Asigna el objeto principal al origen de datos principal
		/// y las listas hijas a los origenes de datos correspondientes
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            InterestRates_BS.DataSource = _entity.InterestRates;
            PgMng.Grow();
						
            base.RefreshMainData();
        }
		
        protected override void UpdatePayments()
        {
            Payments_BS.DataSource = _entity.Payments;
            Payments_BS.ResetBindings(false);
            SetGridColors(Pagos_DGW.Name);
        }
		
        #endregion

        #region Business Methods

        protected void ChangeState(EEstado estado)
        {
            if (!ControlsMng.IsCurrentItemValid(Pagos_DGW)) return;

            Payment payment = ControlsMng.GetCurrentItem(Pagos_DGW) as Payment;

            if (payment == null)
            {
                PgMng.ShowInfoException(Face.Resources.Messages.NO_SELECTED);
                return;
            }

            if (payment.EEstado == EEstado.Anulado)
            {
                PgMng.ShowInfoException(Face.Resources.Messages.ITEM_ANULADO_NO_EDIT);
                return;
            }

            switch (estado)
            {
                case EEstado.Anulado:

                    if (payment.EEstado == EEstado.Contabilizado)
                    {
                        PgMng.ShowInfoException(Library.Common.Resources.Messages.NULL_CONTABILIZADO_NOT_ALLOWED);
                        return;
                    }

                    if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.NULL_CONFIRM) != DialogResult.Yes)
                    {
                        return;
                    }
                    break;
            }

            try
            {
                payment.ChangeEstado(estado);
                UpdatePayments();
            }
            catch (Exception ex)
            {
                PgMng.ShowInfoException(ex);
            }
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
            if (_entity.CheckPeriodoTipoInteres())
                _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
            else
                PgMng.ShowWarningException(Resources.Messages.TIPOS_INTERES_NO_VALIDOS);
        }

        protected virtual void AsociaPagoFacturaAction()
        {
            BankAccountInfo cuenta = BankAccountInfo.Get(_entity.OidCuenta, false);

            if (cuenta.ETipoCuenta == ETipoCuenta.CuentaCorriente)
            {
                PgMng.ShowInfoException("No se puede asociar un pago de factura a una cuenta corriente. Debe seleccionar una cuenta de comercio exterior");
                return;
            }

            PaymentInfo pago = PaymentInfo.Get(_entity.OidPago, ETipoPago.Factura, false);

            if (pago.EMedioPago == EMedioPago.ComercioExterior)
            {
                PgMng.ShowInfoException("El pago asociado al préstamo actual ha sido generado automáticamente por la aplicación y no es posible modificarlo");
                return;
            }
            else
            {
                if (_entity.Payments.Count > 0)
                {
                    PgMng.ShowInfoException("El préstamo actual tiene pagos y no es posible modificar el pago de factura asociado");
                    return;
                }
                else
                {
					PaymentSelectForm form = new PaymentSelectForm(this, ETipoPago.Factura);

                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        PaymentInfo info = form.Selected as PaymentInfo;

                        _entity.OidPago = info.Oid;
                        _entity.Pago = _entity.Nombre;
                        _entity.Importe = info.Importe;
                        _entity.FechaFirma = info.Fecha;
                        _entity.FechaIngreso = info.Fecha;
                        _entity.FechaVencimiento = info.Vencimiento;

                    }
                }
            }
        }

        protected override void SetBankAccountAction()
        {
            BankAccountSelectForm form = new BankAccountSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                BankAccountInfo cuenta = form.Selected as BankAccountInfo;

                if (cuenta.ETipoCuenta == ETipoCuenta.CuentaCorriente && _entity.OidPago != 0)
                {
                    PgMng.ShowInfoException("Este préstamo tiene una factura asociada. Debe seleccionar una cuenta de Comercio Exterior");
                    return;
                }
                _entity.OidCuenta = cuenta.Oid;
                _entity.CuentaBancaria = cuenta.Valor;
                _entity.Entidad = cuenta.Entidad;
                _entity.CuentaBancariaAsociada = cuenta.CuentaAsociada;
            }
        }

        protected override void AddAllPaymentsAction() 
        {
            DateTime payment_date = _entity.InicioPago;
            LoanInfo entity_info = _entity.GetInfo();

            for (int i = 1; i <= _entity.NCuotas; i++)
            {
                Payment payment = _entity.Payments.GetItemByDueDate(payment_date);

                if (payment == null)
                {
                    payment = _entity.Payments.NewItem(entity_info, payment_date);
                    TransactionPayment transaction = payment.Operations.NewItem(payment, _entity, ETipoPago.Prestamo);
                    transaction.Cantidad = payment.Importe;
                }
                else if (payment.Vencimiento >= DateTime.Today)
                    payment.SetCuota(entity_info);

                payment.Observaciones = "PLAZO " + i + "/" + _entity.NCuotas;

                payment_date = payment_date.AddMonths(1);
            }

            UpdatePayments();
        }

        protected override void NewPaymentAction() 
        {
            Payment payment = _entity.Payments.NewItem(_entity.GetInfo());
            TransactionPayment transaction = payment.Operations.NewItem(payment, _entity, ETipoPago.Prestamo);
            transaction.Cantidad = payment.Importe;

            LoanPaymentEditForm form = new LoanPaymentEditForm(payment, ETipoPago.Prestamo, _entity, this);
            form.ShowDialog();

            if (form.ActionResult != DialogResult.OK)
                _entity.Payments.Remove(payment);

            UpdatePayments();

            UpdateImportes();
        }

        protected override void EditPaymentAction()
        {
            if (!ControlsMng.IsCurrentItemValid(Pagos_DGW)) return;

            Payment item = ControlsMng.GetCurrentItem(Pagos_DGW) as Payment;

            if (item.Operations.Count == 0) item.LoadChilds(typeof(TransactionPayment), false);

			LoanPaymentEditForm form = new LoanPaymentEditForm(item, ETipoPago.Prestamo, _entity, this);
            form.ShowDialog();

            UpdatePayments();

            UpdateImportes();
        }

        protected override void ViewPaymentAction()
        {
            if (Payments_BS.Current == null) return;
            LoanPaymentViewForm form = new LoanPaymentViewForm((Payments_BS.Current as PaymentInfo).Oid, ETipoPago.Prestamo, this);
            form.ShowDialog();
        }

        protected override void DeletePagoAction()
        {
            ChangeState(EEstado.Anulado);
            UpdateImportes();
        }

        protected override void UnlockPaymentAction()
        {
            ChangeState(EEstado.Abierto);
        }

        protected override void LockPaymentAction()
        {
            ChangeState(EEstado.Contabilizado);
        }

        protected override void AddPeriodoTipoInteresAction()
        {
            _entity.InterestRates.NewItem(_entity);
            ControlsMng.UpdateBinding(InterestRates_BS);

            UpdateImportes();
        }

        protected override void DeletePeriodoTipoInteresAction()
        {
            if (InterestRates_BS.Current == null) return;

            if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.DELETE_CONFIRM) == DialogResult.Yes)
            {
                InterestRate item = (InterestRate)InterestRates_BS.Current;
                _entity.InterestRates.Remove(item);
            }

            UpdateImportes();
        }

        protected override void UpdateImportes()
        {
            decimal total = _entity.Importe;

            _entity.TotalPagado = 0;
            _entity.Asignado = 0;
            _entity.PendienteAsignar = 0;

            int n_pagos = 0;
           
            foreach (Payment payment in _entity.Payments)
            {
                if (payment.EEstado == EEstado.Anulado) continue;
                PaymentInfo pginfo = payment.GetInfo(false);

                total -= pginfo.Importe;

                _entity.TotalPagado += pginfo.Total;
                _entity.PendienteAsignar += pginfo.PendienteAsignar;

                n_pagos++;
            }

            _entity.CapitalPendiente = total;
            _entity.CapitalAmortizado = _entity.Importe - total;

            _entity.Pendiente = _entity.ImporteCuota * (_entity.NCuotas - n_pagos); 

            Datos.ResetBindings(false);
        }

        protected override void NoContabilizarAction()
        {
            CuentaContable_TB.Mask = string.Empty;
            _entity.CuentaContable = string.Empty.PadLeft(Library.Common.ModulePrincipal.GetNDigitosCuentasContablesSetting(), '0');
        }

        protected override void CustomAction1() { GenerarPagosAction(); }

        protected virtual void GenerarPagosAction()
        {
            UpdatePayments();
        }

        #endregion

        #region Buttons

        private void Pago_BT_Click(object sender, EventArgs e)
        {
            AsociaPagoFacturaAction();
        }

        private void Status_BT_Click(object sender, EventArgs e)
        {
            SelectEnumInputForm form = new SelectEnumInputForm(true);

            EEstado[] list = { EEstado.Abierto, EEstado.Pagado };
            form.SetDataSource(Library.Common.EnumText<EEstado>.GetList(list));

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ComboBoxSource estado = form.Selected as ComboBoxSource;
                _entity.Estado = estado.Oid;
            }
        }

        #endregion

        #region Events

        #endregion
    }
}