using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.WebFace.Models;

namespace moleQule.WebFace.Invoice.Models
{
	/// <summary>
	/// ViewModel
	/// </summary>
	[Serializable()]
	public class ClientViewModel : ViewModelBase<Cliente, ClienteInfo>, IViewModel
	{
		#region Attributes

		protected ClientBase _base = new ClientBase();
		protected Country _ocountry;

		#endregion

        #region Properties

		[HiddenInput]
		public long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "ID")]
		public string Codigo { get { return _base.Record.Codigo; } set { _base.Record.Codigo = value; } }

		[HiddenInput]
		public long Status { get { return _base.Record.Estado; } set { _base.Record.Estado = value; } }

		[Required]
		[Display(ResourceType = typeof(Resources.Labels), Name = "NAME")]
		public string Nombre { get { return _base.Record.Nombre; } set { _base.Record.Nombre = value; } }

		//public string NombreComercial { get { return _base._nombre_comercial; } }

        [Required]
		[Display(ResourceType = typeof(Resources.Labels), Name = "VAT_NUMBER")]
		public string VatNumber { get { return _base.Record.VatNumber; } set { _base.Record.VatNumber = value; } }

		//public string Titular { get { return _base._titular; } }
		[Display(ResourceType = typeof(Resources.Labels), Name = "ADDRESS")]
		public string Direccion { get { return _base.Record.Direccion; } set { _base.Record.Direccion = value; } }

		//public string Poblacion { get { return _base._poblacion; } }
		[Display(ResourceType = typeof(Resources.Labels), Name = "ZIP_CODE")]
		public string CodigoPostal { get { return _base.Record.CodigoPostal; } set { _base.Record.CodigoPostal = value; } }

		public string Provincia { get { return _base.Record.Provincia; } set { _base.Record.Provincia = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "CITY")]
		public string Municipio { get { return _base.Record.Municipio; } set { _base.Record.Municipio = value; } }

		[Required]
		[DataType(DataType.EmailAddress)]
        [Remote("ValidateEmail", "Account")]
		[Display(ResourceType = typeof(Resources.Labels), Name = "EMAIL")]
		public string Email { get { return _base.Record.Email; } set { _base.Record.Email = value; } }

		[DataType(DataType.Date)]
		[Display(ResourceType = typeof(Resources.Labels), Name = "BIRTH_DATE")]
		public DateTime BirthDate { get { return _base.Record.BirthDate; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "COUNTRY")]
		public string Country { get { return (_ocountry != null) ? _ocountry.Iso2 : _base.Record.Country; } set { _base.Record.Country = value; _ocountry = Library.Country.Find(value); } }

		public string Prefix { get { return (_ocountry != null) ? _ocountry.Prefix : string.Empty; } set { _base.Record.Prefix = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "PHONE_NUMBER")]
		public string Telefonos { get { return _base.Record.Telefonos; } set { _base.Record.Telefonos = value; } }

		[Required]
		[Display(ResourceType = typeof(Resources.Labels), Name = "PHONE_NUMBER")]
		public string Movil { get { return _base.Record.Movil; } set { _base.Record.Movil = value; } }

		//public string Fax { get { return _base._fax; } }
		//public string Movil { get { return _base._movil; } }
		[UIHint("MultilineText")]
		public string Observaciones { get { return _base.Record.Observaciones; } set { _base.Record.Observaciones = value; } }
		//public string Historia { get { return _base._historia; } }
		//public Decimal LimiteCredito { get { return _base._limite_credito; } }
		//public string Contacto { get { return _base._contacto; } }
		//public long FormaPago { get { return _base._forma_pago; } }
		//public long DiasPago { get { return _base._dias_pago; } }
		//public string CodigoExplotacion { get { return _base._codigo_explotacion; } }
		//public long TipoId { get { return _base._tipo_id; } }
		//public long MedioPago { get { return _base._medio_pago; } }
		//public Decimal PDescuento { get { return _base._p_descuento; } }
		//public Decimal PDescuentoPtoPago { get { return _base._p_descuento_pto_pago; } }
		//public long OidTransporte { get { return _base._oid_transporte; } }
		//public Decimal PrecioTransporte { get { return _base._precio_transporte; } }
		//public long OidCuentaBAsociada { get { return _base._oid_cuentab_asociada; } }
		//public string CuentaBancaria { get { return _base._cuenta_bancaria; } }
		//public string CuentaContable { get { return _base._cuenta_contable; } }
		//public long OidImpuesto { get { return _base._oid_impuesto; } }
		//public Decimal TipoInteres { get { return _base._tipo_interes; } }
		//public long PrioridadPrecio { get { return _base._prioridad_precio; } }
		//public bool EnviarFacturaPendiente { get { return _base._enviar_factura_pendiente; } }

		//NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.EStatus; } }
		public virtual string StatusLabel { get { return _base.StatusLabel; } set {} }

		[HiddenInput]
		public long OidUser { get { return _base.OidUser; } set { _base.OidUser = value; } }

		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "USERNAME")]
		public string Username { get { return _base.Username; } set { _base.Username = value; } }

		public virtual EEstadoItem EUserStatus { get { return _base.EUserStatus; } set { _base.EUserStatus = value; } }
		public virtual string UserStatusLabel { get { return _base.UserStatusLabel; } set { } }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "CREATION_DATE")]
		public DateTime CreationDate { get { return _base.CreationDate; } set { _base.CreationDate = value; } }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(ResourceType = typeof(moleQule.Library.Resources.Labels), Name = "LAST_LOGIN_DATE")]
		public DateTime LastLoginDate { get { return _base.LastLoginDate; } set { _base.LastLoginDate = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "PHOTO")]
		public virtual string Photo { get { return _base.GetCrypFileName(Oid, EFile.Photo); } set { ; } }
		[Display(ResourceType = typeof(Resources.Labels), Name = "PASSPORT")]
		public virtual string Passport { get { return _base.GetCrypFileName(Oid, EFile.Passport); } set { ; } }
		[Display(ResourceType = typeof(Resources.Labels), Name = "DRIVING_LICENSE")]
		public virtual string DrivingLicense { get { return _base.GetCrypFileName(Oid, EFile.DrivingLicense); } set { ; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "PHOTO")]
		public virtual string PhotoRelative { get { return Path.Combine(DataRelativePath, Photo).Replace("\\", "/"); } set { ; } }
		[Display(ResourceType = typeof(Resources.Labels), Name = "PASSPORT")]
		public virtual string PassportRelative { get { return Path.Combine(DataRelativePath, Passport).Replace("\\", "/"); } set { ; } }
		[Display(ResourceType = typeof(Resources.Labels), Name = "DRIVING_LICENSE")]
		public virtual string DrivingLicenseRelative { get { return Path.Combine(DataRelativePath, DrivingLicense).Replace("\\", "/"); } set { ; } }

		public virtual string PhotoAbsolute { get { return Path.Combine(DataAbsolutePath, _base.GetCrypFileName(Oid, EFile.Photo)); } set { ; } }
		public virtual string PassportAbsolute { get { return Path.Combine(DataAbsolutePath, _base.GetCrypFileName(Oid, EFile.Passport)); } set { ; } }
		public virtual string DrivingLicenseAbsolute { get { return Path.Combine(DataAbsolutePath, _base.GetCrypFileName(Oid, EFile.DrivingLicense)); } set { ; } }

		public virtual string DataRelativePath { get { return _base.GetRelativePath(Oid); } set { ; } }
		public virtual string DataAbsolutePath { get { return _base.GetAbsolutePath(Oid); } set { ; } }
		
		//public virtual EPrioridadPrecio EPrioridadPrecio { get { return _base.EPrioridadPrecio; } set { _base._prioridad_precio = (long)value; } }
		//public virtual string PrioridadPrecioLabel { get { return _base.PrioridadPrecioLabel; } }
		//public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } }
		//public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
		//public virtual EFormaPago EFormaPago { get { return _base.EFormaPago; } }
		//public virtual string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
		//public virtual Decimal TotalFacturado { get { return _base._total_facturado; } set { _base._total_facturado = value; } }
		//public virtual Decimal CreditoDispuesto { get { return _base._credito_dispuesto; } set { _base._credito_dispuesto = value; } }
		//public virtual string CuentaAsociada { get { return _base._cuenta_asociada; } set { _base._cuenta_asociada = value; } }
		//public virtual string Impuesto { get { return _base.Impuesto; } }
		//public virtual Decimal PImpuesto { get { return _base._p_impuesto; } }
		//public virtual string NumeroClienteLabel { get { return _base.NumeroClienteLabel; } } /*DEPRECATED*/

		public Cliente BusinessObj { get; set; }
		public ClienteInfo ReadOnlyObj { get; set; }

		#endregion

		#region Business Objects

		public new void CopyFrom(ClienteInfo source)
		{
			if (source == null) return;

			_base.CopyValues(source);
			_ocountry = Library.Country.Find(source.Country);
		}
		public new void CopyTo(Cliente dest, HttpRequestBase request = null)
		{
			if (dest == null) return;

			base.CopyTo(dest, request);
		}

		#endregion

		#region Factory Methods

		public ClientViewModel() {}

		public static ClientViewModel New() { return New(ClienteInfo.New()); }
		public static ClientViewModel New(Cliente source) { return New(source.GetInfo(false)); }
		public static ClientViewModel New(ClienteInfo source)
		{
			ClientViewModel obj = new ClientViewModel();
			obj.CopyFrom(source);
			obj.ReadOnlyObj = source;
			return obj;
		}

		public static ClientViewModel Get(long oid)
		{
			ClientViewModel obj = new ClientViewModel();
			obj.ReadOnlyObj = ClienteInfo.GetUser(oid, false);
			obj.CopyFrom(obj.ReadOnlyObj);			
			return obj;
		}

		public static void Add(ClientViewModel item)
		{
			Cliente newItem = Cliente.New();
			item.CopyTo(newItem);
			newItem.Save();
		}
		public static void Edit(ClientViewModel source, HttpRequestBase request = null)
		{
			Cliente item = Cliente.GetUser(source.Oid);
			source.CopyTo(item, request);
			item.Save();
		}
		public static void Remove(long oid)
		{
			Cliente.Delete(oid);
		}

		#endregion
	}

	/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class ClientListViewModel : List<ClientViewModel>
	{
		#region Business Objects

		#endregion

		#region Factory Methods

		public ClientListViewModel() { }

		public static ClientListViewModel Get()
		{
			ClientListViewModel list = new ClientListViewModel();

			ClienteList sourceList = ClienteList.GetList();

			foreach (ClienteInfo item in sourceList)
				list.Add(ClientViewModel.New(item));

			return list;
		}

		public static ClientListViewModel Get(ClienteList sourceList)
		{
			ClientListViewModel list = new ClientListViewModel();

			foreach (ClienteInfo item in sourceList)
				list.Add(ClientViewModel.New(item));

			return list;
		}

		#endregion
	}
}
