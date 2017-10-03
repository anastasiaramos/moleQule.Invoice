using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using moleQule.Face;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class BudgetSelectForm : BudgetMngForm
    {
        #region Factory Methods

        public BudgetSelectForm()
            : this(null) {}

        public BudgetSelectForm(Form parent)
            : this(parent, 0, 0) {}

        public BudgetSelectForm(Form parent, long oid_serie, long oid_cliente)
            : base(true, parent)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
            
			_oid_cliente = oid_cliente;
            _oid_serie = oid_serie;            
        }

        public BudgetSelectForm(Form parent, BudgetList lista)
            : base(true, parent, lista)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Accion por defecto. Se usa para el Double_Click del Grid
        /// <returns>void</returns>
        /// </summary>
        protected override void DefaultAction() 
        {
            if (Tabla.CurrentRow != null)
                Tabla.CurrentRow.Cells[Seleccionar.Name].Value = "True";
            ExecuteAction(molAction.Select); 
        }

        public override void SelectObject()
        {
            Datos.MoveFirst();
            Datos.MoveLast();

            List<BudgetInfo> list = new List<BudgetInfo>();

            foreach (DataGridViewRow row in Tabla.Rows)
            {
                if (row.Cells[Seleccionar.Name] != null)
                    if (((DataGridViewCheckBoxCell)row.Cells[Seleccionar.Name]).Value.ToString() == "True")
                        list.Add(row.DataBoundItem as BudgetInfo);
            }

            _selected = list;
            _action_result = list.Count > 0 ? DialogResult.OK : DialogResult.Cancel;
        }

        public override void SelectAll()
        {
            Datos.MoveFirst();
            Datos.MoveLast();

            List<BudgetInfo> list = new List<BudgetInfo>();

            foreach (DataGridViewRow row in Tabla.Rows)
            {
                list.Add(row.DataBoundItem as BudgetInfo);
            }

            _selected = list;
            _action_result = list.Count > 0 ? DialogResult.OK : DialogResult.Cancel;
        }

        #endregion
    }
}
