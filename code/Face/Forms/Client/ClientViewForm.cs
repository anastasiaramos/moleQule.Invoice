using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Cliente;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
	public partial class ClientViewForm : ClientForm
	{
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del cliente actual y que se va a editar.
        /// </summary>
        private ClienteInfo _entity;

        public override ClienteInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

        public ClientViewForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
			SetFormData();
            this.Text = Resources.Labels.CLIENTE_DETAIL_TITLE + " " + EntityInfo.Nombre.ToUpper();
            _mf_type = ManagerFormType.MFView;
		}

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
            _entity = ClienteInfo.Get(oid, false);
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
            base.FormatControls();
			SetReadOnlyControls(this.Controls);

            Productos_TS.Enabled = false;
        }

        protected override void RefreshMainData()
        {
			Datos.DataSource = _entity;
		    PgMng.Grow();

            Datos_ProductoCliente.DataSource = _entity.Productos;
            PgMng.Grow();

            SelectTipoIDAction();

			base.RefreshMainData();
        }

        #endregion

		#region Validation & Format

		#endregion

		#region Actions

        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        protected override void CustomAction1() { PrintHistoriaAction(); }

        protected override void LoadPreciosAction()
        {
            if (_entity.Productos == null || _entity.Productos.Count == 0)
            {
                try
                {
                    PgMng.Reset(4, 1, Face.Resources.Messages.LOADING_DATA, this);

                    PgMng.Grow();

                    _entity.LoadChilds(typeof(ProductoCliente), false);
                    PgMng.Grow();

                    Datos_ProductoCliente.DataSource = _entity.Productos;
                    PgMng.Grow();
                }
                finally
                {
                    PgMng.FillUp();
                }
            }
        }

        protected override void LoadRegistroEmailsAction()
        {
            if (_entity.Emails == null)
            {
                try
                {
                    PgMng.Reset(4, 1, Face.Resources.Messages.LOADING_DATA, this);

                    PgMng.Grow();

                    _entity.LoadChilds(typeof(LineaRegistro), false);
                    PgMng.Grow();

                    Datos_Emails.DataSource = _entity.Emails;
                    PgMng.Grow();
                }
                finally
                {
                    PgMng.FillUp();
                }
            }
        }

        protected override void PrintHistoriaAction()
        {
            PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
            ClienteReportMng reportMng = new ClienteReportMng(AppContext.ActiveSchema, "Historia de Cliente", string.Empty);

            PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
            HistoriaClienteRpt report = reportMng.GetHistoriaClienteRpt(_entity);

            PgMng.FillUp();

            ShowReport(report);
        }

		#endregion

        #region Events

        #endregion
	}
}

