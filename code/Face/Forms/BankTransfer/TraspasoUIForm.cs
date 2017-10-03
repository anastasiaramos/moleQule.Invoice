using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Store;
using moleQule.Library.Invoice;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Store;

namespace moleQule.Face.Invoice
{
    public partial class TraspasoUIForm : TraspasoForm
    {
        #region Attributes & Properties
		
        public new const string ID = "TraspasoUIForm";
		public new static Type Type { get { return typeof(TraspasoUIForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected Traspaso _entity;

        public override Traspaso Entity { get { return _entity; } set { _entity = value; } }
        public override TraspasoInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected TraspasoUIForm() 
			: this(null) { }

        public TraspasoUIForm(Form parent) 
			: this(-1, parent) { }

        public TraspasoUIForm(long oid) 
			: this(oid, null) { }

        public TraspasoUIForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
			this.Datos.RaiseListChangedEvents = false;

			Traspaso temp = _entity.Clone();
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
				PgMng.FillUp();
				PgMng.ShowInfoException(iQExceptionHandler.GetAllMessages(ex));
				return false;
			}
			finally
			{
				this.Datos.RaiseListChangedEvents = true;
			}
        }

        #endregion

        #region Layout

        /// <summary>Da formato a los controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();

            CuentaDestino_BT.Enabled = _entity.ETipoMovimientoBanco == EBankLineType.Traspaso;
            CuentaOrigen_BT.Enabled = _entity.ETipoMovimientoBanco == EBankLineType.Traspaso;
        }

		#endregion
		
		#region Source
		
        /// <summary>
        /// Asigna el objeto principal al origen de datos principal
		/// y las listas hijas a los origenes de datos correspondientes
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();
						
            base.RefreshMainData();
        }	
		
        #endregion

        #region Validation & Format

        /// <summary>
        /// Valida datos de entrada
        /// </summary>
        protected override void ValidateInput()
        {	
        }
		
        #endregion

		#region Actions

        protected override void SaveAction()
        {	
			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		protected virtual void SetEstadoAction()
		{
			EEstado[] list = { EEstado.Anulado };

			SelectEnumInputForm form = new SelectEnumInputForm(true);

			form.SetDataSource(Library.Common.EnumText<EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;
				_entity.EEstado = (EEstado)estado.Oid;
				Estado_TB.Text = _entity.EstadoLabel;
			}
		}

		protected virtual void SetCuentaOrigenAction()
		{
			BankAccountSelectForm form = new BankAccountSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				BankAccountInfo cuenta = form.Selected as BankAccountInfo;

				_entity.OidCuentaOrigen = cuenta.Oid;
				_entity.CuentaOrigen = cuenta.Valor;
				CuentaOrigen_TB.Text = _entity.CuentaOrigen;
			}
		}

		protected virtual void SetCuentaDestinoAction()
		{
			BankAccountSelectForm form = new BankAccountSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				BankAccountInfo cuenta = form.Selected as BankAccountInfo;

				_entity.OidCuentaDestino = cuenta.Oid;
				_entity.CuentaDestino = cuenta.Valor;
				CuentaDestino_TB.Text = _entity.CuentaDestino;
			}
		}

		#endregion

		#region Buttons

		private void Estado_BT_Click(object sender, EventArgs e)
		{
			SetEstadoAction();
		}

		private void CuentaDestino_BT_Click(object sender, EventArgs e)
		{
			SetCuentaDestinoAction();
		}

		private void CuentaOrigen_BT_Click(object sender, EventArgs e)
		{
			SetCuentaOrigenAction();
		}

		#endregion
    }
}
