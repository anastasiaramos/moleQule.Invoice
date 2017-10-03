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

		protected override int BarSteps { get { return base.BarSteps + 7; } }

        public CreditCardList TarjetasList { get; set; }
		public List<PaymentList> PagosList { get; set; }
		public List<BankLineList> MovimientosList { get; set; }

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

			MovimientosList = new List<BankLineList>();
			PagosList = new List<PaymentList>();
		}

		#endregion

		#region Layout

		public override void FormatControls()
		{
			base.FormatControls();

			MaximizeForm(new Size(325, 0));

			SetActionStyle(molAction.CustomAction1, Resources.Labels.NEW_EXTRACTO_TI, Properties.Resources.apunte_bancario);
		}
		
		protected override void SetView(molView view)
		{
			base.SetView(view);
			
			ShowAction(molAction.CustomAction1);
			HideAction(molAction.Print);
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Message = Resources.Messages.RETRIEVING_EXTRACTOS;

			if (List == null) List = new NotifyEntityList();
			else List.Clear();

			List = new NotifyEntityList();
            
			Date_TI.Text = _fecha.ToShortDateString();

            TarjetasList = CreditCardList.GetList(ETipoTarjeta.Credito, false);

			MovimientosList.Clear();
			PagosList.Clear();

			PaymentList pagos;

            foreach (CreditCardInfo item in TarjetasList)
			{
                //Extractos
                item.LoadChilds(typeof(CreditCardStatement), false);

				//Extracto Pendiente
				pagos = PaymentList.GetListByVencimientoTarjeta(DateTime.MinValue, _fecha, item, false);
				PagosList.Add(pagos);

				//Extractos cobrados
                BankLineList movimientos = BankLineList.GetByCreditCardList(item, _fecha, false);
				MovimientosList.Add(movimientos);

				foreach (BankLineInfo mov in movimientos)
				{
					pagos = PaymentList.GetListByMovimiento(mov, false);
					PagosList.Add(pagos);
				}
			}
			PgMng.Grow();

			base.RefreshMainData();
		}

		protected override void BuildTree()
		{
			if (List == null) return;

			TreeNode node = null;
			
			NewRootNode("Extractos de Tarjetas de Crédito");

			int index = 0;
			PaymentList pagos;
			NotifyEntity item;

            foreach (CreditCardInfo tarjeta in TarjetasList)
			{
				node = new TreeNode();
				node.Text = tarjeta.Nombre;
				node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text, true);
				node.SelectedImageKey = TARJETA_IMAGE_KEY;
				node.ImageKey = TARJETA_IMAGE_KEY;
				Tree_TV.TopNode.Nodes.Add(node);

				//Extracto pendiente
				pagos = PagosList[index++];

				item = new NotifyEntity(ETipoNotificacion.PagoTarjetaVencido
										, ETipoEntidad.Pago
										, pagos.Count
										, pagos.Total()
										, "Extracto de pagos pendientes"
										, pagos);

				node = new TreeNode();
				node.Name = item.ETipoEntidad.ToString();
				node.Text = item.FullTitle;
				node.Tag = item;
				node.SelectedImageKey = Tree_TV.SelectedImageKey;
				node.ImageKey = PAGO_IMAGE_KEY;
				node.Checked = item.Checked;

				Tree_TV.TopNode.Nodes[Tree_TV.TopNode.Nodes.Count - 1].Nodes.Add(node);

				//Extractos cobrados
				BankLineList movs = MovimientosList[TarjetasList.IndexOf(tarjeta)];

				foreach (BankLineInfo mov in movs)
				{
					pagos = PagosList[index++];

					item = new NotifyEntity(ETipoNotificacion.ExtractoTarjeta
											, ETipoEntidad.Pago
											, pagos.Count
											, pagos.Total()
											, "Extracto del  " + mov.FechaOperacion.ToShortDateString()
											, pagos);

					node = new TreeNode();
					node.Name = item.ETipoEntidad.ToString();
					node.Text = item.FullTitle;
					node.Tag = item;
					node.SelectedImageKey = Tree_TV.SelectedImageKey;
					node.ImageKey = EXTRACTO_IMAGE_KEY;
					node.Checked = item.Checked;

					Tree_TV.TopNode.Nodes[Tree_TV.TopNode.Nodes.Count - 1].Nodes.Add(node);
				}
			}

			Tree_TV.ExpandAll();

			SetTotales();
		}

		#endregion

		#region Business Methods

		protected void NewApunteBancario(NotifyEntity item)
		{
			if (item.ETipoNotificacion != ETipoNotificacion.PagoTarjetaVencido)
			{
                if (((PaymentList)item.List).Count > 0)
                {
                    //PgMng.ShowInfoException("Seleccione un extracto pendiente de la tarjeta");
                    
                    PaymentInfo pg = ((PaymentList)item.List)[0];
                    CreditCardInfo tarjeta = CreditCardInfo.Get(pg.OidTarjetaCredito, false);
                    PaymentInfo pago = PaymentInfo.GetByVencimientoTarjeta(tarjeta, pg.Vencimiento, false);
                    
                    PagoFraccionadoTarjetaEditForm form = new PagoFraccionadoTarjetaEditForm(pago.Oid, ETipoPago.FraccionadoTarjeta, this);
                    form.ShowDialog();
                }
				return;
			}

			PaymentList list = (PaymentList)item.List;

			if (list.Count == 0)
			{
				PgMng.ShowInfoException("No existen extractos pendientes para esta tarjeta");
				return;
			}

			Library.Invoice.ModuleController.CreateApuntesBancarios(list);

			RefreshAction();
		}

		private void SetTotales()
		{
			SetSubtotales(Tree_TV.TopNode);
		}

		private void SetSubtotales(TreeNode node)
		{
			if (((NotifyEntity)node.Tag).ETipoNotificacion != ETipoNotificacion.Node) return;

			if (node.Nodes.Count > 0) ((NotifyEntity)node.Tag).Total = 0;

			foreach (TreeNode item in node.Nodes)
			{
				SetSubtotales(item);
				if (item.Checked) ((NotifyEntity)node.Tag).Total += ((NotifyEntity)item.Tag).Total;
			}

			node.Text = ((NotifyEntity)node.Tag).FullTitle;
		}

		#endregion

		#region Actions

		protected override void OpenMngFormAction(NotifyEntity item)
		{
			switch (item.ETipoEntidad)
			{
				case ETipoEntidad.Pago:
					{
						PaymentMngForm form = new PaymentMngForm(false, _parent, ETipoPago.Todos, (PaymentList)item.List);
						FormMngBase.Instance.ShowFormulario(form, this);
						form.ViewMode = molView.Enbebbed;
						form.Text = item.Title;
						form.Left = this.Right + 1;
						form.Width -= this.Width;
						form.FitColumns();
					}
					break;
			}
		}

		public override void CustomAction1()
		{
			if (Tree_TV.SelectedNode == null) return;
			if ((NotifyEntity)Tree_TV.SelectedNode.Tag == null) return;

			NewApunteBancario((NotifyEntity)Tree_TV.SelectedNode.Tag);
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