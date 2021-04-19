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
    public class utMedicationTrackingController
    {
        MedicationTracking mt = new MedicationTracking();
        Guid medicationId = Guid.Parse("aa596935-5d6f-4a1d-940e-3c3afb6fd9c7");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void GetTest()
        {
            Task.Run(async () =>
            {
                var controller = new MedicationTrackingController();
                var task = controller.Get();
                IEnumerable<MedicationTracking> appointments = (IEnumerable<MedicationTracking>)task;
                Assert.AreEqual(6, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new MedicationTrackingController();
                var task = controller.GetByPatientId(patientId);
                IEnumerable<MedicationTracking> appointments = (IEnumerable<MedicationTracking>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void PostTest()
        {
            Task.Run(async () =>
            {
                mt.PillTakenTime = DateTime.Parse("2021-04-14 11:00:00");
                mt.MedicationId = medicationId;
                mt.PatientId = patientId;

                var controller = new MedicationTrackingController();
                var task = controller.Post(mt);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<MedicationTracking>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void PutTest()
        {
            mt.PillTakenTime = DateTime.Parse("2021-04-14 12:00:00");

            Task.Run(async () =>
            {
                var controller = new MedicationTrackingController();
                var task = controller.Put(mt);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<MedicationTracking>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var controller = new MedicationTrackingController();
                var task = controller.Delete(mt.Id);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<MedicationTracking>;
                Assert.IsNotNull(createdResult);
            });
        }
    }
}