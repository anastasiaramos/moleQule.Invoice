using System;
using System.Collections.Generic;
using System.Windows.Forms;

using moleQule.Library.Common;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class PedidoSelectForm : PedidoMngForm
    {

        #region Factory Methods

        public PedidoSelectForm()
            : this(null, EEstado.Todos) {}

        public PedidoSelectForm(Form parent, EEstado estado)
            : this(parent, null) {}

		public PedidoSelectForm(Form parent, PedidoList lista)
            : this(parent, EEstado.Todos, lista) {}

		protected PedidoSelectForm(Form parent, EEstado estado, PedidoList lista)
			: base(true, parent, estado, lista)
		{
			InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
		}
		
        #endregion

        #region Actions

        protected override void DefaultAction() 
		{
			SeleccionaLinea();
			ExecuteAction(molAction.Select); 
		}

		public override void SelectObject()
		{
			Datos.MoveFirst();
			Datos.MoveLast();

			List<PedidoInfo> list = new List<PedidoInfo>();

			foreach (DataGridViewRow row in Tabla.Rows)
			{
				if (IsSelected(row)) list.Add(row.DataBoundItem as PedidoInfo);
			}

			_selected = list;
			_action_result = list.Count > 0 ? DialogResult.OK : DialogResult.Cancel;
		}

		public override void SelectAll()
		{
			Datos.MoveFirst();
			Datos.MoveLast();

			List<PedidoInfo> list = new List<PedidoInfo>();

			foreach (DataGridViewRow row in Tabla.Rows)
			{
				list.Add(row.DataBoundItem as PedidoInfo);
			}

			_selected = list;
			this.DialogResult = list.Count > 0 ? DialogResult.OK : DialogResult.Cancel;
			Close();
		}

        #endregion

    }
}
