using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
	public partial class DeliveryAddForm : DeliveryUIForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 2; } }

        #endregion

        #region Factory Methods

        public DeliveryAddForm()
			: this(ETipoEntidad.Cliente, null) { }

		public DeliveryAddForm(ETipoEntidad tipo, Form parent)
			: this(new object[2] { null, tipo }, parent) { }

		public DeliveryAddForm(WorkReportInfo workReport, Form parent)
			: this(new object[3] { null, ETipoEntidad.WorkReport, workReport }, parent) { }

		public DeliveryAddForm(OutputDelivery entity, Form parent)
			: this(new object[2] { entity, ETipoEntidad.Cliente }, parent) { }

		public DeliveryAddForm(ClienteInfo cliente, PedidoInfo pedido, Form parent)
			: this(new object[3] { null, ETipoEntidad.Cliente, ETipoAlbaranes.Todos }, parent)
		{
			SetCliente(cliente);
			SetSerie(SerieInfo.Get(pedido.OidSerie, false), true);
			AddPedidoAction(pedido);
		}

		public DeliveryAddForm(object[] parameters, Form parent)
			: base(-1, parameters, true, parent)
		{
			InitializeComponent();
			SetFormData();
			_mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData(object[] parameters)
        {
			if (parameters[0] == null)
			{
				ETipoEntidad holder_type = (ETipoEntidad)parameters[1];

				_entity = OutputDelivery.New(holder_type);
				_entity.BeginEdit();

				switch (holder_type)
				{
					case ETipoEntidad.Cliente:
						{
							if (parameters.Length >= 3)
							{
								_delivery_type = (ETipoAlbaranes)parameters[2];

								if (_delivery_type == ETipoAlbaranes.Agrupados)
								{
									_entity.Contado = true;
									_entity.GetNewCode(ETipoEntidad.Cliente, ETipoAlbaranes.Agrupados);
									_entity.EMedioPago = EMedioPago.Efectivo;
								}
							}
						}
						break;

					case ETipoEntidad.WorkReport:
						{
							//Se ha pasado como parametro en el constructor
							if (parameters.Length >= 3)
							{
								_work_report = (WorkReportInfo)parameters[2];
							}
							//Venimos de un parte de trabajo y esta en cache
							else if (Cache.Instance.Contains(typeof(WorkReport)))
							{
								_work_report = (Cache.Instance.Get(typeof(WorkReport)) as WorkReport).GetInfo();
							}

							_entity.OidHolder = (_work_report != null) ? _work_report.Oid : 0;
							_entity.EMedioPago = EMedioPago.Efectivo;
							SerieInfo serie = SerieInfo.Get(Library.Invoice.ModulePrincipal.GetWorkDeliverySerieSetting(), false);
							_entity.OidSerie = (serie != null) ? serie.Oid : 0;
						}
						break;
				}
			}
			else
			{
				_entity = (OutputDelivery)parameters[0];
				_entity.BeginEdit();
				SerieInfo serie = SerieInfo.Get(_entity.OidSerie, false);
				_entity.OidSerie = (serie != null) ? serie.Oid : 0;
			}
        }

		#endregion

        #region Source

        protected override void RefreshMainData()
        {
			if (_entity.Contado) SetCliente(ClienteInfo.Get(139, false));
			PgMng.Grow();

            base.RefreshMainData();
        }

        #endregion	

		#region Business Methods

		protected override void SetCliente(ClienteInfo source)
		{
			if (source == null) return;

			base.SetCliente(source);

			if (_entity.Conceptos.Count == 0)
				_entity.AddProductosCliente(source, _serie); 
		}

		#endregion

		#region Actions

		protected virtual void AddPedidoAction(PedidoInfo albaran)
		{
			List<PedidoInfo> list = new List<PedidoInfo>();
			list.Add(albaran);

			AddPedidoAction(list);
		}

        protected override void ClusteredAction()
        {
            base.ClusteredAction();

            Entity.Contado = Contado_CB.Checked;
			_delivery_type = Entity.Contado ? ETipoAlbaranes.Agrupados : ETipoAlbaranes.Todos;
			Entity.GetNewCode(_entity.EHolderType, _delivery_type);

			Datos.ResetBindings(false);
        }

        protected override void RectificativoAction()
        {
            Entity.Rectificativo = Rectificativo_CKB.Checked;
            if (Entity.Rectificativo) Entity.Contado = false;

			Entity.GetNewCode(_entity.EHolderType, _delivery_type);

            if (Rectificativo_CKB.Checked)
                Contado_CB.CheckState = CheckState.Unchecked;

            Contado_CB.Enabled = !Rectificativo_CKB.Checked;
			
			Datos.ResetBindings(false);
        }

		#endregion	
    }
}

