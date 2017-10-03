using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;
using moleQule.Library.Hipatia;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
    [Serializable()]
    public class OutputInvoiceInfo : ReadOnlyBaseEx<OutputInvoiceInfo>, IAgenteHipatia, IEntidadRegistroInfo
    {
        #region IAgenteHipatia

        public string IDHipatia { get { return Ano + "/" + Codigo; } }
        public string NombreHipatia { get { return Cliente; } }
		public Type TipoEntidad { get { return typeof(OutputInvoice); } }
        public string ObservacionesHipatia { get { return Observaciones; } }

        #endregion

		#region IEntidadRegistroInfo

		public ETipoEntidad ETipoEntidad { get { return ETipoEntidad.FacturaEmitida; } }
		public string DescripcionRegistro { get { return "FACTURA EMITIDA Nº " + NFactura + " de " + Fecha.ToShortDateString() + " de " + Total.ToString("C2") + " de " + Cliente; } }

		#endregion

        #region Attributes

		protected OutputInvoiceBase _base = new OutputInvoiceBase();

        protected CobroFacturaList _cobro_facturas = null;
        protected OutputInvoiceLineList _conceptos = null;
		protected AlbaranFacturaList _albaran_facturas = null;

        #endregion

        #region Properties

		public OutputInvoiceBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidSerie { get { return _base.Record.OidSerie; } }
		public long OidCliente { get { return _base.Record.OidCliente; } }
		public long OidTransportista { get { return _base.Record.OidTransportista; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public string VatNumber { get { return _base.Record.VatNumber; } }
		public string Cliente { get { return _base.Record.Cliente; } }
		public string Direccion { get { return _base.Record.Direccion; } }
		public string CodigoPostal { get { return _base.Record.CodigoPostal; } }
		public string Provincia { get { return _base.Record.Provincia; } }
		public string Municipio { get { return _base.Record.Municipio; } }
		public long Ano { get { return _base.Record.Ano; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public long FormaPago { get { return _base.Record.FormaPago; } }
		public long DiasPago { get { return _base.Record.DiasPago; } }
		public long MedioPago { get { return _base.Record.MedioPago; } }
		public DateTime Prevision { get { return _base.Record.Prevision; } }
		public Decimal BaseImponible { get { return _base.Record.BaseImponible; } }
		public Decimal Impuestos { get { return _base.Record.Impuestos; } }
		public Decimal PDescuento { get { return _base.Record.PDescuento; } }
		public Decimal Descuento { get { return _base.Record.Descuento; } }
		public Decimal Total { get { return _base.Record.Total; } }
		public string CuentaBancaria { get { return _base.Record.CuentaBancaria; } }
		public bool Nota { get { return _base.Record.Nota; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public bool Albaran { get { return _base.Record.Agrupada; } }
		public bool Rectificativa { get { return _base.Record.Rectificativa; } }
		public long Estado { get { return _base.Record.Estado; } }
		public Decimal PIRPF { get { return _base.Record.PIrpf; } }
		public string Albaranes { get { return _base.Record.Albaranes; } }
		public string IdMovContable { get { return _base.Record.IdMovContable; } }
		public long EstadoCobro { get { return _base.Record.EstadoCobro; } }
		public long OidUsuario { get { return _base.Record.OidUsuario; } }

        public virtual CobroFacturaList CobroFacturas { get { return _cobro_facturas; } }
        public virtual OutputInvoiceLineList ConceptoFacturas { get { return _conceptos; } }
		public virtual AlbaranFacturaList AlbaranFacturas { get { return _albaran_facturas; } }

        //CAMPOS NO ENLAZADOS
		public EEstado EEstado { get { return _base.EStatus; } }
		public string EstadoLabel { get { return _base.StatusLabel; } }
		public EEstado EEstadoCobro { get { return _base.EEstadoCobro; } set { _base.EEstadoCobro = value; } }
		public string EstadoCobroLabel { get { return _base.EstadoCobroLabel; } }
		public EFormaPago EFormaPago { get { return _base.EFormaPago; } }
		public string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
		public EMedioPago EMedioPago { get { return _base.EMedioPago; } }
		public string MedioPagoLabel { get { return _base.MedioPagoLabel; } }

		public virtual string Usuario { get { return _base.Usuario; } set { _base.Usuario = value; } }
		public string NFactura { get { return _base.NFactura; } }
        public string NumeroSerie { get { return _base._n_serie; } }
		public virtual string Serie { get { return _base._serie; } }
		public virtual string NSerieSerie { get { return _base.NSerieSerie; } }
		public virtual string IDCliente { get { return _base._id_cliente; } set { _base._id_cliente = value; } }
		public virtual string NumeroCliente { get { return _base._id_cliente; } } /* DEPRECATED */
		public virtual bool Cobrada { get { return _base.Cobrada; } }
        public decimal Cobrado { get { return _base._cobrado; } set { _base._cobrado = value; } }
		public decimal Pendiente { get { return _base.Pendiente; } set { _base.Pendiente = value; } }
		public decimal NoVencido { get { return _base._efectos_negociados; } } /* DEPRECATED */
        public decimal Vencido { get { return _base._efectos_devueltos; } } /* DEPRECATED */
		public decimal EfectosNegociados { get { return _base._efectos_negociados; } }
		public decimal EfectosDevueltos { get { return _base._efectos_devueltos; } }
        public decimal EfectosPendientesVto { get { return _base._efectos_pendientes_vto; } }
		public decimal PendienteVencido { get { return _base._pendiente_vencido; } set { _base._pendiente_vencido = value; } }
		public decimal DudosoCobro { get { return _base._dudoso_cobro; } }
		public decimal GastosCobro { get { return _base._gastos_demora; } }/*DEPRECATED*/
		public decimal TipoInteres { get { return _base._tipo_interes; } }
        public long DiasTranscurridos { get { return _base.DiasTranscurridos; } }
		public Decimal Subtotal { get { return _base.Subtotal; } }
		public virtual Decimal IRPF { get { return _base.IRPF; } }
		public virtual Decimal IGIC { get { return Impuestos; } } /* DEPRECATED */
        public bool IDManual { get { return false; } }
		public string Link { get { return _base.Link; } }
		public string FileName { get { return _base.FileName;} }
		public decimal Asignado { get { return _base._asignado; } set { _base._asignado = value; } }
		public string Vinculado { get { return _base._activo; } set { _base._activo = value; } }
		public string FechaCobro { get { return _base.FechaCobro; } }
		public string FechaAsignacion { get { return _base.FechaAsignacion; } set { _base.FechaAsignacion = value; } }
		public decimal Acumulado { get { return _base._acumulado; } set { _base._acumulado = value; } }
		public string IDMovimientoContable { get { return _base._id_mov_contable; } }
		public virtual Decimal TotalExpediente { get { return _base._total_expediente; } set { _base._total_expediente = value; } }
        public virtual decimal GastosDemora { get { return _base._gastos_demora; } }
        public virtual decimal CondicionesVenta { get { return _base._condiciones_venta; } }
        public virtual decimal PrecioCoste { get { return Decimal.Round(_base._precio_coste, 2); } set { _base._precio_coste = value; } }
        public virtual decimal Beneficio { get { return Decimal.Round(_base._beneficio, 2); } set { _base._beneficio = value; } }
        public virtual decimal PBeneficio { get { return Decimal.Round(_base._p_beneficio, 2); } set { _base._p_beneficio = value; } }
        public bool AlbaranContado { get { return _base.Record.Agrupada; } }
        public DateTime StepDate { get { return _base.StepDate; } }

        #endregion

        #region Business Methods

        public void CopyFrom(OutputInvoice source) { _base.CopyValues(source); }

        public List<CuentaResumen> GetCuentas()
        {
			try
			{
				List<CuentaResumen> list = new List<CuentaResumen>();
				ProductList productos = ProductList.GetList(false, true);
				FamiliaList familias = FamiliaList.GetList(false, true);
				bool nuevo;
				ProductInfo producto;
				FamiliaInfo familia;
				string cuenta;

				foreach (OutputInvoiceLineInfo item in _conceptos)
				{
                    if (item.OidProducto == 0) continue;

					nuevo = true;
					producto = productos.GetItem(item.OidProducto);
					familia = familias.GetItem(producto.OidFamilia);

					cuenta = (producto.CuentaContableVenta == string.Empty) ? familia.CuentaContableVenta : producto.CuentaContableVenta;

					//Agrupamos los conceptos por cuentas contables
					for (int i = 0; i < list.Count; i++)
					{
						CuentaResumen cr = list[i];

						//Tiene prioridad la cuenta contable del producto
						if (producto.CuentaContableVenta != string.Empty)
						{
							if (cr.CuentaContable == producto.CuentaContableVenta)
							{
								cr.Importe += item.BaseImponible;
								list[i] = cr;
								nuevo = false;
								break;
							}
						}
						//Luego la de la familia
						else if (cr.CuentaContable == familia.CuentaContableVenta)
						{
							cr.Importe += item.BaseImponible;
							list[i] = cr;
							nuevo = false;
							break;
						}
					}

					if (nuevo)
						list.Add(new CuentaResumen { OidFamilia = producto.OidFamilia, Importe = item.BaseImponible, CuentaContable = cuenta });

				}

				return list;
			}
			catch 
			{
				throw new iQException(String.Format(Resources.Messages.ERROR_FACTURA_CUENTA, NFactura, Cliente));
			}
		}

        public Hashtable GetImpuestos() { return _base.GetImpuestos(_conceptos); }

        public List<CuentaResumen> GetCuentasAndImpuestos()
        {
            try
            {
                List<CuentaResumen> list = new List<CuentaResumen>();
                ProductList productos = ProductList.GetList(false, true);
                FamiliaList familias = FamiliaList.GetList(false, true);
                ImpuestoList impuestos = ImpuestoList.GetList(false);
                bool nuevo;
                ProductInfo producto;
                FamiliaInfo familia;
                string cuenta;
                string nombre;

                foreach (OutputInvoiceLineInfo item in _conceptos)
                {
                    ImpuestoResumen impuesto = new ImpuestoResumen();

                    nuevo = true;
                    producto = productos.GetItem(item.OidProducto);
                    familia = familias.GetItem(producto.OidFamilia);


                    cuenta = (producto.CuentaContableVenta == string.Empty) ? familia.CuentaContableVenta : producto.CuentaContableVenta;
                    nombre = (producto.CuentaContableVenta == string.Empty) 
                            ? familia.Codigo + " " + familia.Nombre 
                            : producto.Codigo + " " + producto.Nombre;

                    //Agrupamos los conceptos por cuentas contables
                    for (int i = 0; i < list.Count; i++)
                    {
                        CuentaResumen cr = list[i];

                        //Tiene prioridad la cuenta contable del producto
                        if (producto.CuentaContableVenta != string.Empty)
                        {
                            if ((cr.CuentaContable == producto.CuentaContableVenta) && (cr.Impuesto != null && cr.Impuesto.OidImpuesto == item.OidImpuesto))
                            {
                                cr.Importe += item.Subtotal;
                                cr.Impuesto.Importe += item.Subtotal * item.PImpuestos / 100;
                                cr.Impuesto.BaseImponible += item.Subtotal;
                                list[i] = cr;
                                nuevo = false;
                                break;
                            }
                        }
                        //Luego la de la familia
                        else if ((cr.CuentaContable == familia.CuentaContableVenta) && (cr.Impuesto != null && cr.Impuesto.OidImpuesto == item.OidImpuesto))
                        {

                            cr.Importe += item.Subtotal;
                            cr.Impuesto.Importe += item.Subtotal * item.PImpuestos / 100;
                            cr.Impuesto.BaseImponible += item.Subtotal;
                            list[i] = cr;
                            nuevo = false;
                            break;
                        }
                    }

                    if (nuevo)
                    {
                        CuentaResumen new_cr = new CuentaResumen
                        {
                            OidFamilia = producto.OidFamilia,
                            Importe = item.Subtotal,
                            CuentaContable = cuenta,
                            Nombre = nombre,
                        };

                        if (item.Impuestos != 0)
                        {
                            ImpuestoInfo imp = null;
                            
                            if (item.OidImpuesto != 0)
                                imp = impuestos.GetItem(item.OidImpuesto);
                            else 
                                imp = impuestos.GetItemByProperty("Porcentaje", item.PImpuestos);

                            new_cr.Impuesto = new ImpuestoResumen
                            {
                                OidImpuesto = item.OidImpuesto,
                                BaseImponible = item.Subtotal,
                                Importe = item.Subtotal * item.PImpuestos / 100,
                                SubtipoFacturaEmitida = imp.CodigoImpuestoA3Emitida,
                                Porcentaje = item.PImpuestos,
                            };
                        }
                        else 
                        {
                            new_cr.Impuesto = new ImpuestoResumen
                            {
                                OidImpuesto = item.OidImpuesto,
                                BaseImponible = item.Subtotal,
                                Importe = item.Subtotal
                            };
                        }

                        list.Add(new_cr);
                    }

                    if (item.PDescuento > 0)
                    {
                        CuentaResumen new_cr = new CuentaResumen
                        {
                            OidFamilia = producto.OidFamilia,
                            Importe = -item.Descuento,
                            CuentaContable = cuenta,
                            Nombre = nombre,
                        };

                        if (item.Impuestos != 0)
                        {
                            ImpuestoInfo imp = impuestos.GetItem(item.OidImpuesto);

                            new_cr.Impuesto = new ImpuestoResumen
                            {
                                OidImpuesto = item.OidImpuesto,
                                BaseImponible = item.Descuento,
                                Importe = item.Descuento * item.PImpuestos / 100,
                                SubtipoFacturaEmitida = imp.CodigoImpuestoA3Emitida,
                                Porcentaje = item.PImpuestos,
                            };
                        }
                        else
                        {
                            new_cr.Impuesto = new ImpuestoResumen
                            {
                                OidImpuesto = item.OidImpuesto,
                                BaseImponible = item.Descuento,
                                Importe = item.Descuento,
                            };
                        }

                        list.Add(new_cr);
                    }
                }

                return list;
            }
            catch
            {
                throw new iQException(String.Format(Resources.Messages.ERROR_FACTURA_CUENTA, NFactura, Cliente));
            }
        }

        public List<CuentaResumen> GetCuentasAndImpuestosA3()
        {
            try
            {
                List<CuentaResumen> list = new List<CuentaResumen>();
                ProductList productos = ProductList.GetList(false, true);
                FamiliaList familias = FamiliaList.GetList(false, true);
                ImpuestoList impuestos = ImpuestoList.GetList(false);
                bool nuevo;
                ProductInfo producto;
                FamiliaInfo familia;
                string cuenta;
                string nombre;

                foreach (OutputInvoiceLineInfo item in _conceptos)
                {
                    ImpuestoResumen impuesto = new ImpuestoResumen();

                    nuevo = true;
                    producto = productos.GetItem(item.OidProducto);
                    familia = familias.GetItem(producto.OidFamilia);


                    cuenta = (producto.CuentaContableVenta == string.Empty) ? familia.CuentaContableVenta : producto.CuentaContableVenta;
                    nombre = (producto.CuentaContableVenta == string.Empty)
                            ? familia.Codigo + " " + familia.Nombre
                            : producto.Codigo + " " + producto.Nombre;

                    //Agrupamos los conceptos por cuentas contables
                    for (int i = 0; i < list.Count; i++)
                    {
                        CuentaResumen cr = list[i];

                        //Tiene prioridad la cuenta contable del producto
                        if (producto.CuentaContableVenta != string.Empty)
                        {
                            if ((cr.CuentaContable == producto.CuentaContableVenta) && (cr.Impuesto != null && cr.Impuesto.OidImpuesto == item.OidImpuesto))
                            {
                                cr.Importe += item.PDescuento > 0 ? item.BaseImponible : item.Subtotal;
                                cr.Impuesto.Importe += item.Impuestos;
                                cr.Impuesto.BaseImponible += item.PDescuento > 0 ? item.BaseImponible : item.Subtotal;
                                list[i] = cr;
                                nuevo = false;
                                break;
                            }
                        }
                        //Luego la de la familia
                        else if ((cr.CuentaContable == familia.CuentaContableVenta) && (cr.Impuesto != null && cr.Impuesto.OidImpuesto == item.OidImpuesto))
                        {

                            cr.Importe += item.PDescuento > 0 ? item.BaseImponible : item.Subtotal;
                            cr.Impuesto.Importe += item.Impuestos;
                            cr.Impuesto.BaseImponible += item.PDescuento > 0 ? item.BaseImponible : item.Subtotal;
                            list[i] = cr;
                            nuevo = false;
                            break;
                        }
                    }

                    if (nuevo)
                    {
                        CuentaResumen new_cr = new CuentaResumen
                        {
                            OidFamilia = producto.OidFamilia,
                            Importe = item.PDescuento > 0 ? item.BaseImponible : item.Subtotal,
                            CuentaContable = cuenta,
                            Nombre = nombre,
                        };

                        if (item.Impuestos != 0)
                        {
                            ImpuestoInfo imp = null;

                            if (item.OidImpuesto != 0)
                                imp = impuestos.GetItem(item.OidImpuesto);
                            else
                                imp = impuestos.GetItemByProperty("Porcentaje", item.PImpuestos);

                            new_cr.Impuesto = new ImpuestoResumen
                            {
                                OidImpuesto = item.OidImpuesto,
                                BaseImponible = item.PDescuento > 0 ? item.BaseImponible : item.Subtotal,
                                Importe = item.Impuestos,
                                SubtipoFacturaEmitida = imp.CodigoImpuestoA3Emitida,
                                Porcentaje = item.PImpuestos,
                            };
                        }
                        //else
                        //{
                        //    new_cr.Impuesto = new ImpuestoResumen
                        //    {
                        //        OidImpuesto = item.OidImpuesto,
                        //        BaseImponible = 0,
                        //        Importe = 0
                        //    };
                        //}

                        list.Add(new_cr);
                    }
                }

                return list;
            }
            catch
            {
                throw new iQException(String.Format(Resources.Messages.ERROR_FACTURA_CUENTA, NFactura, Cliente));
            }
        }

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
        protected OutputInvoiceInfo() { /* require use of factory methods */ }
        private OutputInvoiceInfo(int sessionCode, IDataReader reader, bool childs)
        {
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }
        internal OutputInvoiceInfo(OutputInvoice item, bool copy_childs)
        {
            _base.CopyValues(item);

            if (copy_childs)
            {
                _conceptos = (item.Conceptos != null) ? OutputInvoiceLineList.GetChildList(item.Conceptos) : null;
                _cobro_facturas = (item.CobroFacturas != null) ? CobroFacturaList.GetChildList(item.CobroFacturas) : null;
            }
        }
        
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> con los datos del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        /// <remarks>
        /// NO OBTIENE los datos de los hijos. Para ello utiliza GetChild(IDataReader reader, bool childs)
        /// La utiliza la ReadOnlyListBaseEx correspondiente para montar la lista
        /// <remarks/>
        public static OutputInvoiceInfo GetChild(int sessionCode, IDataReader reader, bool childs = false)
        {
            return new OutputInvoiceInfo(sessionCode, reader, childs);
        }

		public virtual void LoadChilds(Type type, bool getChilds)
		{
            if (type.Equals(typeof(OutputInvoiceLine)) || type.Equals(typeof(OutputInvoiceLineInfo)))
            {
                _conceptos = OutputInvoiceLineList.GetChildList(this, getChilds);
            }
			if (type.Equals(typeof(CobroFacturaInfo)) || type.Equals(typeof(CobroFactura)))
			{
				_cobro_facturas = CobroFacturaList.GetChildList(this, getChilds);
			}
		}

        #endregion

        #region Root Factory Methods

		public static OutputInvoiceInfo Get(string query, bool childs)
		{
			if (!OutputInvoice.CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = OutputInvoice.GetCriteria(OutputInvoice.OpenSession());
			criteria.Childs = childs;
			criteria.Query = query;

			OutputInvoiceInfo obj = DataPortal.Fetch<OutputInvoiceInfo>(criteria);
			OutputInvoice.CloseSession(criteria.SessionCode);
			return (obj.Oid != 0) ? obj : null;
		}

        public static OutputInvoiceInfo Get(long oid, bool childs = false) { return Get(OutputInvoiceInfo.SELECT(oid), childs); }
		
		public static OutputInvoiceInfo GetByCode(string code, long oidSerie, int year, bool childs)
		{
			OutputInvoice fac = OutputInvoice.New();
			Serie ser = Store.Serie.New();
			fac.Codigo = code;
			ser.Oid = oidSerie;

			QueryConditions conditions = new QueryConditions
			{
				Factura = fac.GetInfo(false),
				Serie = ser.GetInfo(false),
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)
			};
			
			return Get(OutputInvoiceInfo.SELECT_BY_CODE(conditions), childs);
		}
		public static OutputInvoiceInfo GetByDelivery(long oidDelivery, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				OutputDelivery = OutputDeliveryInfo.New(oidDelivery)
			};

			return Get(OutputInvoice.SELECT_BY_DELIVERY(conditions, false), childs);
		}

        public OutputInvoicePrint GetPrintObject()
        {
            return OutputInvoicePrint.New(this,
                                    ClienteInfo.Get(this.OidCliente),
                                    TransporterInfo.Get(OidTransportista, ETipoAcreedor.TransportistaDestino, false),
                                    SerieInfo.Get(OidSerie));
        }

		public static OutputInvoiceInfo New(long oid = 0) { return new OutputInvoiceInfo() { Oid = oid }; }

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            try
            {
				_base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;				

                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);

                    if (Childs)
                    {
                        string query = string.Empty;

                        query = CobroFacturaList.SELECT_BY_FACTURA(this.Oid);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_cobro_facturas = CobroFacturaList.GetChildList(SessionCode, reader);

                        query = OutputInvoiceLineList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _conceptos = OutputInvoiceLineList.GetChildList(SessionCode, reader);

						query = AlbaranFacturaList.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_albaran_facturas = AlbaranFacturaList.GetChildList(SessionCode, reader);
                    }
                }
            }
            catch (Exception ex)
            {
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
        }

        #endregion

        #region Child Data Access

        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);

                if (Childs)
                {
                    string query = string.Empty;
                    IDataReader reader;

                    query = CobroFacturaList.SELECT_BY_FACTURA(this.Oid);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_cobro_facturas = CobroFacturaList.GetChildList(SessionCode, reader);

                    query = OutputInvoiceLineList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
					_conceptos = OutputInvoiceLineList.GetChildList(SessionCode, reader);

					query = AlbaranFacturaList.SELECT(this);
					reader = nHMng.SQLNativeSelect(query, Session());
					_albaran_facturas = AlbaranFacturaList.GetChildList(SessionCode, reader);
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        #endregion

        #region SQL

        public static string SELECT(long oid) { return OutputInvoice.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return OutputInvoice.SELECT(conditions, false); }
		public static string SELECT_BY_CODE(QueryConditions conditions) { return OutputInvoice.SELECT_BY_CODE(conditions, false); }

        #endregion
    }

    /// <summary>
    /// ReadOnly Root Object
    /// </summary>
    [Serializable()]
    public class SerialFacturaInfo : SerialInfo
    {
        #region Attributes

        #endregion

        #region Properties

        #endregion

        #region Business Methods

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
        protected SerialFacturaInfo() { /* require use of factory methods */ }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Obtiene el último serial de la entidad desde la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
        public static SerialFacturaInfo Get(long oid_serie, int year, ETipoFactura tipo)
        {
            CriteriaEx criteria = OutputInvoice.GetCriteria(OutputInvoice.OpenSession());
            criteria.Childs = false;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT(oid_serie, year, tipo);

            SerialFacturaInfo obj = DataPortal.Fetch<SerialFacturaInfo>(criteria);
            OutputInvoice.CloseSession(criteria.SessionCode);
            return obj;
        }

        /// <summary>
        /// Obtiene el siguiente serial para una entidad desde la base de datos
        /// </summary>
        /// <param name="entity">Tipo de Entidad</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
        public static long GetNext(long oid_serie, int year, ETipoFactura tipo)
        {
            return Get(oid_serie, year, tipo).Value + 1;
        }
        
        #endregion

        #region Root Data Access

        #endregion

        #region SQL

        public static string SELECT(long oidSerie, int year, ETipoFactura tipo)
        {
            string f = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
            string s = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
            string query;

			QueryConditions conditions = new QueryConditions
			{
				Serie = Serie.New().GetInfo(false),
				TipoFactura = tipo,
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)
			};
			conditions.Serie.Oid = oidSerie;

			query = 
			"	SELECT 0 AS \"OID\", MAX(\"SERIAL\") AS \"SERIAL\"" +
			"	FROM " + f + " AS F" +
			"	INNER JOIN " + s + " AS S ON F.\"OID_SERIE\" = S.\"OID\"" +
				OutputInvoice.WHERE(conditions);

            return query;
        }

        #endregion
    }
}
