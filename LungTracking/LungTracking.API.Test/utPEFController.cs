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
    public class utPEFController
    {
        PEF pef = new PEF();
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void GetTest()
        {
            Task.Run(async () =>
            {
                var controller = new PEFController();
                var task = controller.Get();
                IEnumerable<PEF> appointments = (IEnumerable<PEF>)task;
                Assert.AreEqual(6, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new PEFController();
                var task = controller.Get(patientId);
                IEnumerable<PEF> appointments = (IEnumerable<PEF>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void PostTest()
        {
            Task.Run(async () =>
            {
                pef.PEFNumber = 330;
                pef.BeginningEnd = true;
                pef.TimeOfDay = timeOfDay;
                pef.PatientId = patientId;

                var controller = new PEFController();
                var task = controller.Post(pef);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<PEF>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void PutTest()
        {
            pef.PEFNumber = 340.1M;

            Task.Run(async () =>
            {
                var controller = new PEFController();
                var task = controller.Put(pef);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<PEF>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var controller = new PEFController();
                var task = controller.Delete(pef.Id);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<PEF>;
                Assert.IsNotNull(createdResult);
            });
        }
    }
}