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

namespace LungTracking.API.Test
{
    [TestClass]
    public class utCaregiverIdController
    {
        Caregiver caregiver = new Caregiver();
        Guid caregiverId = Guid.Parse("f8cd0f12-04a4-4bb7-afed-425e95018ecc");

        [TestMethod]
        public void GetByCaregiverIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new CaregiverIdController();
                var task = controller.Get(caregiverId);
                IEnumerable<Caregiver> appointments = (IEnumerable<Caregiver>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }
    }
}