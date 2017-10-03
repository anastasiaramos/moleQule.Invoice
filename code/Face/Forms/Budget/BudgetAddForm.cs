using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
	public partial class BudgetAddForm : BudgetUIForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        #endregion

        #region Factory Methods

        public BudgetAddForm()
            : this(null) {}

        public BudgetAddForm(Form parent) 
			: base(-1, parent, null)
		{
			InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData(object[] parameters)
		{
            _entity = Budget.New();
			_entity.BeginEdit();
		}

		#endregion

        #region Layout & Source

		protected override void RefreshMainData()
		{
			if (Library.Invoice.ModulePrincipal.GetDefaultSerieSetting() > 0)
				SetSerie(SerieInfo.Get(Library.Invoice.ModulePrincipal.GetDefaultSerieSetting(), false), true);
			PgMng.Grow();

			base.RefreshMainData();
		}

        #endregion	
    }
}

