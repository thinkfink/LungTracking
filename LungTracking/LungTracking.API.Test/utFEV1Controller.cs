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
    public class utFEV1Controller
    {
        FEV1 fev1 = new FEV1();
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void GetTest()
        {
            Task.Run(async () =>
            {
                var controller = new FEV1Controller();
                var task = controller.Get();
                IEnumerable<FEV1> appointments = (IEnumerable<FEV1>)task;
                Assert.AreEqual(6, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new FEV1Controller();
                var task = controller.Get(patientId);
                IEnumerable<FEV1> appointments = (IEnumerable<FEV1>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void PostTest()
        {
            Task.Run(async () =>
            {
                fev1.FEV1Number = 3.3M;
                fev1.BeginningEnd = true;
                fev1.TimeOfDay = timeOfDay;
                fev1.PatientId = patientId;

                var controller = new FEV1Controller();
                var task = controller.Post(fev1);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<FEV1>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void PutTest()
        {
            fev1.FEV1Number = 4.1M;

            Task.Run(async () =>
            {
                var controller = new FEV1Controller();
                var task = controller.Put(fev1);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<FEV1>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var controller = new FEV1Controller();
                var task = controller.Delete(fev1.Id);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<FEV1>;
                Assert.IsNotNull(createdResult);
            });
        }
    }
}