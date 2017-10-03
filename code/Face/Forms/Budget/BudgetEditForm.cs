using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
	public partial class BudgetEditForm : BudgetUIForm
    {
        #region Factory Methods

        public BudgetEditForm(long oid, Form parent)
            : base(oid, parent, null)
		{
			InitializeComponent();
            
			if (_entity != null)
                SetFormData();

			_mf_type = ManagerFormType.MFEdit;
        }

        public BudgetEditForm(Budget source, Form parent)
			: base(-1, parent, source)
		{
			InitializeComponent();
			SetFormData();
			_mf_type = ManagerFormType.MFEdit;
		}

		protected override void GetFormSourceData(long oid, object[] parameters)
		{
            _entity = Budget.Get(oid);
			_entity.BeginEdit();
		}

		protected override void GetFormSourceData(object[] parameters)
		{
            _entity = ((Budget)parameters[0]).CloneAsNew();
			_entity.BeginEdit();
		}

		public override void DisposeForm()
		{
			if (_entity != null)
			{
				_entity.CloseSession();
			}
		}

        #endregion

        #region Layout & Source

		protected override void RefreshMainData()
		{
			if (_entity.OidSerie > 0)
				SetSerie(SerieInfo.Get(_entity.OidSerie, false), false);
			PgMng.Grow();

			base.RefreshMainData();
		}

        #endregion
    }
}

