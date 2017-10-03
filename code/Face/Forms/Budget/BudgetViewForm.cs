using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
	public partial class BudgetViewForm : BudgetForm
	{
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata de la Proforma actual y que se va a editar.
        /// </summary>
        private BudgetInfo _entity;

        public override BudgetInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

        public BudgetViewForm(long oid) : this(oid, null) { }

        public BudgetViewForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFView;
		}

		protected override void GetFormSourceData(long oid, object[] parameters)
        {
            _entity = BudgetInfo.Get(oid, true);
        }

        #endregion

        #region Layout & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
			SetReadOnlyControls(this.Controls);
            this.Impresion_GB.Enabled = true;
            foreach (Control ctl in Impresion_GB.Controls)
            {
                switch (ctl.GetType().Name)
                {
                    case "TextBox":
                        ((TextBox)ctl).Enabled = true;
                        ((TextBox)ctl).ReadOnly = false;
                        break;
                    case "ComboBox":
                        ((ComboBox)ctl).Enabled = true;
                        break;
                    case "Button":
                        ((Button)ctl).Enabled = true;
                        break;
                    default:
                        ctl.Enabled = true;
                        break;
                }
            }

            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;
			NAlbaranManual_CKB.Visible = false;

			base.FormatControls();
        }

		protected override void RefreshMainData()
		{
			Datos.DataSource = _entity;
			Datos_Concepto.DataSource = _entity.Conceptos;
			PgMng.Grow();

			if (_entity.OidCliente > 0)
				Datos_Cliente.DataSource = ClienteInfo.Get(_entity.OidCliente, false);
			PgMng.Grow();

			if (_entity.OidSerie > 0)
			{
				SerieInfo serie = SerieInfo.Get(_entity.OidSerie, false);
				Serie_TB.Text = serie.Nombre;
				Nota_TB.Text = serie.Cabecera;
			}
			PgMng.Grow();

			Fecha_DTP.Value = _entity.Fecha;
			PgMng.Grow();

			base.RefreshMainData();
		}

        #endregion

		#region Validation & Format

		#endregion

        #region Actions

        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion

        #region Events

        #endregion
	}
}

