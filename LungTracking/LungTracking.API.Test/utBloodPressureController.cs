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
    public class utBloodPressureController
    {
        BloodPressure bp = new BloodPressure();
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void GetTest()
        {
            Task.Run(async () =>
            {
                var controller = new BloodPressureController();
                var task = controller.Get();
                IEnumerable<BloodPressure> appointments = (IEnumerable<BloodPressure>)task;
                Assert.AreEqual(6, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new BloodPressureController();
                var task = controller.GetByPatientId(patientId);
                IEnumerable<BloodPressure> appointments = (IEnumerable<BloodPressure>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void PostTest()
        {
            Task.Run(async () =>
            {
                bp.BPsystolic = 120;
                bp.BPdiastolic = 80;
                bp.BeginningEnd = true;
                bp.TimeOfDay = timeOfDay;
                bp.PatientId = patientId;

                var controller = new BloodPressureController();
                var task = controller.Post(bp);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<BloodPressure>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void PutTest()
        {
            bp.BPsystolic = 110;
            bp.BPdiastolic = 70;

            Task.Run(async () =>
            {
                var controller = new BloodPressureController();
                var task = controller.Put(bp);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<BloodPressure>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var controller = new BloodPressureController();
                var task = controller.Delete(bp.Id);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<BloodPressure>;
                Assert.IsNotNull(createdResult);
            });
        }
    }
}