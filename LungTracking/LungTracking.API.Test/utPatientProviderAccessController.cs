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
    public class utPatientProviderAccessController
    {
        PatientProviderAccess ppa = new PatientProviderAccess();

        // for testing insert record
        Guid providerId = Guid.Parse("5b0b01f2-f225-4b6a-8084-eaa7b64dfb16");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        // for testing existing record
        Guid oldProviderId = Guid.Parse("5b0b01f2-f225-4b6a-8084-eaa7b64dfb16");
        Guid oldPatientId = Guid.Parse("509db332-936f-4dc2-9c02-66d58a8a20f8");

        [TestMethod]
        public void GetTest()
        {
            Task.Run(async () =>
            {
                var controller = new PatientProviderAccessController();
                var task = controller.Get();
                IEnumerable<PatientProviderAccess> appointments = (IEnumerable<PatientProviderAccess>)task;
                Assert.AreEqual(6, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new PatientProviderAccessController();
                var task = controller.Get(oldPatientId, oldProviderId);
                IEnumerable<PatientProviderAccess> appointments = (IEnumerable<PatientProviderAccess>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void PostTest()
        {
            Task.Run(async () =>
            {
                ppa.PatientId = patientId;
                ppa.ProviderId = providerId;

                var controller = new PatientProviderAccessController();
                var task = controller.Post(ppa);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<PatientProviderAccess>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var controller = new PatientProviderAccessController();
                var task = controller.Delete(ppa.Id);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<PatientProviderAccess>;
                Assert.IsNotNull(createdResult);
            });
        }
    }
}