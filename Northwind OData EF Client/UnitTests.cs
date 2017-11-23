using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.OData.Edm;
using Demos;
using Microsoft.Owin.Hosting;
using Northwind_OData_EF_Client.Server;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Northwind_OData_EF_Client
{
    [TestClass]
    public class UnitTests
    {
        private string _baseAddress;
        private IDisposable _webservice;
        private string _username;
        private string _password;

        [TestInitialize]
        public void Init()
        {
            _baseAddress = "http://localhost:10844"; //"http://localhost:9000";
            //_webservice = WebApp.Start<Startup>(_baseAddress);
            _username = "c8e1fde7-4248-467a-aad4-4e2a906b2d37";
            _password = "caa9517d-c428-4a8a-8a79-4f994f5c34a0";
        }

        [TestMethod]
        public void AllProducts_Get()
        {

            var context = new DefaultContainer(new Uri(_baseAddress));
            context.SendingRequest2 += Context_SendingRequest2;
            var products = context.Products.Execute();
            Assert.IsTrue(products.Count() > 0, "No products were returned");

            //HttpClient client = new HttpClient();

            //var response = client.GetAsync(_baseAddress).Result;
            //Assert.IsNotNull(response);
        }

        private void Context_SendingRequest2(object sender, Microsoft.OData.Client.SendingRequest2EventArgs e)
        {
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_username + ":" + _password));

            e.RequestMessage.SetHeader("Authorization", "Basic " + svcCredentials);
        }

        [TestCleanup]
        public void WrapUp()
        {
            //_webservice.Dispose();
        }
    }
}
