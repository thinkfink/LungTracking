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
    public class utSleepWakeController
    {
        SleepWake sw = new SleepWake();
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void GetTest()
        {
            Task.Run(async () =>
            {
                var controller = new SleepWakeController();
                var task = controller.Get();
                IEnumerable<SleepWake> appointments = (IEnumerable<SleepWake>)task;
                Assert.AreEqual(6, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new SleepWakeController();
                var task = controller.GetByPatientId(patientId);
                IEnumerable<SleepWake> appointments = (IEnumerable<SleepWake>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void PostTest()
        {
            Task.Run(async () =>
            {
                sw.SleepOrWake = true;
                sw.TimeOfDay = timeOfDay;
                sw.PatientId = patientId;

                var controller = new SleepWakeController();
                var task = controller.Post(sw);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<SleepWake>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void PutTest()
        {
            sw.SleepOrWake = false;

            Task.Run(async () =>
            {
                var controller = new SleepWakeController();
                var task = controller.Put(sw);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<SleepWake>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var controller = new SleepWakeController();
                var task = controller.Delete(sw.Id);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<SleepWake>;
                Assert.IsNotNull(createdResult);
            });
        }
    }
}