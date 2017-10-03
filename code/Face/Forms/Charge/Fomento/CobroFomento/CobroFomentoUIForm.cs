using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face.Common;

namespace moleQule.Face.Invoice
{
	public partial class CobroFomentoUIForm : CobroFomentoForm
	{
		#region Attributes & Properties

		public new const string ID = "CobroFomentoUIForm";
		public new static Type Type { get { return typeof(CobroFomentoUIForm); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		/// <summary>
		/// Se trata del objeto actual y que se va a editar.
		/// </summary>
        protected Charge _entity;

        public override Charge Entity { get { return _entity; } set { _entity = value; } }
		public override ChargeInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

		#endregion

		#region Factory Methods

		/// <summary>
		/// Declarado por exigencia del entorno. No Utilizar.
		/// </summary>
		protected CobroFomentoUIForm()
			: this(-1, false, null) { }

		public CobroFomentoUIForm(Form parent)
			: this(-1, true, parent) { }

		public CobroFomentoUIForm(long oid)
			: this(oid, true, null) { }

		public CobroFomentoUIForm(long oid, bool isModal, Form parent)
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

			//Creamos los cobros Fomento de los expedientes seleccionados
			if (!Asignar()) return false;

			// do the save
			try
			{
                Charge temp = _entity.Clone();
				temp.ApplyEdit();

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

			foreach (LineaFomentoInfo item in _facturas)
			{
				if (item.Vinculado == Library.Invoice.Resources.Labels.SET_COBRO)
				{
					try
					{
						if (item.Asignado < 0)
						{
							PgMng.ShowInfoException(string.Format("El importe de la asignación del expediente {0} es negativo.", item.IDExpediente));
							return false;
						}

						CobroREA c = _entity.CobroREAs.GetItemByExpedienteREA(item.Oid);
						if ((item.Asignado > item.Total) && (c == null))
						{
							PgMng.ShowInfoException(string.Format("El importe de la asignación del expediente {0} es superior a la cantidad pendiente de cobro.", item.IDExpediente));
							return false;
						}
					}
					catch
					{
						PgMng.ShowInfoException(string.Format("El valor asignado al expediente {0} no es válido.", item.IDExpediente));
						return false;
					}
				}
			}

			return true;
		}

		protected void UpdateImporte()
		{
			decimal _asignado = 0;

			foreach (LineaFomentoInfo item in _facturas)
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

		private void SetMedioPagoAction()
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

		protected override void VinculaFacturaAction(DataGridViewRow row)
		{
			VinculaFacturaAction(row, false);
		}

		protected void VinculaFacturaAction(DataGridViewRow row, bool inputValue)
		{
			if (row == null) return;

			//if (NoAsignado == 0)
			//{
			//    DesvinculaFacturaAction(row);
			//    return;
			//} 

			LineaFomentoInfo item = row.DataBoundItem as LineaFomentoInfo;

			if (item == null) return;

			item.Cobrado = true;

			//Se le ha asignado algo a mano
			if (item.Asignado != 0 || inputValue)
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

			CobroREA cobro = _entity.CobroREAs.GetItemByExpedienteREA(item.Oid);
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

		protected override void DesvinculaFacturaAction(DataGridViewRow row)
		{
			if (row == null) return;

			LineaFomentoInfo item = row.DataBoundItem as LineaFomentoInfo;
			item.Asignado = 0;
			item.Cobrado = false;

			CobroREA cobro = Entity.CobroREAs.GetItemByExpedienteREA(item.Oid);
			if (cobro != null) _entity.CobroREAs.Remove(cobro);

			UpdateAsignado();

			MarkAsActiva(row);
		}

		protected override void EditarImporteAction(DataGridViewRow row)
		{
			InputDecimalForm form = new InputDecimalForm();
			form.Message = Resources.Labels.IMPORTE_COBRO;

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				LineaFomentoInfo item = row.DataBoundItem as LineaFomentoInfo;

				_no_asignado += item.Asignado;

				CobroFactura cobro = _entity.CobroFacturas.GetItemByFactura(item.Oid);
				if (cobro != null) cobro.Cantidad = 0;
				item.ImporteCobrado -= item.Asignado;
				item.Pendiente += item.Asignado;

				item.Asignado = form.Value;
				VinculaFacturaAction(row, true);
				SetUnlinkedGridValues(Facturas_DGW.Name);
				Datos_Facturas.ResetBindings(false);
				SetGridColors(Facturas_DGW);
			}
		}

		protected void LiberarTodoAction()
		{
			foreach (DataGridViewRow row in Facturas_DGW.Rows)
				DesvinculaFacturaAction(row);

			Datos_Facturas.ResetBindings(false);

			SetGridColors(Facturas_DGW);
		}

		protected void ApportionAction()
		{
			LineaFomentoList lines = LineaFomentoList.GetList((IList<LineaFomentoInfo>)Datos_Facturas.DataSource);

			decimal total_solicitado = lines.TotalSubvencionSolicitada();

			foreach (DataGridViewRow row in Facturas_DGW.Rows)
				ApportionAction(row, total_solicitado);

			Datos_Facturas.ResetBindings(false);

			SetGridColors(Facturas_DGW);
		}

		protected void ApportionAction(DataGridViewRow row, decimal totalSolicitado)
		{
			if (row == null) return;

			LineaFomentoInfo item = row.DataBoundItem as LineaFomentoInfo;

			if (item == null) return;

			item.Cobrado = true;

			decimal proportion = (_entity.Importe != 0) 
										? _entity.Importe * (item.Pendiente * 100 / totalSolicitado) / 100
										: 0;

			//Cobros en positivo
			if (_entity.Importe >= 0)
			{
				if (proportion <= NoAsignado) item.Asignado = proportion;
				else item.Asignado = NoAsignado;
			}
			//Cobros en negativo. Abonos
			else
			{
				if (proportion >= NoAsignado) item.Asignado = proportion;
				else item.Asignado = NoAsignado;
			}

			CobroREA cobro = _entity.CobroREAs.GetItemByExpedienteREA(item.Oid);
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

		protected void AsignarTodoAction()
		{
			foreach (DataGridViewRow row in Facturas_DGW.Rows)
				VinculaFacturaAction(row);

			Datos_Facturas.ResetBindings(false);

			SetGridColors(Facturas_DGW);
		}

		protected override void SetVencimientoAction()
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
			}
		}

		private void Proportional_BT_Click(object sender, EventArgs e)
		{
			ApportionAction();
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
				_facturas = LineaFomentoList.GetSortedList(_facturas_todas.GetSubList(criteria), "NExpediente", ListSortDirection.Ascending);
			}
			else
			{
				_facturas = LineaFomentoList.GetSortedList(_facturas_todas, "NExpediente", ListSortDirection.Ascending);
			}

			Datos_Facturas.DataSource = _facturas;
			SetUnlinkedGridValues(Facturas_DGW.Name);
			SetGridColors(Facturas_DGW);
		}

		private void Ano_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Ano_CB.SelectedItem == null) return;
			if (_facturas_todas == null) return;

			int anyo = Convert.ToInt32(((long)Ano_CB.SelectedValue));

			if (anyo != 0)
			{
				FCriteria criteria = new FCriteria<long>("Ano", anyo);
				_facturas = _facturas_todas.GetSortedSubList(criteria);
			}
			else
			{
				_facturas = _facturas_todas.GetSortedList();
			}

			Datos_Facturas.DataSource = _facturas;
			SetUnlinkedGridValues(Facturas_DGW.Name);
			SetGridColors(Facturas_DGW);

		}

		private void Importe_NTB_Validated(object sender, EventArgs e)
		{
			UpdateImporte();
		}

		private void MedioPago_BT_Click(object sender, EventArgs e)
		{
			SetMedioPagoAction();
		}

		#endregion 
	}
}