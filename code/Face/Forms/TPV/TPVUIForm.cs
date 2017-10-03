using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Store;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class TPVUIForm : TPVForm, IBackGroundLauncher
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata de la Ticket actual y que se va a editar.
        /// </summary>
        protected Ticket _entity = null;
        protected AlbaranTickets _albaranes_factura = null;
        protected ConceptoTickets _conceptos_factura = null;

        protected TransporterInfo _transporter = null;
        protected SerieInfo _serie = null;
		protected List<OutputDeliveryInfo> _albaranes = new List<OutputDeliveryInfo>();
		protected List<OutputDeliveryInfo> _results = new List<OutputDeliveryInfo>();

        public override Ticket Entity { get { return _entity; } set { _entity = value; } }
        public override TicketInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

		public TPVUIForm()
			: this((Form)null) {}

        public TPVUIForm(Form parent)
            : this(-1, parent) {}

        public TPVUIForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
        }

        public TPVUIForm(Ticket Ticket)
            : base(null)
        {
            InitializeComponent();
            _entity = Ticket.Clone();
            _entity.BeginEdit();
            SetFormData();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            // do the save
            try
            {
                Ticket temp = _entity.Clone();
                temp.ApplyEdit();
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

        #region Layout & Source

        public override void FormatControls()
        {
            Imprimir_Button.Text = "Aceptar e Imprimir";
			Serie_BT.Enabled = (_entity.ConceptoTickets.Count == 0);
			TPV_BT.Enabled = (_entity.EMedioPago == EMedioPago.Tarjeta);
            base.FormatControls();
        }

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            _albaranes_factura = _entity.AlbaranTickets.Clone();
            _conceptos_factura = _entity.ConceptoTickets.Clone();
            Datos_Concepto.DataSource = _entity.ConceptoTickets;
            PgMng.Grow();

            _albaranes = OutputDeliveryList.GetListByTicket(true, _entity.Oid).GetListInfo();
			PgMng.Grow();

            Fecha_DTP.Value = _entity.Fecha;

            base.RefreshMainData();
        }

        protected override void HideComponentes()
        {
            foreach (DataGridViewRow row in Conceptos_DGW.Rows)
                if ((row.DataBoundItem as ConceptoTicket).IsKitComponent)
                    row.Visible = false;
        }

        #endregion

        #region Validation & Format

        #endregion

		#region Business Methods

		protected void SetSerie(SerieInfo source, bool new_code)
		{
			if (source == null) return;

			_entity.OidSerie = source.Oid;
			_entity.NumeroSerie = source.Identificador;
			_entity.Serie = source.Nombre;
			Serie_TB.Text = source.Nombre;

			if (new_code) _entity.GetNewCode();
		}

		protected void SetMedioPago(EMedioPago selection)
		{
			switch (selection)
			{
				case EMedioPago.Efectivo:
					_entity.OidTpv = 0;
					_entity.TPV = string.Empty;
					TPV_BT.Enabled = false;
					break;

				case EMedioPago.Tarjeta:
					TPV_BT.Enabled = true;
					break;
			}
		}

		private bool AddAlbaran()
		{
			if (_entity.OidSerie == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_SERIE_SELECTED);
				return false;
			}

			OutputDeliveryList list = null;

			list = OutputDeliveryList.GetNoTicketList(_entity.OidSerie, true);

			//Quitamos de la lista los ya añadidos
			List<OutputDeliveryInfo> lista = new List<OutputDeliveryInfo>();
			foreach (OutputDeliveryInfo item in list)
				if (_entity.AlbaranTickets.GetItemByAlbaran(item.Oid) == null)
					lista.Add(item);

			OutputDeliveryList lista_completa = OutputDeliveryList.GetList(false, ETipoEntidad.Cliente);

			//Añadimos a lista los eliminados
			foreach (AlbaranTicket item in _albaranes_factura)
				if (_entity.AlbaranTickets.GetItemByAlbaran(item.OidAlbaran) == null)
					lista.Add(lista_completa.GetItem(item.OidAlbaran));

			DeliverySelectForm form = new DeliverySelectForm(this, ETipoEntidad.Cliente, OutputDeliveryList.GetList(lista));
			form.ShowDialog(this);
			if (form.DialogResult == DialogResult.OK)
			{
				_results = form.Selected as List<OutputDeliveryInfo>;

				if ((_entity.ETipoFactura == ETipoFactura.Rectificativa) && (_results.Count > 1))
				{
					PgMng.ShowInfoException("No es posible asignar varios albaranes a un ticket rectificativo.");
					return false;
				}

				foreach (OutputDeliveryInfo item in _results)
				{
					if (item.OidHolder != _results[0].OidHolder)
					{
						PgMng.ShowInfoException("No es posible asignar albaranes de clientes distintos a una mismo Ticket.");
						return false;
					}
				}

				_back_job = BackJob.AddAlbaran;
				//PgMng.StartBackJob(this);

				DoAddAlbaran(null);

				if (Result == BGResult.OK)
				{
					Serie_BT.Enabled = false;
					Datos.ResetBindings(false);
				}
			}

			return false;
		}
	
		#endregion

        #region Actions

        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		protected void SetSerieAction()
		{
			SerieSelectForm form = new SerieSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetSerie(form.Selected as SerieInfo, true);
			}
		}

		protected override void SetTipoAction()
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);

			ETipoFactura[] list = { ETipoFactura.Ordinaria, ETipoFactura.Rectificativa };

			form.SetDataSource(Library.Store.EnumText<ETipoFactura>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource tipo = form.Selected as ComboBoxSource;

				if (_entity.Tipo != tipo.Oid)
				{
					_entity.Tipo = tipo.Oid;
					_entity.GetNewCode();
				}
				else
					_entity.Tipo = tipo.Oid;
			}
		}

		protected void SetTPVAction()
		{
			TPVSelectForm form = new TPVSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_entity.OidTpv = ((TPVInfo)form.Selected).Oid;
				_entity.TPV = ((TPVInfo)form.Selected).Nombre;
			}
		}

        protected override void EliminarAlbaranAction()
        {
            if (Entity.AlbaranTickets.Count == 0) return;

			DeliverySelectForm form = new DeliverySelectForm(this, ETipoEntidad.Cliente, OutputDeliveryList.GetList(_albaranes));
            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
				List<OutputDeliveryInfo> results = form.Selected as List<OutputDeliveryInfo>;

				foreach (OutputDeliveryInfo item in results)
                {
                    _entity.Extract(item);
                    _albaranes.Remove(item);
                }
            }

            if (_entity.AlbaranTickets.Count == 0)
            {
				Serie_BT.Enabled = true;
            }

            Datos_Concepto.ResetBindings(false);
        }

        protected override void NuevoAlbaranAction()
        {
            AddAlbaran();

            if (Result == BGResult.OK)
                Datos_Concepto.ResetBindings(false);
        }

        public override void PrintObject()
        {
			SaveObject();

            _entity.SessionCode = Ticket.OpenSession();
            _entity.BeginEdit();
            _entity.BeginTransaction();

			base.PrintObject();

			_entity.EEstado = EEstado.Emitido;

			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
			DialogResult = _action_result;
        }

        #endregion

        #region Buttons

		private void Serie_BT_Click(object sender, EventArgs e)
		{
			SetSerieAction();
		}

		private void TPV_BT_Click(object sender, EventArgs e)
		{
			SetTPVAction();
		}

        #endregion

        #region IBackGroundLauncher

        new enum BackJob { GetFormData, AddAlbaran }
        new BackJob _back_job = BackJob.GetFormData;

        /// <summary>
        /// La llama el backgroundworker para ejecutar codigo en segundo plano
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public new void BackGroundJob(BackgroundWorker bk)
        {
            switch (_back_job)
            {
                case BackJob.AddAlbaran:
                    DoAddAlbaran(bk);
                    break;

                default:
                    base.BackGroundJob(bk);
                    return;
            }

            if (Result == BGResult.OK)
            {
				Serie_BT.Enabled = false;
            }
        }

        protected void DoAddAlbaran(BackgroundWorker bk)
        {
			Datos.RaiseListChangedEvents = false;
			Datos_Concepto.RaiseListChangedEvents = false;

            try
            {
                PgMng.Reset(_results.Count + 1, 1, Resources.Messages.IMPORTANDO_ALBARANES, this);

				_entity.CopyFrom(_results[0]);

				foreach (OutputDeliveryInfo item in _results)
                {
                    _entity.Insert(item);
                    _albaranes.Add(item);
                    PgMng.Grow(string.Empty, "Insertar el Albarán");
                }

                Result = BGResult.OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                PgMng.FillUp();

				Datos.RaiseListChangedEvents = true;
				Datos_Concepto.RaiseListChangedEvents = true;
#if TRACE
                PgMng.ShowCronos();
#endif
            }
        }
        
        #endregion

        #region Events

        private void TicketUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Por posibles fallos en medio no llega a limpiar la cache, aqui eliminamos la sesion y la cache
            Expedients exps = Cache.Instance.Get(typeof(Expedients)) as Expedients;
            if ((exps != null) && (exps.Session() != null)) exps.CloseSession();
            Cache.Instance.Remove(typeof(Expedients));
            Cache.Instance.Remove(typeof(ProductList));
        }

		private void Fecha_DTP_ValueChanged(object sender, EventArgs e)
		{
			_entity.Fecha = Fecha_DTP.Value;
			_entity.PrevisionPago = Library.Common.EnumFunctions.GetPrevisionPago(_entity.EFormaPago, Fecha_DTP.Value, _entity.DiasPago);
		}

        private void Datos_Concepto_ListChanged(object sender, ListChangedEventArgs e)
        {
            //_entity.CalculaTotal();
            Datos.ResetBindings(false);
            HideComponentes();
        }

        #endregion
    }
}