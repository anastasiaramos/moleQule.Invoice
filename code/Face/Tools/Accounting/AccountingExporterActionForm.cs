using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Store;

namespace moleQule.Face.Invoice
{
    public partial class ExportarContabilidadActionForm : Skin01.ActionSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 4; } }

        public const string ID = "ExportarContabilidadActionForm";
        public static Type Type { get { return typeof(ExportarContabilidadActionForm); } }

        private SerieInfo _serie = null;
		private IContabilidadExporterMng _exporter;
		private ContabilidadConfig _config;

        #endregion

        #region Factory Methods

        public ExportarContabilidadActionForm() 
            : this(null) {}

		public ExportarContabilidadActionForm(Form parent)
			: base(true, parent) 
		{
            this.InitializeComponent();
            base.SetFormData();
        }

        #endregion

        #region Layout & Source

		public override void RefreshSecondaryData()
		{
            ETipoAcreedor[] providers = 
            { 
                ETipoAcreedor.Acreedor,
                ETipoAcreedor.Despachante,
                ETipoAcreedor.Empleado,
                ETipoAcreedor.Naviera,
                ETipoAcreedor.Proveedor,
                ETipoAcreedor.TransportistaDestino,
                ETipoAcreedor.TransportistaOrigen,
                ETipoAcreedor.Todos
            };
            Datos_TiposAcreedor.DataSource = Library.Common.EnumText<ETipoAcreedor>.GetList(providers);
			TipoAcreedor_CB.SelectedItem = (long)ETipoAcreedor.Todos;
			PgMng.Grow();

			Datos_MedioPago.DataSource = Library.Common.EnumText<EMedioPago>.GetList(false, false);
			//MedioPago_CB.SelectedValue = (long)EMedioPago.Todos;
            MedioPago_CLB.Items.Clear();
            foreach (ComboBoxSource combo in Datos_MedioPago.DataSource as ComboBoxList<EMedioPago>)
                MedioPago_CLB.Items.Add(combo.Texto, true);
			PgMng.Grow();

			EEstado[] list = { EEstado.Abierto, EEstado.Emitido, EEstado.Charged, EEstado.Solicitado, EEstado.Contabilizado, EEstado.Exportado, EEstado.NoAnulado };
			Datos_Estado.DataSource = Library.Common.EnumText<EEstado>.GetList(list);
			Estado_CB.SelectedValue = (long)EEstado.Abierto;
			PgMng.Grow();

			Datos_TipoPago.DataSource = Library.Store.EnumText<ETipoPago>.GetList(false);
			TipoPago_CB.SelectedItem = (long)ETipoPago.Todos;
			PgMng.Grow();

			Datos_TipoCobro.DataSource = Library.Invoice.EnumText<ETipoCobro>.GetList(false);
			TipoCobro_CB.SelectedItem = (long)ETipoCobro.Todos;
			PgMng.Grow();

            Datos_TipoAyuda.DataSource = Library.Invoice.EnumText<ETipoAyudaContabilidad>.GetList(false);
            TipoAyuda_CB.SelectedItem = (long)ETipoAyudaContabilidad.Todas;

			RutaSalida_TB.Text = Library.Invoice.ModulePrincipal.GetContabilidadFolder();
			CW_Diario_TB.Text = Library.Invoice.ModulePrincipal.GetJournalSetting();
			CW_AsientoInicial_TB.Text = Library.Invoice.ModulePrincipal.GetLastAsientoSetting();
			TF_Diario_TB.Text = Library.Invoice.ModulePrincipal.GetJournalTinforSetting();
			TF_AsientoInicial_TB.Text = Library.Invoice.ModulePrincipal.GetLastAsientoTinforSetting();
            TF_Centro_TB.Text = Library.Invoice.ModulePrincipal.GetCentroTrabajoTinforSetting();
            TF_Empresa_TB.Text = Library.Invoice.ModulePrincipal.GetEmpresaTinforSetting();
            CodigoEmpresaA3_TB.Text = Library.Invoice.ModulePrincipal.GetEmpresaA3Setting();

            ETipoExportacion tipo = Library.Invoice.ModulePrincipal.GetDefaultTipoExportacionSetting();

            switch (tipo)
            {
                case ETipoExportacion.ContaWin:
                    ContaWin_RB.Checked = true;
                    break;
                case ETipoExportacion.A3:
                    A3_RB.Checked = true;
                    break;
                case ETipoExportacion.Tinfor:
                    Tinfor_RB.Checked = true;
                    break;
            }

			FInicial_DTP.Checked = true;
			FFinal_DTP.Checked = true;

			FInicial_DTP.Value = DateAndTime.FirstDay(DateTime.Today.Month, DateTime.Today.Year);
			FFinal_DTP.Value = DateTime.Today;
		}

        #endregion

        #region Business Methods

        private bool GetSettings()
        {
            if (ContaWin_RB.Checked) 
			{
				_config.TipoExportacion = ETipoExportacion.ContaWin;
				_config.RutaSalida = RutaSalida_TB.Text;
				_config.AsientoInicial = CW_AsientoInicial_TB.Text;
				_config.Diario = CW_Diario_TB.Text;
			}
			if (A3_RB.Checked)
			{
				_config.TipoExportacion = ETipoExportacion.A3;
                _config.RutaSalida = RutaSalida_TB.Text;
                _config.Empresa = CodigoEmpresaA3_TB.Text;
			}
			else if (Tinfor_RB.Checked) 
			{
				_config.TipoExportacion = ETipoExportacion.Tinfor;
				_config.RutaSalida = RutaSalida_TB.Text;
				_config.AsientoInicial = TF_AsientoInicial_TB.Text;
				_config.Diario = TF_Diario_TB.Text;
				_config.Empresa = TF_Empresa_TB.Text;
				_config.CentroTrabajo = TF_Centro_TB.Text;

				if (_config.Empresa == string.Empty)
				{
					PgMng.ShowWarningException(Resources.Messages.NO_COMPANY_SELECTED);

					return false;
				}


				if (_config.CentroTrabajo == string.Empty)
				{
					PgMng.ShowWarningException(Resources.Messages.NO_CENTRO_SELECTED);

					return false;
				}				
			}

			return true;
        }

		private void OpenForm(ETipoEntidad entityType, long oid)
		{
			switch (entityType)
			{
				case ETipoEntidad.Cliente:
					{
						ClientEditForm form = new ClientEditForm(oid, this);
						form.ShowDialog();
					} break;

				case ETipoEntidad.CuentaBancaria:
					{
                        BankAccountEditForm form = new BankAccountEditForm(oid, this);
						form.ShowDialog();
					} break;

				case ETipoEntidad.Despachante:
					{
						DespachanteEditForm form = new DespachanteEditForm(oid, this);
						form.ShowDialog();
					} break;

				case ETipoEntidad.Empleado:
					{
                        EmployeeEditForm form = new EmployeeEditForm(oid, this);
                        form.ShowDialog();
					} break;

				case ETipoEntidad.Familia:
					{
                        FamiliaEditForm form = new FamiliaEditForm(oid, this);
                        form.ShowDialog();
					} break;

				case ETipoEntidad.Impuesto:
					{
						ImpuestoUIForm form = new ImpuestoUIForm(this);
						form.ShowDialog();
					} break;

				case ETipoEntidad.Naviera:
					{
						NavieraEditForm form = new NavieraEditForm(oid, this);
						form.ShowDialog();
					} break;

				case ETipoEntidad.Acreedor:
				case ETipoEntidad.Proveedor:
					{
						ProveedorEditForm form = new ProveedorEditForm(oid, moleQule.Library.Store.EnumConvert.ToETipoAcreedor(entityType), this);
						form.ShowDialog();
					} break;

				case ETipoEntidad.TipoGasto:
					{
						TipoGastoEditForm form = new TipoGastoEditForm(oid);
						form.ShowDialog();
					} break;

				case ETipoEntidad.TransportistaDestino:
				case ETipoEntidad.TransportistaOrigen:
					{
						TransporterEditForm form = new TransporterEditForm(oid, moleQule.Library.Store.EnumConvert.ToETipoAcreedor(entityType), this);
						form.ShowDialog();
					} break;
                case ETipoEntidad.Prestamo:
                    {
                        LoanEditForm form = new LoanEditForm(oid, this);
                        form.ShowDialog();
                    } break;
			}
		}

		private void SetPaymentType()
		{
			switch ((ETipoAcreedor)(long)TipoAcreedor_CB.SelectedValue)
			{ 
				case ETipoAcreedor.Acreedor:
				case ETipoAcreedor.Despachante:
				case ETipoAcreedor.Instructor:
				case ETipoAcreedor.Naviera:
				case ETipoAcreedor.Partner:
				case ETipoAcreedor.Proveedor:
				case ETipoAcreedor.TransportistaDestino:
				case ETipoAcreedor.TransportistaOrigen:
					switch ((ETipoPago)(long)TipoPago_CB.SelectedValue)
					{
						case ETipoPago.Todos:
						case ETipoPago.Gasto:
						case ETipoPago.Nomina:
						case ETipoPago.Prestamo:
							TipoPago_CB.SelectedValue = (long)ETipoPago.Factura;
							break;
					}
					break;

				case ETipoAcreedor.Empleado:
					TipoPago_CB.SelectedValue = (long)ETipoPago.Nomina;
					break;
			}
		}

		private void SetProviderType()
		{
			switch ((ETipoPago)(long)TipoPago_CB.SelectedValue)
			{
				case ETipoPago.Todos:
				case ETipoPago.Gasto:
				case ETipoPago.Prestamo:
					TipoAcreedor_CB.SelectedValue = (long)ETipoAcreedor.Todos;
					break;

				case ETipoPago.Nomina:
					TipoAcreedor_CB.SelectedValue = (long)ETipoAcreedor.Empleado;
					break;
			}
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
			Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions();
            conditions.Serie = _serie;
            conditions.FechaIni = FInicial_DTP.Value;
            conditions.FechaFin = FFinal_DTP.Value;
			conditions.TipoAcreedor = (ETipoAcreedor)(long)TipoAcreedor_CB.SelectedValue;
			//conditions.MedioPago = (EMedioPago)(long)MedioPago_CB.SelectedValue;
			conditions.Estado = (EEstado)(long)Estado_CB.SelectedValue;
			conditions.TipoPago = (ETipoPago)(long)TipoPago_CB.SelectedValue;
			conditions.TipoCobro = (ETipoCobro)(long)TipoCobro_CB.SelectedValue;
            conditions.TipoAyudas = (ETipoAyudaContabilidad)(long)TipoAyuda_CB.SelectedValue;

            conditions.MedioPagoList = new List<EMedioPago>();

            for(int i = 0; i<MedioPago_CLB.CheckedIndices.Count ; i++)
                conditions.MedioPagoList.Add((EMedioPago)MedioPago_CLB.CheckedIndices[i] + 1);
            
            try
            {
				_config.Conditions = conditions;
				_exporter = ContabilidadExporterMng.GetExporter(_config);
									
				PgMng.Grow();
				if (FRecibidas_CkB.Checked)
				{
					PgMng.Grow(Resources.Messages.EXPORTANDO_FACTURAS_RECIBIDAS);
					_exporter.Export(EElementoExportacion.FacturaRecibida);
				}

				PgMng.Grow();
				if (FEmitidas_CkB.Checked)
				{
					PgMng.Grow(Resources.Messages.EXPORTANDO_FACTURAS_EMITIDAS);
					_exporter.Export(EElementoExportacion.FacturaEmitida);
				}

				PgMng.Grow();
				if (Pagos_CkB.Checked)
				{
					PgMng.Grow(Resources.Messages.EXPORTANDO_PAGOS);
					_exporter.Export(EElementoExportacion.Pago);
				}

				PgMng.Grow();
				if (Cobros_CkB.Checked)
				{
					PgMng.Grow(Resources.Messages.EXPORTANDO_COBROS);
					_exporter.Export(EElementoExportacion.Cobro);
				}

				PgMng.Grow();
				if (Gastos_CkB.Checked)
				{
					PgMng.Grow(Resources.Messages.EXPORTANDO_GASTOS);
					_exporter.Export(EElementoExportacion.Gasto);
				}

				PgMng.Grow();
				if (Nominas_CkB.Checked)
				{
					PgMng.Grow(Resources.Messages.EXPORTANDO_NOMINAS);
					_exporter.Export(EElementoExportacion.Nomina);
				}

				PgMng.Grow();
				if (Ayudas_CkB.Checked)
				{
					PgMng.Grow(Resources.Messages.EXPORTANDO_AYUDAS);
					_exporter.Export(EElementoExportacion.Ayuda);
				}

                PgMng.Grow();
                if (Traspasos_CkB.Checked)
                {
                    PgMng.Grow(Resources.Messages.EXPORTANDO_TRASPASOS);
                    _exporter.Export(EElementoExportacion.Traspaso);
                }

                PgMng.Grow();
                if (Prestamos_CkB.Checked)
                {
                    PgMng.Grow(Resources.Messages.EXPORTANDO_PRESTAMOS);
                    _exporter.Export(EElementoExportacion.Prestamo);
                }

				_exporter.Close();

				PgMng.FillUp();

				if (ProgressInfoMng.ShowQuestion(Resources.Messages.EXPORTACION_SUCCESS) == DialogResult.Yes)
				{
					RegistroViewForm form = new RegistroViewForm(_exporter.Registro.Oid, this);
					form.ShowDialog(this);
				}	

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
        
        #endregion

		#region Buttons

		private void Cliente_BT_Click(object sender, EventArgs e)
		{
			ClientSelectForm form = new ClientSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_serie = form.Selected as SerieInfo;
				Serie_TB.Text = _serie.Nombre;
			}
		}

		private void Examinar_BT_Click(object sender, EventArgs e)
		{
			Browser.SelectedPath = RutaSalida_TB.Text;
			Browser.ShowDialog();
			RutaSalida_TB.Text = Browser.SelectedPath;
		}

		#endregion

		#region Events

		private void A3_RB_CheckedChanged(object sender, EventArgs e)
		{
			if (A3_RB.Checked)
				Settings_TC.SelectedTab = Settings_TC.TabPages[A3_TP.Name];
		}

		private void Ayudas_CkB_CheckedChanged(object sender, EventArgs e)
		{
			TipoAyuda_CB.Enabled = Ayudas_CkB.Checked;
		}

		private void Cobros_CkB_CheckedChanged(object sender, EventArgs e)
		{
			TipoCobro_CB.Enabled = Cobros_CkB.Checked;
		}

		private void ContaWin_RB_CheckedChanged(object sender, EventArgs e)
		{
			if (ContaWin_RB.Checked)
				Settings_TC.SelectedTab = Settings_TC.TabPages[ContaWin_TP.Name];
		}

		private void FRecibidas_CkB_CheckedChanged(object sender, EventArgs e)
		{
			Acreedor_GB.Enabled = (Pagos_CkB.Checked || FRecibidas_CkB.Checked);
			TipoPago_CB.Enabled = Pagos_CkB.Checked;
		}

		private void Tinfor_RB_CheckedChanged(object sender, EventArgs e)
		{
			if (Tinfor_RB.Checked)
				Settings_TC.SelectedTab = Settings_TC.TabPages[Tinfor_TP.Name];
		}

		private void TipoAcreedor_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetPaymentType();
		}

		private void TipoPago_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetProviderType();
		}

		private void TodosSerie_GB_CheckedChanged(object sender, EventArgs e)
		{
			Serie_BT.Enabled = !TodosSerie_CkB.Checked;
		}

		private void Pagos_CkB_CheckedChanged(object sender, EventArgs e)
		{
			Acreedor_GB.Enabled = (Pagos_CkB.Checked || FRecibidas_CkB.Checked);
			TipoPago_CB.Enabled = Pagos_CkB.Checked;
		}

        #endregion
	}
}