using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Csla;
using moleQule.Library.CslaEx; 
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports;
using moleQule.Library.Hipatia;
using moleQule.Face;
using moleQule.Face.Hipatia;

namespace moleQule.Face.Invoice
{
	public partial class ResumenCuentasMngForm : ResumenCuentasMngBaseForm
	{
		#region Attributes & Properties

        public const string ID = "ResumenCuentasMngForm";
        public static Type Type { get { return typeof(ResumenCuentasMngForm); } }
		public override Type EntityType { get { return typeof(Impuesto); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected ResumenCuentasContablesInfo _entity;

		#endregion

		#region Factory Methods

		public ResumenCuentasMngForm()
			: this(null) { }

		public ResumenCuentasMngForm(Form parent)
			: this(false, parent, null) { }

        protected ResumenCuentasMngForm(bool isModal, Form parent, ResumenCuentasContablesList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = ResumenCuentasContablesList.NewList().GetSortedList();
            SortProperty = Entidad.DataPropertyName;
            
        }

		#endregion
				
		#region Layout
		
		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Nombre.Tag = 1;

			cols.Add(Nombre);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}
		
		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
                case molView.Select:

                    HideAction(molAction.Add);
                    HideAction(molAction.Edit);
                    HideAction(molAction.Copy);
                    HideAction(molAction.Delete);
                    HideAction(molAction.View);
                    HideAction(molAction.PrintDetail);

					break;

				case molView.Normal:

                    HideAction(molAction.Add);
                    HideAction(molAction.Edit);
                    HideAction(molAction.Copy);
                    HideAction(molAction.Delete);
                    HideAction(molAction.View);
                    HideAction(molAction.PrintDetail);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Cuentas Contables");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = ResumenCuentasContablesList.GetList();
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Cuentas Contables");
		}

		#endregion

        #region Actions

        public override void OpenEditForm()
        {
        }

		#endregion

		#region Print

		public override void PrintList()
		{
            PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

            CommonReportMng reportMng = new CommonReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);
            PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

            CuentasContablesListRpt report = reportMng.GetListReport(ResumenCuentasContablesList.GetList((IList<ResumenCuentasContablesInfo>)Datos.List));
            PgMng.FillUp();

            ShowReport(report);
		}
		
		#endregion
	}

	public partial class ResumenCuentasMngBaseForm : Skin06.EntityMngSkinForm<ResumenCuentasContablesList, ResumenCuentasContablesInfo>
	{
		public ResumenCuentasMngBaseForm()
			: this(false, null, null) { }

		public ResumenCuentasMngBaseForm(bool isModal, Form parent, ResumenCuentasContablesList lista)
			: base(isModal, parent, lista) { }
	}
}
