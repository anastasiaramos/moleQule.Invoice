using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Web.Mail;
using System.Net.Mail;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

using Csla;
using moleQule.Face.Hipatia;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;
using moleQule.Library.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
	public partial class InvoiceMngForm : InvoiceMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "FacturaMngForm";
		public static Type Type { get { return typeof(InvoiceMngForm); } }
        public override Type EntityType { get { return typeof(OutputInvoice); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected OutputInvoice _entity;
		protected ETipoFacturas _tipo;

		#endregion

		#region Factory Methods

		public InvoiceMngForm()
			: this(null, ETipoFacturas.Todas) { }

		public InvoiceMngForm(Form parent, ETipoFacturas tipo)
			: this(false, parent, tipo) { }

		public InvoiceMngForm(bool isModal, Form parent, ETipoFacturas tipo)
			: this(isModal, parent, null, tipo) { }

		public InvoiceMngForm(bool isModal, Form parent, OutputInvoiceList list, ETipoFacturas tipo)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			Datos.DataSource = OutputInvoiceList.NewList().GetSortedList();
			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;

            _tipo = tipo;

            SetView(molView.Normal);
		}

		#endregion

		#region Authorization

		protected override void ActivateAction(molAction action, bool state)
		{
			if (EntityType == null) return;

			switch (action)
			{
				case molAction.ChangeStateContabilizado:

					if ((AppContext.User != null) && (state))
						base.ActivateAction(action, Library.AutorizationRulesControler.CanEditObject(Library.Invoice.Resources.SecureItems.CUENTA_CONTABLE));
					else
						base.ActivateAction(action, state);

					break;

				default:
					base.ActivateAction(action, state);
					break;
			}
		}

		#endregion

		#region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Cliente.Tag = 0.4;
			Observaciones.Tag = 0.6;

			cols.Add(Cliente);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{		
			base.FormatControls();

			SetActionStyle(molAction.CustomAction1, Resources.Labels.CLIENTE_TI, Properties.Resources.cliente);
			SetActionStyle(molAction.CustomAction2, Resources.Labels.COBROS_TI, Properties.Resources.cobro);
			SetActionStyle(molAction.CustomAction3, Resources.Labels.INVOICE_CHARGES_TI, Properties.Resources.cobro_factura_emitida.ToBitmap());
			
			SetColumnActive(ControlsMng.GetColumn(Tabla, Cliente.DataPropertyName));
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (!row.Displayed) return;
			if (row.IsNewRow) return;

			OutputInvoiceInfo item = row.DataBoundItem as OutputInvoiceInfo;

			Tabla.SuspendLayout();

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);

			if (item.EEstadoCobro == EEstado.DudosoCobro)
			{
				row.Cells[DiasTranscurridos.Name].Style = Face.Common.ControlTools.Instance.PendienteStyleE;
			}
			else
			{
				if (item.Cobrada) return;
				/*{
					row.Cells[DiasTranscurridos.Name].Style = Face.Common.ControlTools.Instance.CobradoStyle;
				}*/
				else if (0 <= item.DiasTranscurridos && item.DiasTranscurridos < 15)
				{
					row.Cells[DiasTranscurridos.Name].Style = Face.Common.ControlTools.Instance.PendienteStyleA;
				}
				else if (15 <= item.DiasTranscurridos && item.DiasTranscurridos < 31)
				{
					row.Cells[DiasTranscurridos.Name].Style = Face.Common.ControlTools.Instance.PendienteStyleB;
				}
				else if (31 <= item.DiasTranscurridos && item.DiasTranscurridos < 45)
				{
					row.Cells[DiasTranscurridos.Name].Style = Face.Common.ControlTools.Instance.PendienteStyleC;
				}
				else if (45 <= item.DiasTranscurridos && item.DiasTranscurridos < 60)
				{
					row.Cells[DiasTranscurridos.Name].Style = Face.Common.ControlTools.Instance.PendienteStyleD;
				}
				else
				{
					row.Cells[DiasTranscurridos.Name].Style = Face.Common.ControlTools.Instance.PendienteStyleE;
				}
			}

			Tabla.ResumeLayout();
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateEmitido);
					HideAction(molAction.ChangeStateContabilizado);
					ShowAction(molAction.ShowDocuments);
					HideAction(molAction.PrintDetail);
					HideAction(molAction.ExportPDF);
					//HideAction(molAction.EmailLink);
					HideAction(molAction.EmailPDF);
					HideAction(molAction.CustomAction1);
					HideAction(molAction.CustomAction2);
					HideAction(molAction.CustomAction3);

					break;

				case molView.Normal:

					ShowAction(molAction.Unlock);
					ShowAction(molAction.ChangeStateEmitido);
					ShowAction(molAction.ChangeStateContabilizado);
					ShowAction(molAction.ShowDocuments);
					ShowAction(molAction.PrintDetail);
					ShowAction(molAction.ExportPDF);
					//ShowAction(molAction.EmailLink);
					ShowAction(molAction.EmailPDF);
					ShowAction(molAction.CustomAction1);
					ShowAction(molAction.CustomAction2);
					ShowAction(molAction.CustomAction3);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "FacturaMngForm::RefreshMainData INI");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					switch (_tipo)
					{
						case ETipoFacturas.Todas:
							if (Library.Common.ModulePrincipal.GetUseActiveYear())
								List = OutputInvoiceList.GetList(Library.Common.ModulePrincipal.GetActiveYear().Year, false);
							else
								List = OutputInvoiceList.GetList(false);
							break;

						case ETipoFacturas.Cobradas:
							if (Library.Common.ModulePrincipal.GetUseActiveYear())
								List = OutputInvoiceList.GetCobradasList(Library.Common.ModulePrincipal.GetActiveYear().Year, false);
							else
								List = OutputInvoiceList.GetCobradasList(false);
							break;

						case ETipoFacturas.Pendientes:
							if (Library.Common.ModulePrincipal.GetUseActiveYear())
								List = OutputInvoiceList.GetNoCobradasList(Library.Common.ModulePrincipal.GetActiveYear().Year, false);
							else
								List = OutputInvoiceList.GetNoCobradasList(false);
							break;
					}
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}

			PgMng.Grow(string.Empty, "FacturaMngForm::RefreshMainData END");
		}

		public override void UpdateList()
		{
			switch (_current_action)
			{
				case molAction.Add:
				case molAction.Copy:
					if (_entity == null) return;
					List.AddItem(_entity.GetInfo(false));
					if (FilterType == IFilterType.Filter)
					{
						OutputInvoiceList listA = OutputInvoiceList.GetList((SortedBindingList<OutputInvoiceInfo>)_filter_results);
						listA.AddItem(_entity.GetInfo(false));
						_filter_results = listA.GetSortedList();
					}
					break;

				case molAction.Edit:
				case molAction.ChangeStateContabilizado:
				case molAction.ChangeStateEmitido:
				case molAction.Unlock:
				case molAction.PrintDetail:
				case molAction.ExportPDF:
				case molAction.EmailPDF:
					if (_entity == null) return;
					ActiveItem.CopyFrom(_entity);
					break;

				case molAction.Delete:
					if (ActiveItem == null) return;
					List.RemoveItem(ActiveOID);
					if (FilterType == IFilterType.Filter)
					{
						OutputInvoiceList listD = OutputInvoiceList.GetList((SortedBindingList<OutputInvoiceInfo>)_filter_results);
						listD.RemoveItem(ActiveOID);
						_filter_results = listD.GetSortedList();
					}
					break;
			}

			RefreshSources();
			if (_entity != null) Select(_entity.Oid);
			_entity = null;
		}

		#endregion

		#region Actions

		public override void OpenAddForm()
		{
			InvoiceAddForm form = new InvoiceAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public void OpenAddForm(ClienteInfo cliente, OutputDeliveryInfo delivery)
		{
			InvoiceAddForm form = new InvoiceAddForm(cliente, delivery, this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()	{ AddForm(new InvoiceViewForm(ActiveOID, this)); }

		public override void OpenEditForm()
		{
			try
			{
				Library.Common.EntityBase.CheckEditAllowedEstado(ActiveItem.EEstado, EEstado.Abierto);
			}
			catch (iQException ex)
			{
				PgMng.ShowInfoException(ex);
				_action_result = DialogResult.Ignore;
				return;
			}

			InvoiceEditForm form = new InvoiceEditForm(ActiveOID, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
				_action_result = DialogResult.OK;
			}
		}

		public override void DeleteAction()
		{
            OutputInvoice.Delete(ActiveOID);
			_action_result = DialogResult.OK;
		}

		public override void ShowDocumentsAction()
		{
			try
			{
				AgenteInfo agent = AgenteInfo.Get(ActiveItem.TipoEntidad, ActiveItem);
				AgenteEditForm form = new AgenteEditForm(ActiveItem.TipoEntidad, ActiveItem, this);
				AddForm(form);
			}
			catch (HipatiaException ex)
			{
				if (ex.Code == HipatiaCode.NO_AGENTE)
				{
					AgenteAddForm form = new AgenteAddForm(ActiveItem.TipoEntidad, ActiveItem, this);
					AddForm(form);
				}
			}
		}

		public override void ChangeStateAction(EEstadoItem estado)
		{
            _entity = OutputInvoice.ChangeEstado(ActiveOID, Library.Common.EnumConvert.ToEEstado(estado));

			_action_result = DialogResult.OK;
		}

		public override void UnlockAction() { ChangeStateAction(EEstadoItem.Unlock); }

		public override void CustomAction1() { ShowClienteAction(); }
		public override void CustomAction2() { ShowClientChargesAction(); }
		public override void CustomAction3() { ShowInvoiceChargesAction(); }

		public virtual void ShowClienteAction()
		{
			ClientEditForm form = new ClientEditForm(ActiveItem.OidCliente, this);
			form.ShowDialog(this);
		}

		public virtual void ShowInvoiceChargesAction()
		{
			InvoiceChargesForm form = new InvoiceChargesForm(ActiveItem, this);
			form.ShowDialog(this);
		}

		public virtual void ShowClientChargesAction()
		{
			ClienteInfo cliente = ClienteInfo.Get(ActiveItem.OidCliente, true);
			ChargeSummary item = ChargeSummary.Get(cliente);
			CobroEditForm form = new CobroEditForm(cliente.Oid, item, null, this);
			form.ShowDialog(this);
		}

		#endregion

		#region Print

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

            OutputInvoiceReportMng reportMng = new OutputInvoiceReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);
			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

			ReportClass report = reportMng.GetListReport(OutputInvoiceList.GetList(Datos.DataSource as IList<OutputInvoiceInfo>),
															SerieList.GetList(false));
			PgMng.FillUp();

			ShowReport(report);
		}

		public override void PrintDetailAction()
		{
			if (ActiveItem == null) return;

			PgMng.Reset(6, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

            OutputInvoiceReportMng reportMng = new OutputInvoiceReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);

			SerieInfo serie = SerieInfo.Get(ActiveItem.OidSerie, false);
			PgMng.Grow();

			ClienteInfo client = ClienteInfo.Get(ActiveItem.OidCliente, false);
			PgMng.Grow();

			TransporterInfo transporter = TransporterInfo.Get(ActiveItem.OidTransportista, ETipoAcreedor.TransportistaDestino, false);
			PgMng.Grow();

			FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();

			conf.nota = (client.OidImpuesto == 1) ? Library.Invoice.Resources.Messages.NOTA_EXENTO_IGIC : string.Empty;
			conf.nota += (conf.nota != string.Empty) ? Environment.NewLine : string.Empty;
			conf.nota += (ActiveItem.Nota ? serie.Cabecera : "");
			conf.cuenta_bancaria = ActiveItem.CuentaBancaria;
			PgMng.Grow();

			OutputInvoiceInfo item = OutputInvoiceInfo.Get(ActiveOID, true);
            PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

            ReportClass report = reportMng.GetDetailReport(item, serie, client, transporter, conf);
            PgMng.FillUp();

            if (report != null)
            {
                if (SettingsMng.Instance.GetUseDefaultPrinter())
                {
                    int n_copias = SettingsMng.Instance.GetDefaultNCopies();
                    PrintReport(report, n_copias);
                }
                else
                    ShowReport(report);

                if (item.EEstado == EEstado.Abierto)
                    ChangeStateAction(EEstadoItem.Emitido);
            }
		}

		public override void ExportPDFAction()
		{
			if (ActiveItem == null) return;

			try
			{
				PgMng.Reset(9, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

                OutputInvoiceReportMng reportMng = new OutputInvoiceReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);

				SerieInfo serie = SerieInfo.Get(ActiveItem.OidSerie, false);
				PgMng.Grow();

				ClienteInfo client = ClienteInfo.Get(ActiveItem.OidCliente, false);
				PgMng.Grow();

				TransporterInfo transporter = TransporterInfo.Get(ActiveItem.OidTransportista, ETipoAcreedor.TransportistaDestino, false);
				PgMng.Grow();

				FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();

				conf.nota = (client.OidImpuesto == 1) ? Library.Invoice.Resources.Messages.NOTA_EXENTO_IGIC : string.Empty;
				conf.nota += Environment.NewLine + (ActiveItem.Nota ? serie.Cabecera : "");
				conf.cuenta_bancaria = ActiveItem.CuentaBancaria;
				PgMng.Grow();

				OutputInvoiceInfo item = OutputInvoiceInfo.Get(ActiveOID, true);
				PgMng.Grow();

				ReportClass report = reportMng.GetDetailReport(item, serie, client, transporter, conf);
				PgMng.Grow();

				ExportPDF(report, ActiveItem.FileName);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally 
			{ 
				PgMng.FillUp(); 
			}
		}

		public override void EmailLinkAction()
		{
			if (ActiveItem == null) return;

			PgMng.Reset(5, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

			ClienteInfo cliente = ClienteInfo.Get(ActiveItem.OidCliente, false);
			PgMng.Grow();

			CompanyInfo empresa = CompanyInfo.Get(AppContext.ActiveSchema.Oid);
			PgMng.Grow();

			MailParams mail = new MailParams();

			string url = empresa.Url + String.Format(Library.Invoice.ModuleController.GetFacturaWebScript(), ActiveItem.Link);
			mail.To = cliente.Email;
			mail.Body = String.Format(Library.Invoice.Resources.Messages.FACTURA_EMAIL_LINK_BODY, url, empresa.Name);
			mail.Subject = Library.Invoice.Resources.Messages.FACTURA_EMAIL_SUBJECT;

			try
			{
				PgMng.Grow(moleQule.Face.Resources.Messages.OPENING_EMAIL_CLIENT, string.Empty);

				EMailSender.MailTo(mail);

				OutputInvoiceInfo item = OutputInvoiceInfo.Get(ActiveOID, true);
				PgMng.Grow();

				if (item.EEstado == EEstado.Abierto)
					ChangeStateAction(EEstadoItem.Emitido);
			}
			catch
			{
				PgMng.ShowInfoException(moleQule.Face.Resources.Messages.NO_EMAIL_CLIENT);
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		public override void EmailPDFAction()
		{
			if (ActiveItem == null) return;

			PgMng.Reset(10, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

			SerieInfo serie = SerieInfo.Get(ActiveItem.OidSerie, false);
			PgMng.Grow();

			ClienteInfo client = ClienteInfo.Get(ActiveItem.OidCliente, false);
			PgMng.Grow();

			TransporterInfo transporter = TransporterInfo.Get(ActiveItem.OidTransportista, ETipoAcreedor.TransportistaDestino, false);
			PgMng.Grow();

			CompanyInfo company = CompanyInfo.Get(AppContext.ActiveSchema.Oid);
			PgMng.Grow();

			FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();

			conf.nota = (client.OidImpuesto == 1) ? Library.Invoice.Resources.Messages.NOTA_EXENTO_IGIC : string.Empty;
			conf.nota += Environment.NewLine + (ActiveItem.Nota ? serie.Cabecera : "");
			conf.cuenta_bancaria = ActiveItem.CuentaBancaria;
			PgMng.Grow();

			OutputInvoiceInfo item = OutputInvoiceInfo.Get(ActiveOID, true);
			PgMng.Grow();

            OutputInvoiceReportMng reportMng = new OutputInvoiceReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);
			ReportClass report = reportMng.GetDetailReport(item, serie, client, transporter, conf);
			PgMng.Grow();

			if (report != null)
			{
				ExportOptions options = new ExportOptions();
				DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();

				string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				fileName += "\\" + ActiveItem.FileName;

				PgMng.Grow(String.Format(Face.Resources.Messages.EXPORTING_PDF, fileName), string.Empty);

				diskFileDestinationOptions.DiskFileName = fileName;
				options.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
				options.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
				options.ExportDestinationOptions = diskFileDestinationOptions;

				PgMng.Grow();

				report.Export(options);

				PgMng.Grow(Face.Resources.Messages.SENDING_EMAIL);
				
				System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

				mail.To.Add(new MailAddress(client.Email, client.Nombre));
				mail.From = new MailAddress(SettingsMng.Instance.GetSMTPMail(), company.Name);
				mail.Body = String.Format(Library.Invoice.Resources.Messages.FACTURA_EMAIL_ATTACHMENT_BODY, company.Name);
				mail.Subject = Library.Invoice.Resources.Messages.FACTURA_EMAIL_SUBJECT;
				mail.Attachments.Add(new Attachment(fileName));

				try
				{
					PgMng.Grow(moleQule.Face.Resources.Messages.SENDING_EMAIL, string.Empty);

					EMailClient.Instance.SmtpCliente.Send(mail);

					if (item.EEstado == EEstado.Abierto)
						ChangeStateAction(EEstadoItem.Emitido);

					PgMng.ShowInfoException("Mensaje enviado con éxito");
				}
				catch (Exception ex)
				{
					PgMng.ShowInfoException(ex.Message + Environment.NewLine + Environment.NewLine + moleQule.Library.Resources.Errors.SMTP_SETTINGS);
				}
				finally
				{
					mail.Dispose();
					PgMng.FillUp();
				}
			}
			else
			{
				PgMng.ShowInfoException(Face.Resources.Messages.NO_DATA_REPORTS);

				PgMng.FillUp();
			}
		}

		/*public override void EmailPDFAction()
		{
			if (ActiveItem == null) return;

			PgMng.Reset(9, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

			SerieInfo serie = SerieInfo.Get(ActiveItem.OidSerie, false);
			PgMng.Grow();
			ClienteInfo cliente = ClienteInfo.Get(ActiveItem.OidCliente, false);
			PgMng.Grow();
			EmpresaInfo empresa = EmpresaInfo.Get(AppContext.ActiveSchema.Oid);
			PgMng.Grow();

			FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();

			conf.nota = (cliente.OidImpuesto == 1) ? Resources.Messages.NOTA_EXENTO_IGIC : string.Empty;
			conf.nota += Environment.NewLine + (ActiveItem.Nota ? serie.Cabecera : "");
			conf.cuenta_bancaria = ActiveItem.CuentaBancaria;
			PgMng.Grow();

			OutputInvoiceInfo item = OutputInvoiceInfo.Get(ActiveOID, true);
			PgMng.Grow();

			FacturaReportMng reportMng = new FacturaReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);
			ReportClass report = reportMng.GetFacturaReport(item, conf);
			PgMng.Grow();

			if (report != null)
			{
				ExportOptions options = new ExportOptions();
				DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();

				string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				fileName += "\\" + ActiveItem.FileName;

				PgMng.Grow(String.Format(Face.Resources.Messages.EXPORTING_PDF, fileName), string.Empty);

				diskFileDestinationOptions.DiskFileName = fileName;
				options.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
				options.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
				options.ExportDestinationOptions = diskFileDestinationOptions;

				PgMng.Grow();

				report.Export(options);

				MailParams mail = new MailParams();

				string url = empresa.Url + String.Format(Library.Invoice.ModuleController.GetFacturaWebScript(), ActiveItem.Link);
				mail.To = cliente.Email;
				mail.Body = String.Format(Resources.Messages.FACTURA_EMAIL_ATTACHMENT_BODY, empresa.Name);
				mail.Subject = Resources.Messages.FACTURA_EMAIL_SUBJECT;
				mail.AttachmentPath = fileName;

				try
				{
					PgMng.Grow(moleQule.Face.Resources.Messages.OPENING_EMAIL_CLIENT, string.Empty);

					EMailSender.MailTo(mail);

					if (item.EEstado == EEstado.Abierto)
						ChangeStateAction(EEstadoItem.Emitido);
				}
				catch
				{
					PgMng.ShowInfoException(moleQule.Face.Resources.Messages.NO_EMAIL_CLIENT);
				}
				finally
				{
					PgMng.FillUp();
				}
			}
			else
			{
				PgMng.ShowInfoException(Face.Resources.Messages.NO_DATA_REPORTS);

				PgMng.FillUp();
			}
		}*/

		#endregion
	}

    public partial class InvoiceMngBaseForm : Skin08.EntityMngSkinForm<OutputInvoiceList, OutputInvoiceInfo, OutputInvoice>
	{
		public InvoiceMngBaseForm()
			: this(false, null, null) { }

		public InvoiceMngBaseForm(bool isModal, Form parent, OutputInvoiceList lista)
			: base(isModal, parent, lista) { }
	}

	public class InvoiceAllMngForm : InvoiceMngForm
	{
		public new const string ID = "InvoiceAllMngForm";
		public new static Type Type { get { return typeof(InvoiceAllMngForm); } }

		public InvoiceAllMngForm(Form parent)
			: base(parent, ETipoFacturas.Todas)
		{
			this.Text = Resources.Labels.FACTURA_TODAS;
		}
	}

	public class InvoiceDueMngForm : InvoiceMngForm
	{
		public new const string ID = "InvoiceDueMngForm";
		public new static Type Type { get { return typeof(InvoiceDueMngForm); } }

		public InvoiceDueMngForm(Form parent)
			: base(parent, ETipoFacturas.Pendientes)
		{
			this.Text = Resources.Labels.FACTURA_PENDIENTES;
		}
	}

	public class InvoiceChargedMngForm : InvoiceMngForm
	{
		public new const string ID = "InvoiceChargedMngForm";
		public new static Type Type { get { return typeof(InvoiceChargedMngForm); } }

		public InvoiceChargedMngForm(Form parent)
			: base(parent, ETipoFacturas.Cobradas)
		{
			this.Text = Resources.Labels.FACTURA_COBRADAS;
		}
	}


}

