using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using moleQule.Face.Common;
using moleQule.Face.Store;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Balance;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
	public partial class CreditCardStatementMngForm : moleQule.Face.Common.TreeBaseMngForm
	{
		#region Constants

		protected const string PAGO_IMAGE_KEY = "Pago";
		protected const string TARJETA_IMAGE_KEY = "Tarjeta";
		protected const string EXTRACTO_IMAGE_KEY = "Extracto";

		const int GASTOS_PENDIENTES = 0;
		const int INGRESOS_PENDIENTES = 1;

		const int PAGOS_TARJETA_PENDIENTES = 0;

		#endregion

		#region Attributes & Properties

        public new const string ID = "CreditCardStatementMngForm";
		public new static Type Type { get { return typeof(CreditCardStatementMngForm); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        public CreditCardList CreditCards { get; set; }
		public List<BankLineList> BankLines { get; set; }

		#endregion

		#region Factory Methods

		public CreditCardStatementMngForm()
			: this((Form)null) { }

		public CreditCardStatementMngForm(string schema)
			: this(false, null, null) { }

		public CreditCardStatementMngForm(Form parent)
			: this(false, parent, null) { }

		public CreditCardStatementMngForm(bool isModal, Form parent, NotifyEntityList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Tree);
		}

		#endregion

		#region Layout

		public override void FormatControls()
		{
			base.FormatControls();

			MaximizeForm(new Size(325, 0));    	}
		
		protected override void SetView(molView view)
		{
			base.SetView(view);
			
			HideAction(molAction.Print);
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Message = Resources.Messages.RETRIEVING_EXTRACTOS;

            CreditCards = CreditCardList.GetList(ETipoTarjeta.Credito, false);
            PgMng.Grow();

			if (List == null) List = new NotifyEntityList();
			else List.Clear();

			List = new NotifyEntityList();
            
			Date_TI.Text = _fecha.ToShortDateString();            

            PgMng.Grow();

            foreach (CreditCardInfo item in CreditCards)
			{
                //Extractos
                item.LoadChilds(typeof(CreditCardStatement), false);
			}
            PgMng.Grow();

			base.RefreshMainData();
		}

		protected override void BuildTree()
		{
			if (List == null) return;

			TreeNode node = null;
			
			NewRootNode("Extractos de Tarjetas de Crédito", false);

			NotifyEntity item;

            foreach (CreditCardInfo credit_card in CreditCards)
			{
				node = new TreeNode();
				node.Text = credit_card.Nombre;
				node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text, true);
				node.SelectedImageKey = TARJETA_IMAGE_KEY;
				node.ImageKey = TARJETA_IMAGE_KEY;
				Tree_TV.TopNode.Nodes.Add(node);

				//Extractos cobrados
                foreach (CreditCardStatementInfo statement in credit_card.Statements)
				{
                    //Active year filter
                    if (Library.Common.ModulePrincipal.GetUseActiveYear())
                        if (statement.DueDate.Year != Library.Common.ModulePrincipal.GetActiveYear().Year)
                            continue;

					item = new NotifyEntity() 
                    {
                        ETipoNotificacion = ETipoNotificacion.ExtractoTarjeta,
						ETipoEntidad = ETipoEntidad.CreditCardStatement,
                        Oid = statement.Oid,
                        Title = "Extracto de " + statement.DueDate.ToShortDateString(),
                        Total = statement.Amount,
                        SetTotal = true
                    };

					node = new TreeNode();
					node.Name = item.ETipoEntidad.ToString();
					node.Text = item.FullTitle;
					node.Tag = item;
					node.SelectedImageKey = Tree_TV.SelectedImageKey;
					node.ImageKey = EXTRACTO_IMAGE_KEY;
					node.Checked = item.Checked;

					Tree_TV.TopNode.Nodes[Tree_TV.TopNode.Nodes.Count - 1].Nodes.Add(node);

                    //Pagos
                    item = new NotifyEntity()
                    {
                        ETipoNotificacion = ETipoNotificacion.Node,
                        ETipoEntidad = ETipoEntidad.Pago,
                        Oid = statement.Oid,
                        Title = "Pagos",
                        Total = statement.Amount - statement.CashAmount,
                        SetTotal = true
                    };

                    TreeNode subnode = new TreeNode();
                    subnode.Name = item.ETipoEntidad.ToString();
                    subnode.Text = item.FullTitle;
                    subnode.Tag = item;
                    subnode.SelectedImageKey = Tree_TV.SelectedImageKey;
                    subnode.ImageKey = EXTRACTO_IMAGE_KEY;
                    subnode.Checked = item.Checked;

                    node.Nodes.Add(subnode);

                    //Disposiciones en efectivo
                    item = new NotifyEntity()
                    {
                        ETipoNotificacion = ETipoNotificacion.Node,
                        ETipoEntidad = ETipoEntidad.Caja,
                        Oid = statement.Oid,
                        Title = "Disposiciones en efectivo",
                        Total = statement.CashAmount,
                        SetTotal = true
                    };

                    subnode = new TreeNode();
                    subnode.Name = item.ETipoEntidad.ToString();
                    subnode.Text = item.FullTitle;
                    subnode.Tag = item;
                    subnode.SelectedImageKey = Tree_TV.SelectedImageKey;
                    subnode.ImageKey = EXTRACTO_IMAGE_KEY;
                    subnode.Checked = item.Checked;

                    node.Nodes.Add(subnode);
				}
			}

			Tree_TV.TopNode.Expand();

			SetTotales();
		}

		#endregion

		#region Business Methods

		private void SetTotales()
		{
			SetSubtotales(Tree_TV.Nodes[0]);
		}

		private void SetSubtotales(TreeNode node)
		{
            NotifyEntity notify_entity = (NotifyEntity)node.Tag;

			if (notify_entity.ETipoNotificacion != ETipoNotificacion.Node && !notify_entity.SetTotal) return;

            if (node.Nodes.Count > 0) notify_entity.Total = 0;

			foreach (TreeNode item in node.Nodes)
			{
				SetSubtotales(item);
				if (item.Checked) notify_entity.Total += ((NotifyEntity)item.Tag).Total;
			}

            node.Text = notify_entity.FullTitle;
		}

		#endregion

		#region Actions

		protected override void OpenMngFormAction(NotifyEntity item)
		{
			switch (item.ETipoEntidad)
			{
                case ETipoEntidad.Pago:
				case ETipoEntidad.CreditCardStatement:
					{
                        if (item.List == null)
                        {
                            item.List = PaymentList.GetByCreditCardStatement(item.Oid, false);
                            SetTotales();
                        }

						PaymentMngForm form = new PaymentMngForm(false, _parent, ETipoPago.Todos, (PaymentList)item.List);
						FormMngBase.Instance.ShowFormulario(form, this);
						form.ViewMode = molView.Enbebbed;
						form.Text = item.Title;
						form.Left = this.Right + 1;
						form.Width -= this.Width;
						form.FitColumns();
					}
					break;

                case ETipoEntidad.Caja:
                    {
                        if (item.List == null)
                        {
                            item.List = CashLineList.GetByCreditCardStatement(item.Oid, false);
                            SetTotales();
                        }

                        CashLineMngForm form = new CashLineMngForm(false, _parent, (CashLineList)item.List, 1);
                        form.ViewMode = molView.Enbebbed;
                        FormMngBase.Instance.ShowFormulario(form, this);
                        form.Text = item.Title;
                        form.Left = this.Right + 1;
                        form.Width -= this.Width;
                        form.FitColumns();
                    }
                    break;
			}
		}

		public override void RefreshAction()
		{
			try
			{
				ResetTree();

				PgMng.Reset(BarSteps, 1,string.Empty, this);

				RefreshMainData();
				BuildTree();

				FormMngBase.Instance.CloseAllForms(this);
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		#endregion

		#region Events

		#endregion
	}
}