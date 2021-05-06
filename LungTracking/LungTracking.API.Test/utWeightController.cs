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
    public class utWeightController
    {
        Weight weight = new Weight();
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void GetTest()
        {
            Task.Run(async () =>
            {
                var controller = new WeightController();
                var task = controller.Get();
                IEnumerable<Weight> appointments = (IEnumerable<Weight>)task;
                Assert.AreEqual(6, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new WeightController();
                var task = controller.Get(patientId);
                IEnumerable<Weight> appointments = (IEnumerable<Weight>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void PostTest()
        {
            Task.Run(async () =>
            {
                weight.WeightNumberInPounds = 120;
                weight.TimeOfDay = timeOfDay;
                weight.PatientId = patientId;

                var controller = new WeightController();
                var task = controller.Post(weight);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Weight>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void PutTest()
        {
            weight.WeightNumberInPounds = 130;

            Task.Run(async () =>
            {
                var controller = new WeightController();
                var task = controller.Put(weight);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Weight>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var controller = new WeightController();
                var task = controller.Delete(weight.Id);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Weight>;
                Assert.IsNotNull(createdResult);
            });
        }
    }
}