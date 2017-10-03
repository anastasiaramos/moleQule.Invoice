using System;
using System.Collections.Generic;

using moleQule.Library.Hipatia;

namespace moleQule.Library.Invoice
{
    public partial class AgenteSelector : AgenteSelectorBase
    {
        #region Business Methods

        #endregion

        #region Style & Source

        public new static IAgenteHipatiaList GetAgentes(EntidadInfo entidad)
        {
            IAgenteHipatiaList lista = new IAgenteHipatiaList(new List<IAgenteHipatia>());

            if (entidad.Tipo == typeof(Cliente).Name)
            {
                ClienteList list = ClienteList.GetList(false);

                foreach (ClienteInfo obj in list)
                {
                    if (entidad.Agentes.GetItemByProperty("Oid", obj.Oid) == null)
                        lista.Add(obj);
                }
            }
            else if (entidad.Tipo == typeof(Charge).Name)
            {
                ChargeList list = ChargeList.GetList(false);

                foreach (ChargeInfo obj in list)
                {
                    if (entidad.Agentes.GetItemByProperty("Oid", obj.Oid) == null)
                        lista.Add(obj);
                }
            }
			else if (entidad.Tipo == typeof(OutputInvoice).Name)
			{
				OutputInvoiceList list = OutputInvoiceList.GetList(false);

				foreach (OutputInvoiceInfo obj in list)
				{
					if (entidad.Agentes.GetItemByProperty("Oid", obj.Oid) == null)
						lista.Add(obj);
				}
			}
            else
                throw new iQException("No se ha encontrado el tipo de entidad " + entidad.Tipo);

            return lista;
        }

        #endregion
    }
}