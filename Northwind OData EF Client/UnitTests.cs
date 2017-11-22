using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.OData.Edm;
using Demos;
using Microsoft.Owin.Hosting;
using Northwind_OData_EF_Client.Server;
using System.Linq;
using System.Net.Http;

namespace Northwind_OData_EF_Client
{
    [TestClass]
    public class UnitTests
    {
        private string _baseAddress;
        private IDisposable _webservice;

        [TestInitialize]
        public void Init()
        {
            _baseAddress = "http://localhost:10844"; //"http://localhost:9000";
            //_webservice = WebApp.Start<Startup>(_baseAddress);
        }

        [TestMethod]
        public void AllProducts_Get()
        {

            var context = new DefaultContainer(new Uri(_baseAddress));
            var products = context.Products.Execute();
            Assert.IsTrue(products.Count() > 0, "No products were returned");

            //HttpClient client = new HttpClient();

            //var response = client.GetAsync(_baseAddress).Result;
            //Assert.IsNotNull(response);
        }

        [TestCleanup]
        public void WrapUp()
        {
            //_webservice.Dispose();
        }
    }
}
