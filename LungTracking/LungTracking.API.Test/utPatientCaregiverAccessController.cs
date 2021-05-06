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

namespace AJP.LungTracking.API.Test
{
    [TestClass]
    public class utPatientCaregiverAccessController
    {
        PatientCaregiverAccess pca = new PatientCaregiverAccess();

        // for testing insert record
        Guid caregiverId = Guid.Parse("f8cd0f12-04a4-4bb7-afed-425e95018ecc");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        // for testing existing record
        Guid oldCaregiverId = Guid.Parse("79f49d26-fcaa-458f-9a8c-9e98bbce67d5");
        Guid oldPatientId = Guid.Parse("16ee99e5-f8e9-45d3-8621-2435e0b60576");

        [TestMethod]
        public void GetTest()
        {
            Task.Run(async () =>
            {
                var controller = new PatientCaregiverAccessController();
                var task = controller.Get();
                IEnumerable<PatientCaregiverAccess> appointments = (IEnumerable<PatientCaregiverAccess>)task;
                Assert.AreEqual(6, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new PatientCaregiverAccessController();
                var task = controller.Get(oldPatientId, oldCaregiverId);
                IEnumerable<PatientCaregiverAccess> appointments = (IEnumerable<PatientCaregiverAccess>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void PostTest()
        {
            Task.Run(async () =>
            {
                pca.PatientId = patientId;
                pca.CaregiverId = caregiverId;

                var controller = new PatientCaregiverAccessController();
                var task = controller.Post(pca);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<PatientCaregiverAccess>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var controller = new PatientCaregiverAccessController();
                var task = controller.Delete(pca.Id);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<PatientCaregiverAccess>;
                Assert.IsNotNull(createdResult);
            });
        }
    }
}