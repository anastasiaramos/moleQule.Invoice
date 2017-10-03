using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face.Common;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class REAChargeUIForm : CobroREAForm
    {
        #region Attributes & Properties

        public new const string ID = "CobroREAUIForm";
        public new static Type Type { get { return typeof(REAChargeUIForm); } }

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected Charge _entity;

        protected REAExpedients _rea_expedients;

        public override Charge Entity { get { return _entity; } set { _entity = value; } }
        public override ChargeInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(true) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected REAChargeUIForm() 
			: this(-1, false, null) { }

        public REAChargeUIForm(Form parent) 
			: this(-1, true, parent) { }

        public REAChargeUIForm(long oid) 
			: this(oid, true, null) { }

        public REAChargeUIForm(long oid, bool isModal, Form parent)
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

            //Creamos los cobros REA de los expedientes seleccionados
            if (!Asignar()) return false;

            // do the save
            try
            {
                Charge temp = _entity.Clone();
				temp.ApplyEdit();

                _entity = temp.Save();
                _entity.ApplyEdit();

                _rea_expedients.Save();

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

        #region Validation & Format

        /// <summary>
        /// Valida datos de entrada
        /// </summary>
        protected override void ValidateInput()
        {
        }

        #endregion

		#region Business Methods

		protected virtual bool Asignar()
		{
			if (_entity.Pendiente == 0)
			{
				PgMng.ShowInfoException("No existe cantidad pendiente de asignación en este cobro.");
				return false;
			}

			if (NoAsignado > _entity.Pendiente)
			{
				PgMng.ShowInfoException(string.Format("La asignación {0:C2} es superior a la cantidad pendiente en el cobro {1:C2}.", NoAsignado, _entity.Pendiente));
				return false;
			}

			foreach (FacREAInfo item in _facturas)
			{
                REAExpedient rea_expedient = _rea_expedients.GetItem(item.OidExpedienteREA);

                if (item.Vinculado == Library.Invoice.Resources.Labels.RESET_COBRO)
                {
                    try
                    {
                        if (item.Asignado < 0)
                        {
                            PgMng.ShowInfoException(string.Format("El importe de la asignación del expediente {0} es negativo.", item.NExpediente));
                            return false;
                        }

                        CobroREA c = _entity.CobroREAs.GetItemByExpedienteREA(item.OidExpedienteREA);
                        if ((item.Asignado > item.TotalAyuda) && (c == null))
                        {
                            PgMng.ShowInfoException(string.Format("El importe de la asignación del expediente {0} es superior a la cantidad pendiente de cobro.", item.NExpediente));
                            return false;
                        }
                    }
                    catch
                    {
                        PgMng.ShowInfoException(string.Format("El valor asignado al expediente {0} no es válido.", item.NExpediente));
                        return false;
                    }

                    if (rea_expedient != null) 
                        rea_expedient.EEstado = EEstado.Charged;
                }
                else
                {
                    if (rea_expedient != null) 
                        if (rea_expedient.EEstado == EEstado.Charged)
                            rea_expedient.EEstado = EEstado.Abierto;
                }
			}

			return true;
		}

		protected void UpdateImporte()
		{
			decimal _asignado = 0;

			foreach (FacREAInfo item in _facturas)
				_no_asignado += item.Asignado;

			if (_entity.Importe >= 0)
				_entity.Importe = (_entity.Importe) > _asignado ? _entity.Importe : _asignado;
			else
				_entity.Importe = (_entity.Importe) < _asignado ? _entity.Importe : _asignado;
		}

		#endregion

        #region Actions

        protected override void SaveAction()
        {
            if (_entity.EMedioPago == EMedioPago.Seleccione)
            {
                MessageBox.Show("No ha seleccionado el Medio de pago");
                _action_result = DialogResult.Ignore;
                return;
            }

            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		private void SetPaymentMethodAction()
		{
			EMedioPago[] list = { EMedioPago.Domiciliacion, EMedioPago.Ingreso, EMedioPago.Transferencia };
			SelectEnumInputForm form = new SelectEnumInputForm(true);
			form.SetDataSource(Library.Common.EnumText<EMedioPago>.GetList(list));

			try
			{
				Datos.RaiseListChangedEvents = false;

				if (form.ShowDialog(this) == DialogResult.OK)
				{
					ComboBoxSource item = form.Selected as ComboBoxSource;
					_entity.MedioPago = item.Oid;
					MedioPago_TB.Text = _entity.EMedioPagoLabel;

					if (!Library.Common.EnumFunctions.NeedsCuentaBancaria(_entity.EMedioPago))
					{
						_entity.OidCuentaBancaria = 0;
						_entity.CuentaBancaria = string.Empty;
						CuentaBancaria_TB.Text = string.Empty;
					}
				}
			}
			finally
			{
				Datos.RaiseListChangedEvents = true;
			}
		}

		protected override void LinkREAExpedientAction(DataGridViewRow row)
		{
			if (row == null) return;

			if (NoAsignado == 0)
			{
				UnlinkREAExpedientAction(row);
				return;
			} 

			FacREAInfo item = row.DataBoundItem as FacREAInfo;

			if (item == null) return;

			//Se le ha asignado algo a mano
			if (item.Asignado != 0)
			{
				//Cobros en positivo
				if (_entity.Importe >= 0)
				{
					if (item.Asignado > NoAsignado) item.Asignado = NoAsignado;
					//if (item.Asignado > item.Pendiente) item.Asignado = item.Pendiente;
				}
				//Cobros en negativo. Abonos
				else
				{
					if (item.Asignado < NoAsignado) item.Asignado = NoAsignado;
					if (item.Asignado < item.Pendiente) item.Asignado = item.Pendiente;
				}
			}
			else
			{
				//Cobros en positivo
				if (_entity.Importe >= 0)
				{
					if (item.Pendiente <= NoAsignado) item.Asignado = item.Pendiente;
					else item.Asignado = NoAsignado;
				}
				//Cobros en negativo. Abonos
				else
				{
					if (item.Pendiente >= NoAsignado) item.Asignado = item.Pendiente;
					else item.Asignado = NoAsignado;
				}
			}

			CobroREA cobro = _entity.CobroREAs.GetItemByExpedienteREA(item.OidExpedienteREA);
			if (cobro == null)
			{
				cobro = _entity.CobroREAs.NewItem(_entity, item);
				item.FechaAsignacion = DateTime.Now.ToShortDateString();
			}
			else
			{
				cobro.CopyFrom(_entity, item);
				item.FechaAsignacion = DateTime.Now.ToShortDateString();
			}
            
			UpdateAsignado();

			MarkAsNoActiva(row);
		}

		protected override void UnlinkREAExpedientAction(DataGridViewRow row)
		{
			if (row == null) return;

			FacREAInfo item = row.DataBoundItem as FacREAInfo;
			item.Asignado = 0;

			CobroREA cobro = Entity.CobroREAs.GetItemByExpedienteREA(item.OidExpedienteREA);
			if (cobro != null) _entity.CobroREAs.Remove(cobro);

			UpdateAsignado();

			MarkAsActiva(row);
		}

		protected override void EditAmountAction(DataGridViewRow row)
		{
			InputDecimalForm form = new InputDecimalForm();
			form.Message = Resources.Labels.IMPORTE_COBRO;

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				FacREAInfo item = row.DataBoundItem as FacREAInfo;

				_no_asignado += item.Asignado;

				CobroFactura cobro = _entity.CobroFacturas.GetItemByFactura(item.Oid);
				if (cobro != null) cobro.Cantidad = 0;
				item.Cobrado -= item.Asignado;
				item.Pendiente += item.Asignado;

				item.Asignado = form.Value;
				LinkREAExpedientAction(row);
				SetUnlinkedGridValues(Facturas_DGW.Name);
				REAExpedients_BS.ResetBindings(false);
				SetGridColors(Facturas_DGW);
			}
		}

        protected void LiberarTodoAction()
        {
            foreach (DataGridViewRow row in Facturas_DGW.Rows)
                UnlinkREAExpedientAction(row);

			REAExpedients_BS.ResetBindings(false);

			SetGridColors(Facturas_DGW);
		}

		protected void AsignarTodoAction()
		{
			foreach (DataGridViewRow row in Facturas_DGW.Rows)
				LinkREAExpedientAction(row);

			REAExpedients_BS.ResetBindings(false);

			SetGridColors(Facturas_DGW);
		}

        protected override void SetDueDateAction()
        {
            _entity.SetVencimiento(Vencimiento_DTP.Value);
        }

        #endregion

        #region Buttons

        private void Repartir_BT_Click(object sender, EventArgs e)
        {
            AsignarTodoAction();
        }

        private void Liberar_BT_Click(object sender, EventArgs e)
        {
            LiberarTodoAction();
        }

        private void Cuenta_BT_Click(object sender, EventArgs e)
        {
			BankAccountSelectForm form = new BankAccountSelectForm(this, BankAccountList.GetList(ETipoCuenta.CuentaCorriente, EEstado.Active, false));

            if (form.ShowDialog(this) == DialogResult.OK)
            {
				BankAccountInfo cuenta = form.Selected as BankAccountInfo;

                _entity.OidCuentaBancaria = cuenta.Oid;
                _entity.CuentaBancaria = cuenta.Valor;
                CuentaBancaria_TB.Text = cuenta.Valor;
                _entity.Entidad = cuenta.Entidad;
            }
        }
        
        #endregion

        #region Events
	        
        private void TipoExpediente_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TipoExpediente_CB.SelectedItem == null) return;
            if (_facturas_todas == null) return;

            ETipoExpediente tipo = (ETipoExpediente)((long)TipoExpediente_CB.SelectedValue);

            if (tipo != ETipoExpediente.Todos)
            {
                FCriteria criteria = new FCriteria<long>("TipoExpediente", (long)TipoExpediente_CB.SelectedValue);
				_facturas = FacREAList.GetSortedList(_facturas_todas.GetSubList(criteria), "NExpediente", ListSortDirection.Ascending);
            }
            else
            {
				_facturas = FacREAList.GetSortedList(_facturas_todas, "NExpediente", ListSortDirection.Ascending);
            }

            REAExpedients_BS.DataSource = _facturas;
			SetUnlinkedGridValues(Facturas_DGW.Name);
			SetGridColors(Facturas_DGW);
        }
		
		private void Importe_NTB_Validated(object sender, EventArgs e)
		{
			UpdateImporte();
		}

		private void MedioPago_BT_Click(object sender, EventArgs e)
		{
			SetPaymentMethodAction();
		}

        #endregion 
    }
}