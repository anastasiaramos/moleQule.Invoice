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
    public partial class CobroUIForm : CobroForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 5; } }

        /// <summary>
        /// Se trata de la Cobro actual y que se va a editar.
        /// </summary>
        protected Cliente _entity;

        public override Cliente Entity { get { return _entity; } set { _entity = value; } }
        public override ChargeSummary Resumen { get { return _resumen; } set { _resumen = value; } }

        #endregion

        #region Factory Methods

        public CobroUIForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
        }

        public CobroUIForm(Cliente Cliente, Form parent)
            : base(parent)
        {
            InitializeComponent();
            _entity = Cliente.Clone();
            _entity.BeginEdit();
            SetFormData();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            Cliente temp = _entity.Clone();
            temp.ApplyEdit();

            // do the save
            try
            {
                _entity = temp.Save();
                _entity.ApplyEdit();

                return true;
            }
            catch (Exception ex)
            {
				PgMng.ShowInfoException(ex);
                return false;
            }
            finally
            {
                this.Datos.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            _series = SerieList.GetList(false);
            PgMng.Grow(string.Empty, "Series");

            Datos.DataSource = _entity;
            PgMng.Grow();

            Datos_Cobros.DataSource = _entity.Cobros;
            PgMng.Grow();

            Datos_Resumen.DataSource = _resumen;

            base.RefreshMainData();
        }
		
		public override void RefreshSecondaryData()
		{
			_facturas_cliente = OutputInvoiceList.GetByClienteList(_entity.Oid, false);
			PgMng.Grow(string.Empty, "Facturas del Cliente");
		}

        protected void Select(Charge charge)
        {
            if (charge == null) return;
            int foundIndex = Datos_Cobros.IndexOf(charge);
            Datos_Cobros.Position = foundIndex;
        }
        public void Select(ChargeInfo charge)
        {
            if (charge == null) return;
            Charge item = _entity.Cobros.GetItem(charge.Oid);
            Select(item);            
        }

        #endregion

        #region Business Methods

        protected override void UpdateFacturasCobro()
        {
            if (Datos_Cobros.Current == null)
            {
                Datos_FCobro.DataSource = null;
                return;
            }

            Charge cobro = (Charge)Datos_Cobros.Current;

            List<OutputInvoiceInfo> lista = new List<OutputInvoiceInfo>();

            foreach (CobroFactura cf in cobro.CobroFacturas)
            {
                OutputInvoiceInfo info = _facturas_cliente.GetItem(cf.OidFactura);
                if (info != null)
                    lista.Add(info);
            }

            Datos_FCobro.DataSource = OutputInvoiceList.GetChildList(lista);
            
            SetUnlinkedGridValues(Facturas_DGW.Name);
        }

		protected override void UpdateFacturasPendientes()
		{
			Datos_FPendientes.DataSource = OutputInvoiceList.GetNoCobradasByClienteList(_entity.Oid, false);
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

			_entity.Cobros.ChangeState(estado, Cobro, _entity);
			_entity.ApplyEdit();
			_entity.Save();
			_entity.BeginEdit();

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
            Charge current = Cobro;
            
            _facturas_cliente = OutputInvoiceList.GetByClienteList(_entity.Oid, false);
            Datos_Cobros.DataSource = _entity.Cobros.GetSortedList("IdCobro", ListSortDirection.Descending);
			Cobros_DGW.Refresh();
			UpdateFacturasPendientes();
			UpdateFacturasCobro();

			_resumen.Refresh(_entity);
			Datos_Resumen.ResetBindings(false);

			SetGridColors(Cobros_DGW.Name);
			SetGridColors(Facturas_DGW.Name);

			Select(current);
		}

		protected override void NewCobroAction()
		{
			CobroFacturaAddForm form = new CobroFacturaAddForm(_entity, this);
			form.ShowDialog(this);

			RefreshAction();
            Datos_Cobros.MoveFirst();

		}
		protected override void EditCobroAction()
        {
            bool locked = false;

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

            CobroFacturaEditForm form = new CobroFacturaEditForm(_entity, Cobro, locked, this);
            form.MedioPago_BT.Enabled = false;
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

			CobroFacturaEditForm form = new CobroFacturaEditForm(_entity, Cobro, false, this);
			form.SetReadOnly();
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

            ChargeInfo c;
            if (Datos_Cobros.Current.GetType().Equals("ChargeInfo"))
                c = (ChargeInfo)Datos_Cobros.Current;
            else
                c = Cobro.GetInfo();

            CobroReportMng reportMng = new CobroReportMng(AppContext.ActiveSchema);
            ReportViewer.SetReport(reportMng.GetDetallesCobroIndividualReport(c));
            ReportViewer.ShowDialog();
        }

        #endregion
    }
}

