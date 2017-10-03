using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class CobrosREAUIForm : CobrosREAForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 5; } }

        /// <summary>
        /// Se trata de la Cobro actual y que se va a editar.
        /// </summary>
        public override ChargeSummary Resumen { get { return _resumen; } set { _resumen = value; } }

        #endregion

        #region Factory Methods

        public CobrosREAUIForm(Form parent)
            : this(null, parent) { }

        public CobrosREAUIForm(ChargeInfo cobro, Form parent)
            : base(parent)
        {
            InitializeComponent();
            SetFormData();
        }
        
        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            Datos_Cobros.DataSource = _cobros;
            PgMng.Grow();

            Datos_Resumen.DataSource = _resumen;

            base.RefreshMainData();
        }
		
		public override void RefreshSecondaryData()
		{
            _facturas_cliente = FacREAList.GetListByCobroAndPendientes(0);
			PgMng.Grow(string.Empty, "Facturas de REA");
		}

        protected void Select(ChargeInfo cobro)
        {
            if (cobro == null) return;
            int foundIndex = Datos_Cobros.IndexOf(cobro);
            Datos_Cobros.Position = foundIndex;
        }

        #endregion

        #region Business Methods

        protected override void UpdateFacturasCobro()
        {
            if (Datos_Cobros.Current == null)
            {
                Datos_Facturas.DataSource = null;
                return;
            }

            ChargeInfo cobro = (ChargeInfo)Datos_Cobros.Current;

            List<FacREAInfo> lista = new List<FacREAInfo>();

            foreach (CobroREAInfo cf in cobro.CobroREAs)
            {
                FacREAInfo info = _facturas_cliente.GetItem(cf.OidExpedienteREA);
                if (info != null)
                    lista.Add(info);
            }

            Datos_Facturas.DataSource = FacREAList.GetList(lista);
            
            SetUnlinkedGridValues(LineasFomento_DGW.Name);
        }

		protected override void UpdateFacturasPendientes()
		{
            Datos_FacPendientes.DataSource = FacREAList.GetNoCobradas();
			SetUnlinkedGridValues(Pendientes_DGW.Name);
		}

		protected void ChangeState(EEstado estado)
		{
			if (Cobro == null)
			{
				PgMng.ShowInfoException(Face.Resources.Messages.NO_SELECTED);
				return;
			}

			if (Cobro.EEstado == EEstado.Anulado)
			{
				PgMng.ShowInfoException(Face.Resources.Messages.ITEM_ANULADO_NO_EDIT);
				return;
			}

			switch (estado)
			{
				case EEstado.Anulado:
					{
						if (Cobro.EEstado == EEstado.Contabilizado)
						{
							PgMng.ShowInfoException(Library.Common.Resources.Messages.NULL_CONTABILIZADO_NOT_ALLOWED);
							return;
						}

						if (Cobro.EMedioPago == EMedioPago.Efectivo)
						{
                            CashLineInfo linea = CashLineInfo.GetByCobro(Cobro.Oid);
							if ((linea.Oid != 0) && (linea.OidCierre != 0) && (linea.EEstado != EEstado.Anulado))
							{
								PgMng.ShowInfoException(String.Format(Resources.Messages.CIERRE_CAJA_COBRO, linea.Codigo, linea.Fecha));
								return;
							}
						}

						if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.NULL_CONFIRM) != DialogResult.Yes)
						{
							return;
						}
					}
					break;
			}

            Charge c = Library.Invoice.Charge.Get(Cobro.Oid, false);
            c.ChangeEstado(estado);
            c.Save();

			RefreshAction();
		}

        #endregion

        #region Actions

        protected override void SaveAction()
        {
            _action_result = DialogResult.OK;
        }

		protected override void RefreshAction()
		{
			ChargeInfo current = Cobro;

			Cobros_DGW.Refresh();
			UpdateFacturasPendientes();
			UpdateFacturasCobro();

			Datos_Resumen.ResetBindings(false);

			SetGridColors(Cobros_DGW.Name);
			SetGridColors(LineasFomento_DGW.Name);

			Select(current);
		}

		protected override void NewCobroAction()
		{
			REAChargeAddForm form = new REAChargeAddForm(this);
			form.ShowDialog(this);

			RefreshAction();
		}
		protected override void EditCobroAction()
        {
            if (Cobro == null)
            {
                PgMng.ShowInfoException(Face.Resources.Messages.NO_SELECTED);
                return;
            }

			if (Cobro.EEstado == EEstado.Anulado)
			{
				PgMng.ShowInfoException(Face.Resources.Messages.ITEM_ANULADO_NO_EDIT);
				return;
			}

            REAChargeEditForm form = new REAChargeEditForm(Cobro.Oid, this);
            form.ShowDialog(this);

			RefreshAction();  
        }
		protected override void ViewCobroAction()
		{
			if (Cobro == null)
			{
				PgMng.ShowInfoException(Face.Resources.Messages.NO_SELECTED);
				return;
			}

            CobroREAViewForm form = new CobroREAViewForm(Cobro.Oid, true, this);
			form.ShowDialog(this);
		}
		protected override void DeleteCobroAction()
        {
			ChangeState(EEstado.Anulado);
        }
		protected override void UnlockCobroAction()
		{
			ChangeState(EEstado.Abierto);
		}
		protected override void LockCobroAction()
		{
			ChangeState(EEstado.Contabilizado);
		}
		protected override void PrintCobroAction()
        {
            if (Cobro == null) return;

            CobroReportMng reportMng = new CobroReportMng(AppContext.ActiveSchema);
            ReportViewer.SetReport(reportMng.GetDetallesCobroREAIndividualReport(Cobro, Datos_Facturas.DataSource as FacREAList));
            ReportViewer.ShowDialog();
        }

        #endregion
    }
}