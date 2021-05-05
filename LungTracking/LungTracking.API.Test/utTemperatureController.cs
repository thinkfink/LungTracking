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
    public class utTemperatureController
    {
        Temperature temp = new Temperature();
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void GetTest()
        {
            Task.Run(async () =>
            {
                var controller = new TemperatureController();
                var task = controller.Get();
                IEnumerable<Temperature> appointments = (IEnumerable<Temperature>)task;
                Assert.AreEqual(6, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new TemperatureController();
                var task = controller.Get(patientId);
                IEnumerable<Temperature> appointments = (IEnumerable<Temperature>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void PostTest()
        {
            Task.Run(async () =>
            {
                temp.TempNumber = 98.6M;
                temp.BeginningEnd = true;
                temp.TimeOfDay = timeOfDay;
                temp.PatientId = patientId;

                var controller = new TemperatureController();
                var task = controller.Post(temp);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Temperature>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void PutTest()
        {
            temp.TempNumber = 99.1M;

            Task.Run(async () =>
            {
                var controller = new TemperatureController();
                var task = controller.Put(temp);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Temperature>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var controller = new TemperatureController();
                var task = controller.Delete(temp.Id);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Temperature>;
                Assert.IsNotNull(createdResult);
            });
        }
    }
}