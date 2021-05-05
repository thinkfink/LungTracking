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
    public class utPulseController
    {
        Pulse pulse = new Pulse();
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void GetTest()
        {
            Task.Run(async () =>
            {
                var controller = new PulseController();
                var task = controller.Get();
                IEnumerable<Pulse> appointments = (IEnumerable<Pulse>)task;
                Assert.AreEqual(6, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new PulseController();
                var task = controller.Get(patientId);
                IEnumerable<Pulse> appointments = (IEnumerable<Pulse>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void PostTest()
        {
            Task.Run(async () =>
            {
                pulse.PulseNumber = 90;
                pulse.BeginningEnd = true;
                pulse.TimeOfDay = timeOfDay;
                pulse.PatientId = patientId;

                var controller = new PulseController();
                var task = controller.Post(pulse);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Pulse>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void PutTest()
        {
            pulse.PulseNumber = 100;

            Task.Run(async () =>
            {
                var controller = new PulseController();
                var task = controller.Put(pulse);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Pulse>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var controller = new PulseController();
                var task = controller.Delete(pulse.Id);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Pulse>;
                Assert.IsNotNull(createdResult);
            });
        }
    }
}