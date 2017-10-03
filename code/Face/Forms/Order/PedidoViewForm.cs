using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Face;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class PedidoViewForm : PedidoForm
    {
        #region Attributes & Properties
		
        public const string ID = "PedidoViewForm";
		public static Type Type { get { return typeof(PedidoViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private PedidoInfo _entity;

        public override PedidoInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        public PedidoViewForm(long oid) 
			: this(oid, null) {}

        public PedidoViewForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFView;
        }

		protected override void GetFormSourceData(long oid)
		{
            _entity = PedidoInfo.Get(oid, true);
            _mf_type = ManagerFormType.MFView;
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
            //SetReadOnlyControls(this.Controls);
            
			base.FormatControls();
			
			Lineas_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.DataGridView == Lineas_DGW)
			{
				LineaPedidoInfo item = row.DataBoundItem as LineaPedidoInfo;
				row.DefaultCellStyle = (item.Pendiente != 0) ?  Face.ControlTools.Instance.CerradoStyle : Common.ControlTools.Instance.CerradoStyle;
			}
		}

		#endregion

		#region Source
		
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();
			
			Datos_Lineas.DataSource = _entity.Lineas;
			PgMng.Grow();			
			
            base.RefreshMainData();
        }

		public override void RefreshSecondaryData()
		{
			if (_entity.OidCliente != 0)
				Datos_Cliente.DataSource = ClienteInfo.Get(_entity.OidCliente);

			PgMng.Grow();
		}

        #endregion

		#region Actions

		protected override void SaveAction() { _action_result = DialogResult.Cancel; }

		protected override void CrearAlbaranAction()
		{
			if (_entity.EEstado != EEstado.Abierto) return;
			if (_entity.IsComplete()) throw new iQException(Resources.Messages.PEDIDO_COMPLETO);

			if (_action_result == DialogResult.OK)
			{
				ClienteInfo cliente = Datos_Cliente.DataSource as ClienteInfo;

				DeliveryAddForm form = new DeliveryAddForm(cliente, _entity, this);
				form.ShowDialog();
			}
		}

		#endregion
     }
}
