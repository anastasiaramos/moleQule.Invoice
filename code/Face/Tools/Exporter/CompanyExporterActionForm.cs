using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    public partial class CompanyExporterActionForm : Skin08.ActionSkinForm
    {
		protected enum ESchema { Source, Destination }

        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 4; } }

        public const string ID = "CompanyExporterActionForm";
        public static Type Type { get { return typeof(CompanyExporterActionForm); } }

		private CompanyInfo _source_company = null;
		private IAcreedorInfo _source_provider = null;
		private ClienteInfo _source_client = null;
		private List<OutputDeliveryInfo> _out_deliveries = new List<OutputDeliveryInfo>();
		private List<OutputInvoiceInfo> _out_invoices = new List<OutputInvoiceInfo>();

		private CompanyInfo _destination_company = null;
		private IAcreedorInfo _destination_provider = null;
		private ClienteInfo _destination_client = null;

		private IExporterMng _exporter;
		private ExporterConfig _config;

        #endregion

        #region Factory Methods

        public CompanyExporterActionForm() 
            : this(null) {}

		public CompanyExporterActionForm(Form parent)
			: base(true, parent) 
		{
            this.InitializeComponent();
            base.SetFormData();
        }

		public CompanyExporterActionForm(Form parent, OutputDeliveryInfo source)
			: this(parent)
		{
			SetOutputDeliverySource(source);
		}

		public CompanyExporterActionForm(Form parent, OutputInvoiceInfo source)
			: this(parent)
		{
			SetOutputInvoiceSource(source);
		}

        #endregion

		#region Layout

		#endregion

		#region Source

		public override void RefreshSecondaryData()
		{			
		}

		public void SetOutputDeliverySource(OutputDeliveryInfo source)
		{
			if (source == null) return;

			SetSourceClient(ClienteInfo.Get(source.OidHolder, false));

			_out_deliveries.Clear();
			_out_deliveries.Add(source);
			SetSourceDeliveries(_out_deliveries);
		}

		public void SetOutputInvoiceSource(OutputInvoiceInfo source)
		{
			if (source == null) return;

			SetSourceClient(ClienteInfo.Get(source.OidCliente, false));

			_out_invoices.Clear();
			_out_invoices.Add(source);
			SetSourceDeliveries(_out_deliveries);
		}

		#endregion

        #region Business Methods

        private bool GetSettings()
        {
			if (SourceOutDelivery_RB.Checked)
				_config.SourceEntityType = ETipoEntidad.OutputDelivery;
			else if (SourceOutInvoice_RB.Checked)
				_config.SourceEntityType = ETipoEntidad.FacturaEmitida;

			if (_config.DestinationCompany == null)
			{
				PgMng.ShowWarningException(Resources.Messages.NO_COMPANY_SELECTED);
				return false;
			}

			if (_config.DestinationHolder == null)
			{
				PgMng.ShowWarningException(Resources.Messages.NO_HOLDER_SELECTED);
				return false;
			}

			if (DestinationInDelivery_RB.Checked)
				_config.DestinationEntityType = ETipoEntidad.InputDelivery;
			else if (DestinationInInvoice_RB.Checked)
				_config.DestinationEntityType = ETipoEntidad.FacturaRecibida;
			else if (DestinationOutDelivery_RB.Checked)
				_config.DestinationEntityType = ETipoEntidad.OutputDelivery;
			else if (DestinationOutInvoice_RB.Checked)
				_config.DestinationEntityType = ETipoEntidad.FacturaEmitida;

			return true;
        }

		private void OpenForm(ETipoEntidad entityType, long oid)
		{
			switch (entityType)
			{
				case ETipoEntidad.Producto:
					{
						ProductEditForm form = new ProductEditForm(oid, this);
                        form.ShowDialog();
					} break;

				case ETipoEntidad.CurrencyExchange:
					{
						CurrencyExchangeUIForm form = new CurrencyExchangeUIForm(this);
						form.ShowDialog();
					} break;
			}
		}

		private void SetDestinationCompany(CompanyInfo source)
		{
			if (source.Oid == AppContext.ActiveSchema.Oid)
			{
				ProgressInfoMng.ShowInfo(Library.Invoice.Resources.Messages.EXPORTING_COMPANY_EQUALS);
				return;
			}

			_destination_company = source;

			Company_TB.Text = (_destination_company != null) ? _destination_company.Name : string.Empty;

			_config.DestinationCompany = _destination_company;
		}

		private void SetSourceClient(ClienteInfo source)
		{
			_source_client = source;
			SourceHolder_TB.Text = (_source_client != null) ? _source_client.Nombre : string.Empty;
		}

		private void SetSourceDeliveries(List<OutputDeliveryInfo> sources)
		{
			_out_deliveries = sources;

			string[] codes = new List<string>(
								from x in sources
								select x.Codigo
								).ToArray();

			SourceCodes_TB.Text = String.Join(", ", codes);

			_config.SourceEntityList = _out_deliveries;
		}

		private void SetSourceInvoices(List<OutputInvoiceInfo> sources)
		{
			_out_invoices  = sources;

			string[] codes = new List<string>(
										from x in _out_invoices
										select x.Codigo
										).ToArray();

			SourceCodes_TB.Text = String.Join(", ", codes);

			_config.SourceEntityList = _out_invoices;
		}

		#endregion

        #region Actions

        public void DoSubmit() { SubmitAction(); }

        protected override void SubmitAction()
        {
			if (!GetSettings())
			{
				_action_result = DialogResult.Ignore;
				return;
			}

			PgMng.Reset(12, 1, Resources.Messages.INICIANDO_EXPORTACION, this);
            
            try
            {
				_exporter = ExporterMng.GetExporter(_config);
									
				PgMng.Grow();
				if (SourceOutDelivery_RB.Checked)
				{
					PgMng.Grow(Resources.Messages.EXPORTING_OUTPUT_DELIVERIES);
					_exporter.Export();
				}

				PgMng.Grow();
				if (SourceOutInvoice_RB.Checked)
				{
					PgMng.Grow(Resources.Messages.EXPORTING_OUTPUT_INVOICES);
					_exporter.Export();
				}

				_exporter.Close();

				PgMng.FillUp();

				ProgressInfoMng.ShowInfo(Resources.Messages.EXPORTACION_SUCCESS);

				/*if (ProgressInfoMng.ShowQuestion(Resources.Messages.EXPORTACION_SUCCESS) == DialogResult.Yes)
				{
					RegistroViewForm form = new RegistroViewForm(_exporter.Registry.Oid, this);
					form.ShowDialog(this);
				}	*/

                _action_result = DialogResult.OK;
            }
			catch (iQException ex)
			{
				PgMng.FillUp();
				PgMng.ShowInfoException(ex);

				_exporter.Close();
				_action_result = DialogResult.Ignore;

				if (ex.Args != null) 
					OpenForm((ETipoEntidad)ex.Args[0], (long)ex.Args[1]);
			} 
            catch (Exception ex)
            {
				PgMng.FillUp();
				PgMng.ShowInfoException(ex);

				_exporter.Close();
                _action_result = DialogResult.Ignore;
            }
        }

		protected void SelectClientAction(ESchema schemaType)
		{
			if (schemaType == ESchema.Source)
			{
				ClientSelectForm form = new ClientSelectForm(this, EEstado.Active);

				if (form.ShowDialog(this) == DialogResult.OK)
				{
					SetSourceClient(form.Selected as ClienteInfo);
				}
			}
			else 
			{
				if (_destination_company == null) 
				{
					ProgressInfoMng.ShowInfo(Resources.Messages.NO_COMPANY_SELECTED);
					return;
				}

				_source_company = AppContext.ActiveSchema as CompanyInfo;

				AppContext.Principal.ChangeUserSchema(_destination_company);

				ClientSelectForm form = new ClientSelectForm(this, EEstado.Active);

				if (form.ShowDialog(this) == DialogResult.OK)
				{
					_destination_client = form.Selected as ClienteInfo;
					DestinationHolder_TB.Text = _destination_client.Nombre;

					_config.DestinationHolder = _destination_client;
				}

				AppContext.Principal.ChangeUserSchema(_source_company);
			}
		}

		protected void SelectCompanyAction()
		{	
			CompanySelectForm form = new CompanySelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetDestinationCompany(form.Selected as CompanyInfo);
			}
		}

		protected void SelectDeliveryAction()
		{
			OutputDeliveryList list = OutputDeliveryList.GetByClientList(_source_client.Oid);
			DeliverySelectForm form = new DeliverySelectForm(this, _source_client.ETipoEntidad, list);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetSourceDeliveries(form.Selected as List<OutputDeliveryInfo>);
			}
		}

		protected void SelectInvoiceAction()
		{
			OutputInvoiceList list = OutputInvoiceList.GetByClienteList(_source_client.Oid, false);
			InvoiceSelectForm form = new InvoiceSelectForm(this, list, ETipoFacturas.Todas);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetSourceInvoices(form.Selected as List<OutputInvoiceInfo>);
			}
		}

		private void SelectProviderAction(ESchema schemaType)
		{
			if (schemaType == ESchema.Source)
			{
				ProviderSelectForm form = new ProviderSelectForm(this, EEstado.Active);

				if (form.ShowDialog(this) == DialogResult.OK)
				{
					_source_provider = form.Selected as IAcreedorInfo;
					DestinationHolder_TB.Text = _source_provider.Nombre;
				}
			}
			else
			{
				if (_destination_company == null) 
				{
					ProgressInfoMng.ShowInfo(Resources.Messages.NO_COMPANY_SELECTED);
					return;
				}

				_source_company = AppContext.ActiveSchema as CompanyInfo;

				AppContext.Principal.ChangeUserSchema(_destination_company);

				ProviderSelectForm form = new ProviderSelectForm(this, EEstado.Active);

				if (form.ShowDialog(this) == DialogResult.OK)
				{
					_destination_provider = form.Selected as IAcreedorInfo;
					DestinationHolder_TB.Text = _destination_provider.Nombre;

					_config.DestinationHolder = _destination_provider;
				}

				AppContext.Principal.ChangeUserSchema(_source_company);
			}
		}

        #endregion

		#region Buttons

		private void Codes_BT_Click(object sender, EventArgs e)
		{
			if (SourceOutDelivery_RB.Checked)
				SelectDeliveryAction();
			else if (SourceOutInvoice_RB.Checked)
				SelectInvoiceAction();
		}

		private void Company_BT_Click(object sender, EventArgs e)
		{
			SelectCompanyAction();
		}

		private void DestinationHolder_BT_Click(object sender, EventArgs e)
		{
			if (DestinationOutDelivery_RB.Checked)
				SelectClientAction(ESchema.Destination);
			else if (DestinationOutInvoice_RB.Checked)
				SelectClientAction(ESchema.Destination);
			else if (DestinationInDelivery_RB.Checked)
				SelectProviderAction(ESchema.Destination);
			else if (DestinationInInvoice_RB.Checked)
				SelectProviderAction(ESchema.Destination);
		}

		private void SourceHolder_BT_Click(object sender, EventArgs e)
		{
			if (SourceOutDelivery_RB.Checked)
				SelectClientAction(ESchema.Source);
			else if (SourceOutInvoice_RB.Checked)
				SelectClientAction(ESchema.Source);
		}

		#endregion

		#region Events

		private void DestinationInDelivery_RB_CheckedChanged(object sender, EventArgs e)
		{
			DestinationHolder_TB.Text = (_destination_provider != null) ? _destination_provider.Nombre : string.Empty;
		}

		private void DestinationInInvoice_RB_CheckedChanged(object sender, EventArgs e)
		{
			DestinationHolder_TB.Text = (_destination_provider != null) ? _destination_provider.Nombre : string.Empty;
		}

		private void DestinationOutDelivery_RB_CheckedChanged(object sender, EventArgs e)
		{
			DestinationHolder_TB.Text = (_destination_client != null) ? _destination_client.Nombre : string.Empty;
		}

		private void DestinationOutInvoice_RB_CheckedChanged(object sender, EventArgs e)
		{
			DestinationHolder_TB.Text = (_destination_client != null) ? _destination_client.Nombre : string.Empty;
		}

		private void SourceOutDelivery_RB_CheckedChanged(object sender, EventArgs e)
		{
			DestinationInDelivery_RB.Enabled = SourceOutDelivery_RB.Checked;
			DestinationOutDelivery_RB.Enabled = SourceOutDelivery_RB.Checked;
			DestinationInInvoice_RB.Enabled = !SourceOutDelivery_RB.Checked;
			DestinationOutInvoice_RB.Enabled = !SourceOutDelivery_RB.Checked;

			DestinationInDelivery_RB.Checked = SourceOutDelivery_RB.Checked;
		}

		private void SourceOutInvoice_RB_CheckedChanged(object sender, EventArgs e)
		{
			DestinationInDelivery_RB.Enabled = !SourceOutInvoice_RB.Checked;
			DestinationOutDelivery_RB.Enabled = !SourceOutInvoice_RB.Checked;
			DestinationInInvoice_RB.Enabled = SourceOutInvoice_RB.Checked;
			DestinationOutInvoice_RB.Enabled = SourceOutInvoice_RB.Checked;

			DestinationInInvoice_RB.Checked = SourceOutInvoice_RB.Checked;
		}

        #endregion
	}
}

