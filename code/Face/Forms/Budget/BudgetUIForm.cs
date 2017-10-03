using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Store;
using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class BudgetUIForm : BudgetForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 4; } }

        /// <summary>
        /// Se trata de la Proforma actual y que se va a editar.
        /// </summary>
        protected Budget _entity = null;

        protected ExpedientInfo _expediente = null;
        protected SerieInfo _serie = null;
		protected ClienteInfo _cliente = null;

        public override Budget Entity { get { return _entity; } set { _entity = value; } }
        public override BudgetInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }

        protected BudgetLine Line { get { return Datos_Concepto.Current != null ? Datos_Concepto.Current as BudgetLine : null; } }
        
        #endregion

        #region Factory Methods

        public BudgetUIForm() : this(-1) {}

        public BudgetUIForm(long oid)
            : this(oid, null, null) {}

        public BudgetUIForm(long oid, Form parent, Budget source)
            : base(oid, true, parent, source)
        {
            InitializeComponent();
        }

        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            Budget temp = _entity.Clone();
            temp.ApplyEdit();

            // do the save
            try
            {
                _entity = temp.Save();
                _entity.ApplyEdit();
                //_entity.BeginEdit();
                return true;
			}
			catch (Exception ex)
			{
				CleanCache();
				PgMng.ShowInfoException(ex);
				return false;
			}
			finally
			{
				this.Datos.RaiseListChangedEvents = true;
				PgMng.FillUp();
			}
        }

        #endregion

        #region Layout & Source

		public override void FormatControls()
		{
			Fecha_DTP.Checked = true;
			if (_entity != null) Serie_BT.Enabled = (_entity.Conceptos.Count == 0);
			base.FormatControls();
		}

        #endregion

        #region Source
        
        protected override void RefreshMainData()
		{
			Datos.DataSource = _entity;
			Datos_Concepto.DataSource = _entity.Conceptos;
			PgMng.Grow();

			if (_entity.OidCliente > 0)
			{
				_cliente = ClienteInfo.Get(_entity.OidCliente, false);
				SetCliente(_cliente);
			}
			PgMng.Grow();

			Fecha_DTP.Value = _entity.Fecha;

			base.RefreshMainData();
		}

        #endregion

        #region Validation & Format

        #endregion

		#region Business Methods

		private void DeleteKit(BatchInfo partida)
		{
			BudgetLine concepto;

			foreach (BatchInfo item in partida.Componentes)
			{
				concepto = _entity.Conceptos.GetItem(new FCriteria<long>("OidPartida", item.Oid));
				_entity.Conceptos.Remove(concepto);
			}
		}

		protected void SetSerie(SerieInfo source, bool new_code)
		{
			if (source == null) return;

			_serie = source;

			_entity.OidSerie = source.Oid;
			_entity.NumeroSerie = source.Identificador;
			Serie_TB.Text = source.Nombre;
			Nota_TB.Text = source.Cabecera;

			if (new_code) _entity.GetNewCode();

			Cache.Instance.Remove(typeof(BatchList));
			Cache.Instance.Remove(typeof(ProductList));
		}

		protected void SetCliente(ClienteInfo source)
		{
			if (source == null) return;

			Datos_Cliente.DataSource = source;

			_entity.CopyFrom(source);

			//Cargamos los precios especiales del cliente
			if (source.Productos == null)
				source.LoadChilds(typeof(ProductoCliente), false);
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

        protected override void EditarClienteAction()
        {
            if (_entity.Conceptos.Count > 0)
            {
				PgMng.ShowInfoException("No es posible cambiar el cliente a una proforma con conceptos asociados.");
                return;
            }

            ClientSelectForm form = new ClientSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _cliente = form.Selected as ClienteInfo;

				SetCliente(_cliente);
	        }
        }

		protected override void NuevoConceptoAction()
        {
			if (_entity.OidSerie == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_SERIE_SELECTED);
				return;
			}

			if (_entity.OidCliente == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_CLIENT_SELECTED);
				return;
			}

			_cliente = Datos_Cliente.Current as ClienteInfo;
			BudgetLineAddForm form = new BudgetLineAddForm(ETipoProducto.Almacen, _entity, _serie, _cliente, this);
			
			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_entity.CalculaTotal();

				if (_entity.Conceptos.Count > 0)
					Serie_BT.Enabled = false;

				RefreshConceptos();
				ControlsMng.UpdateBinding(Datos);

				HideComponentes();
			}
        }

        protected override void NuevoConceptoLibreAction()
        {
			if (_entity.OidSerie == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_SERIE_SELECTED);
				return;
			}

			if (_entity.OidCliente == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_CLIENT_SELECTED);
				return;
			}

			_cliente = Datos_Cliente.Current as ClienteInfo;

			BudgetLineAddForm form = new BudgetLineAddForm(ETipoProducto.Libres, _entity, _serie, _cliente, this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_entity.CalculaTotal();
				RefreshConceptos();
			}
        }

        protected override void EditarConceptoAction()
        {
			if (Datos_Concepto.Current == null) return;

            BudgetLine cf = (BudgetLine)Datos_Concepto.Current;
			_cliente = Datos_Cliente.Current as ClienteInfo;

			if (cf.OidExpediente == 0)
			{
				BudgetLineEditForm form = new BudgetLineEditForm(ETipoProducto.Libres, _entity, _serie, _cliente, cf, this);

				if (form.ShowDialog(this) == DialogResult.OK)
				{
					_entity.CalculaTotal();
					RefreshConceptos();
				}
			}
			else
			{
				BudgetLineEditForm form = new BudgetLineEditForm(ETipoProducto.Libres, _entity, _serie, _cliente, cf, this);

				if (form.ShowDialog(this) == DialogResult.OK)
				{
					_entity.CalculaTotal();
					RefreshConceptos();
				}
			}
        }

		protected override void EliminarConceptoAction()
		{
			if (Datos_Concepto.Current == null) return;

			if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.DELETE_CONFIRM) == DialogResult.Yes)
			{
				PgMng.Reset(5, 1, Store.Resources.Messages.UPDATING_STOCK, this);

				BatchInfo pexp = BatchInfo.Get(Line.OidPartida, true);
				PgMng.Grow();

				if (pexp.IsKit) DeleteKit(pexp);
				PgMng.Grow();

				_entity.Conceptos.Remove(Line);
				_entity.CalculaTotal();
				PgMng.Grow();

				RefreshConceptos();
				ControlsMng.UpdateBinding(Datos);
				PgMng.FillUp();
			}

			Serie_BT.Enabled = (_entity.Conceptos.Count > 0);
		}

		protected override void SelectExpedienteLineaAction()
		{
			if (Datos_Concepto.Current == null) return;

            BudgetLine item = Datos_Concepto.Current as BudgetLine;

			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ExpedientInfo source = (ExpedientInfo)form.Selected;

				item.OidExpediente = source.Oid;
				item.Expediente = source.Codigo;

				//AddCacheItem(source);
			}
		}

		protected override void SelectImpuestoLineaAction()
		{
			if (Datos_Concepto.Current == null) return;

            BudgetLine item = Datos_Concepto.Current as BudgetLine;

			ImpuestoSelectForm form = new ImpuestoSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ImpuestoInfo source = (ImpuestoInfo)form.Selected;

				item.OidImpuesto = source.Oid;
				item.PImpuestos = source.Porcentaje;

				_entity.CalculaTotal();
			}
		}

        #endregion

        #region Print

        public override void PrintObject()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;

            if (_action_result == DialogResult.OK)
            {
				FormMngBase.Instance.RefreshFormsData();

				base.PrintObject();
            }
        }

        #endregion

		#region Buttons

		private void Serie_BT_Click(object sender, EventArgs e)
		{
			SetSerieAction();
		}

		#endregion

		#region Events

		private void ProformaUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cache.Instance.Remove(typeof(Expedients));
            Cache.Instance.Remove(typeof(BatchList));
			Cache.Instance.Remove(typeof(ProductList));
        }

        private void PDescuento_NTB_TextChanged(object sender, EventArgs e)
        {
            _entity.PDescuento = PDescuento_NTB.DecimalValue;
            _entity.CalculaTotal();
            Datos_Concepto.ResetBindings(false);
            Datos.ResetBindings(false);
        }

        private void Fecha_DTP_ValueChanged(object sender, EventArgs e)
        {
            _entity.Fecha = Fecha_DTP.Value;
        }

        #endregion
    }
}

