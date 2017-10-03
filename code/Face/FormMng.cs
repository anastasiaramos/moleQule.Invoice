using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face;

using moleQule.Library.Common;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    /// <summary>
    /// Clase base para manejo (apertura y cierre) de formularios
    /// Es único en el sistema (singleton)
    /// </summary>
    /// <remarks>
    /// Para utilizar el FormMng es necesario indicar cual será el MainForm padre de los formularios
    /// Este MainForm deberá ser un formulario heredado de MainFormBase
    /// </remarks>
    public class FormMng : IFormMng
    {
        #region Factory Methods

        /// <summary>
        /// Única instancia de la clase (Singleton)
        /// </summary>
        protected static FormMng _main;

        /// <summary>
        /// Unique FormMng Class Instance
        /// </summary>
        /// <remarks>
        /// Para utilizar el FormMng es necesario inicializar
        /// </remarks>
        public static FormMng Instance { get { return (_main != null) ? _main : new FormMng(); } }

        /// <summary>
        /// Constructor
        /// </summary>
        public FormMng()
		{
			// Singleton
			_main = this;
		}

        #endregion

        #region Business Methods

        /// <summary>
        /// Abre un nuevo manager para la entidad. Si no está abierto, lo crea, y si 
        /// lo está, lo muestra 
        /// </summary>
        /// <param name="formID">Identificador del formulario que queremos abrir</param>
        public void OpenForm(string formID) { OpenForm(formID, null, null); }
		public void OpenForm(string formID, object param) { OpenForm(formID, new object[1] { param }); }
		public void OpenForm(string formID, object[] param) { OpenForm(formID, param, null); }

        /// <summary>
        /// Abre un nuevo manager para la entidad. Si no está abierto, lo crea, y si 
        /// lo está, lo muestra 
        /// </summary>
        /// <param name="formID">Identificador del formulario que queremos abrir</param>
        /// <param name="parameters">Parámetro para el formulario</param>
        public void OpenForm(string formID, object[] parameters, Form parent)
        {
            try
            {
                switch (formID)
                {
                    case BalanceMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(BalanceMngForm.Type))
                            {
                                BalanceMngForm em = new BalanceMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case BankLineMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(BankLineMngForm.Type))
                            {
                                BankLineMngForm em = new BankLineMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case BankLinesActionForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(BankLinesActionForm.Type))
                            {
                                BankLinesActionForm em = new BankLinesActionForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case BankLoanMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(BankLoanMngForm.Type))
                            {
                                BankLoanMngForm em = new BankLoanMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case BudgetMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(BudgetMngForm.Type))
                            {
                                BudgetMngForm em = new BudgetMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case CashLineMngForm.ID:
                        {
                            if (FormMngBase.Instance.BuscarFormulario(CashLineMngForm.Type))
                                ((CashLineMngForm)GetFormulario(CashLineMngForm.Type)).Cerrar();

                            CashLineMngForm em = new CashLineMngForm(parent, (int)parameters[0]);
                            FormMngBase.Instance.ShowFormulario(em);
                        } break;

                    case CashActionForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(CashActionForm.Type))
                            {
                                CashActionForm em = new CashActionForm(parent, parameters[0] as Cash);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case CashEditForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(CashEditForm.Type))
                            {
                                CashEditForm em = new CashEditForm((int)parameters[0], parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case CarteraClientesActionForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(CarteraClientesActionForm.Type))
                            {
                                CarteraClientesActionForm em = new CarteraClientesActionForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

					case CashCountMngForm.ID:
						{
							if (FormMngBase.Instance.BuscarFormulario(CashCountMngForm.Type))
								((CashCountMngForm)GetFormulario(CashCountMngForm.Type)).Cerrar();
			
							CashCountMngForm em = new CashCountMngForm(parent, (int)parameters[0]);
							FormMngBase.Instance.ShowFormulario(em);
							
						} break;

                    case ClientMngForm.ID:
                        {
                            if (FormMngBase.Instance.BuscarFormulario(ClientMngForm.Type))
								((ClientMngForm)GetFormulario(ClientMngForm.Type)).Cerrar();

							ClientMngForm em = new ClientMngForm(parent, (EEstado)parameters[0]);
							FormMngBase.Instance.ShowFormulario(em);
                        } break;

                    case CobrosActionForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(CobrosActionForm.Type))
                            {
                                CobrosActionForm em = new CobrosActionForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case ClientChargeMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(ClientChargeMngForm.Type))
                            {
                                ClientChargeMngForm em = new ClientChargeMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break; 

                    case CobrosFomentoEditForm.ID:
                        {
                            if (FormMngBase.Instance.BuscarFormulario(CobrosFomentoEditForm.Type))
                                ((CobrosFomentoEditForm)GetFormulario(CobrosFomentoEditForm.Type)).Cerrar();

                            CobrosFomentoEditForm em = new CobrosFomentoEditForm(parent);
                            FormMngBase.Instance.ShowFormulario(em);
                        }break;

                    case CobrosREAEditForm.ID:
                        {
                            if (FormMngBase.Instance.BuscarFormulario(CobrosREAEditForm.Type))
                                ((CobrosREAEditForm)GetFormulario(CobrosREAEditForm.Type)).Cerrar();

                            CobrosREAEditForm em = new CobrosREAEditForm(parent);
                            FormMngBase.Instance.ShowFormulario(em);
                        } break;

                    case CobroMngForm.ID:
                        {
							if (FormMngBase.Instance.BuscarFormulario(CobroMngForm.Type))
								((CobroMngForm)GetFormulario(CobroMngForm.Type)).Cerrar();

                             CobroMngForm em = new CobroMngForm(parent);
                             FormMngBase.Instance.ShowFormulario(em);
                        } break;

                    case CobroAClienteMngForm.ID:
                        {
							if (FormMngBase.Instance.BuscarFormulario(CobroMngForm.Type))
								((CobroMngForm)GetFormulario(CobroMngForm.Type)).Cerrar();
                                
							CobroAClienteMngForm em = new CobroAClienteMngForm(parent);
                            FormMngBase.Instance.ShowFormulario(em);                            
                        } break;

                    case CobroREAMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(CobroREAMngForm.Type))
                            {
                                CobroREAMngForm em = new CobroREAMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case CobroFomentoMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(CobroFomentoMngForm.Type))
                            {
                                CobroFomentoMngForm em = new CobroFomentoMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case ControlCobrosREAActionForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(ControlCobrosREAActionForm.Type))
                            {
                                ControlCobrosREAActionForm em = new ControlCobrosREAActionForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case CreditCardStatementMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(CreditCardStatementMngForm.Type))
                            {
                                CreditCardStatementMngForm em = new CreditCardStatementMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case DetalleAlbaranesActionForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(DetalleAlbaranesActionForm.Type))
                            {
                                DetalleAlbaranesActionForm em = new DetalleAlbaranesActionForm((OutputDeliveryList)parameters[0]);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case DeliveryAgrupadoMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(DeliveryAgrupadoMngForm.Type))
                            {
                                DeliveryAgrupadoMngForm em = new DeliveryAgrupadoMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case DeliveryFacturadosMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(DeliveryFacturadosMngForm.Type))
                            {
                                DeliveryFacturadosMngForm em = new DeliveryFacturadosMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case DeliveryNoFacturadosMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(DeliveryNoFacturadosMngForm.Type))
                            {
                                DeliveryNoFacturadosMngForm em = new DeliveryNoFacturadosMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case DeliveryAllMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(DeliveryAllMngForm.Type))
                            {
                                DeliveryAllMngForm em = new DeliveryAllMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case ExportarContabilidadActionForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(ExportarContabilidadActionForm.Type))
                            {
                                ExportarContabilidadActionForm em = new ExportarContabilidadActionForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }

                            if (FormMngBase.Instance.BuscarFormulario(Common.RegistroMngForm.Type))
                                ((Common.RegistroMngForm)GetFormulario(Common.RegistroMngForm.Type)).ReloadData();

                            if (FormMngBase.Instance.BuscarFormulario(Common.LineaRegistroMngForm.Type))
                                ((Common.LineaRegistroMngForm)GetFormulario(Common.LineaRegistroMngForm.Type)).ReloadData();

                        } break;

                    case InvoicesBenefitActionForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(InvoicesBenefitActionForm.Type))
                            {
                                InvoicesBenefitActionForm em = new InvoicesBenefitActionForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case FinancialCashMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(FinancialCashMngForm.Type)) 
                            {
                                FinancialCashMngForm em = new FinancialCashMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case InvoiceLineMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(InvoiceLineMngForm.Type))
                            {
                                InvoiceLineMngForm em = new InvoiceLineMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case InvoiceAllMngForm.ID:
                        {
                            if (FormMngBase.Instance.BuscarFormulario(InvoiceChargedMngForm.Type))
                            {
                                ((InvoiceChargedMngForm)GetFormulario(InvoiceChargedMngForm.Type)).Cerrar();
                            }
                            if (FormMngBase.Instance.BuscarFormulario(InvoiceDueMngForm.Type))
                            {
                                ((InvoiceDueMngForm)GetFormulario(InvoiceDueMngForm.Type)).Cerrar();
                            }
                            if (!FormMngBase.Instance.BuscarFormulario(InvoiceAllMngForm.Type))
                            {
								InvoiceAllMngForm em = new InvoiceAllMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }

                        } break;

                    case InvoiceChargedMngForm.ID:
                        {
                            if (FormMngBase.Instance.BuscarFormulario(InvoiceAllMngForm.Type))
                            {
                                ((InvoiceAllMngForm)GetFormulario(InvoiceAllMngForm.Type)).Cerrar();
                            }
                            if (FormMngBase.Instance.BuscarFormulario(InvoiceDueMngForm.Type))
                            {
                                ((InvoiceDueMngForm)GetFormulario(InvoiceDueMngForm.Type)).Cerrar();
                            }
                            if (!FormMngBase.Instance.BuscarFormulario(InvoiceChargedMngForm.Type))
                            {
                                InvoiceChargedMngForm em = new InvoiceChargedMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }

                        } break;

                    case InvoiceDueMngForm.ID:
                        {
                            if (FormMngBase.Instance.BuscarFormulario(InvoiceChargedMngForm.Type))
                            {
                                ((InvoiceChargedMngForm)GetFormulario(InvoiceChargedMngForm.Type)).Cerrar();
                            }
                            if (FormMngBase.Instance.BuscarFormulario(InvoiceAllMngForm.Type))
                            {
                                ((InvoiceAllMngForm)GetFormulario(InvoiceAllMngForm.Type)).Cerrar();
                            }
                            if (!FormMngBase.Instance.BuscarFormulario(InvoiceDueMngForm.Type))
                            {
								InvoiceDueMngForm em = new InvoiceDueMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

					case HistoricoPreciosActionForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(HistoricoPreciosActionForm.Type))
							{
								HistoricoPreciosActionForm em = new HistoricoPreciosActionForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						}
						break;

                    case MergeClientsActionForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(MergeClientsActionForm.Type))
                            {
                                MergeClientsActionForm em = new MergeClientsActionForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }

                        } break;

					case ModelosActionForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(ModelosActionForm.Type))
							{
								ModelosActionForm em = new ModelosActionForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

                    case LoanMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(LoanMngForm.Type))
                            {
                                LoanMngForm em = new LoanMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case MerchantLoanMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(MerchantLoanMngForm.Type))
                            {
                                MerchantLoanMngForm em = new MerchantLoanMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case PedidoMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(PedidoMngForm.Type))
                            {
                                PedidoMngForm em = new PedidoMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        } break;

                    case PricesActionForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(PricesActionForm.Type))
                            {
                                PricesActionForm em = new PricesActionForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        }
                        break;

                    case ResumenCuentasMngForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(ResumenCuentasMngForm.Type))
                            {
                                ResumenCuentasMngForm em = new ResumenCuentasMngForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        }break;

					case TicketMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(TicketMngForm.Type))
							{
								TicketMngForm em = new TicketMngForm(parent, Library.Store.ETipoFacturas.Todas);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

					case TraspasoMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(TraspasoMngForm.Type))
							{
								TraspasoMngForm em = new TraspasoMngForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

                    case SalesActionForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(SalesActionForm.Type))
                            {
                                SalesActionForm em = new SalesActionForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        }
                        break;

                    case VentasMensualActionForm.ID:
                        {
                            if (!FormMngBase.Instance.BuscarFormulario(VentasMensualActionForm.Type))
                            {
                                VentasMensualActionForm em = new VentasMensualActionForm(parent);
                                FormMngBase.Instance.ShowFormulario(em);
                            }
                        }
                        break;

					case WorkDeliveryMngForm.ID:
						{
							if (!FormMngBase.Instance.BuscarFormulario(WorkDeliveryMngForm.Type))
							{
								WorkDeliveryMngForm em = new WorkDeliveryMngForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
						} break;

                    default:
                        {
                            throw new iQImplementationException(string.Format(moleQule.Face.Resources.Messages.FORM_NOT_FOUND, formID), string.Empty);
                        } 
                }
            }
            catch (iQImplementationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
				if (Globals.Instance.ProgressInfoMng != null)
				{
					Globals.Instance.ProgressInfoMng.ShowErrorException(ex);
					Globals.Instance.ProgressInfoMng.FillUp();
				}
				else
					ProgressInfoMng.ShowException(ex);
            }
        }

        /// <summary>
        /// Devuelve un formulario hijo del tipo pasado como parámetro
        /// </summary>
        /// <param name="childType">Tipo de formulario</param>
        public object GetFormulario(Type childType) { return FormMngBase.Instance.GetFormulario(childType); }

        #endregion
    }
}
