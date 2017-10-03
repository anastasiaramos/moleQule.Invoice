using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class CashCountUIForm : CashCountForm
    {
        #region Attributes & Properties
		
        public new const string ID = "CierreCajaUIForm";
		public new static Type Type { get { return typeof(CashCountUIForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected CierreCaja _entity;

        protected Cash _caja;

        public override CierreCaja Entity { get { return _entity; } set { _entity = value; } }
        public override CierreCajaInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(true) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected CashCountUIForm() 
			: this(-1, null) { }

        public CashCountUIForm(long oid) 
			: this(oid, null) { }

        public CashCountUIForm(long oid, Form parent)
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

			// do the save
			try
			{

				CierreCaja temp = _entity.Clone();
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

        #region Layout

        public override void FormatControls()
        {
            base.FormatControls();
        }

        protected override void SetRowFormat(DataGridViewRow row)
        {
            if (row.IsNewRow) return;
            CashLine item = (CashLine)row.DataBoundItem;

            if (item.Locked) row.ReadOnly = true;

            Face.Common.ControlTools.Instance.SetRowColorIM(row, item.EEstado);
        }

        #endregion

		#region Business Methods

		protected virtual void UpdateSaldo()
		{
            if (_caja != null)
            {
                _caja.UpdateSaldo(Fecha_DTP.Value);
                _entity.CopyFrom(_caja);

                Debe_NTB.Text = _entity.Debe.ToString("C2");
                Haber_NTB.Text = _entity.Haber.ToString("C2");
                SaldoCierre_NTB.Text = _entity.Saldo.ToString("C2");
                SaldoFinal_NTB.Text = _entity.SaldoFinal.ToString("C2");
            }
		}

		#endregion

		#region Actions

		/// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		protected virtual void SetCajaAction() {}
        
		#endregion

		#region Buttons

		private void CajaTickets_BT_Click(object sender, EventArgs e)
		{
			SetCajaAction();
		}

		#endregion

		#region Events

		private void Fecha_DTP_ValueChanged(object sender, EventArgs e)
		{
			Datos.RaiseListChangedEvents = false;
			UpdateSaldo();
			Datos.RaiseListChangedEvents = true;
		}

		#endregion
	}
}