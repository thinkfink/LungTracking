using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using LungTracking.API.Controllers;
using LungTracking.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AJP.SurveyMaker.API.Test
{
    [TestClass]
    public class utProviderIdController
    {
        Provider provider = new Provider();
        Guid providerId = Guid.Parse("bf27f470-06f9-418d-bde9-18be4c4c11cd");

        [TestMethod]
        public void GetByProviderIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new ProviderIdController();
                var task = controller.Get(providerId);
                IEnumerable<Provider> appointments = (IEnumerable<Provider>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }
    }
}