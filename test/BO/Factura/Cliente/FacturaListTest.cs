using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System;

using moleQule.Library;
using moleQule.Library.Application;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice.Tests
{    
    /// <summary>
    ///This is a test class for FacturaListTest and is intended
    ///to contain all FacturaListTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FacturaListTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
         
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            System.Diagnostics.Process.Start(@"xcopy", @"""P:\\dotNet\Cattle\code\\moleQule.Application\\Library\\Asm\Test"" "".\\Asm"" /Y /R /I");

            try { AppController.Instance.Init("7.5.23.2"); }
            catch { }

            SettingsMng.Instance.SetLANServer("localhost");

            if (AppContext.Principal != null)
                AppContext.Principal.Logout();

            Principal.Login("Admin", "iQi_1998");

            Assert.IsTrue(AppContext.User.IsAuthenticated);
            
            //Carga del schema
            long oidSchema = SettingsMng.Instance.GetDefaultSchema();
            
            Assert.AreNotEqual(oidSchema, 0);
            
            CompanyInfo company = CompanyInfo.Get(oidSchema);
            
            Assert.AreNotEqual(company.Oid, 0);
            Assert.IsTrue(AppContext.User.CanAccessSchema(company.Oid));

            AppContext.Principal.ChangeUserSchema(company);
        }
        
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        public static void MyClassCleanup()
        {
            AppContext.Principal.Logout();
        }
        
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod()]
        public void OutputInvoiceListNotIsNull()
        {
            OutputInvoiceList list = null;
            list = OutputInvoiceList.GetList(2015, false);
            Assert.IsNotNull(list);
            Assert.AreEqual(list.Count, 0);
        }

        [TestMethod()]
        public void OutputInvoiceListHasItems()
        {
            OutputInvoiceList list = null;
            list = OutputInvoiceList.GetList(2014, false);
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod()]
        public void OutputInvoiceListHasItemsOnlyFromAClient()
        {
            OutputInvoiceList list = null;
            QueryConditions conditions = new QueryConditions { Cliente = ClienteInfo.New(1) };
            list = OutputInvoiceList.GetList(conditions, false);
            Assert.IsNull(list.FirstOrDefault(x => x.OidCliente != conditions.Cliente.Oid));
        }

        [TestMethod()]
        public void OutputInvoiceListHasItemsOnlyFromASerie()
        {
            OutputInvoiceList list = null;
            QueryConditions conditions = new QueryConditions { Serie = SerieInfo.New(1) };
            list = OutputInvoiceList.GetList(conditions, false);
            Assert.IsNull(list.FirstOrDefault(x => x.OidSerie != conditions.Serie.Oid));
        }
    }
}
