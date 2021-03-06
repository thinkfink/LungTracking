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
    public class utMedicationDetailsController
    {
        MedicationDetails mdt = new MedicationDetails();
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void GetTest()
        {
            Task.Run(async () =>
            {
                var controller = new MedicationDetailsController();
                var task = controller.Get();
                IEnumerable<MedicationDetails> appointments = (IEnumerable<MedicationDetails>)task;
                Assert.AreEqual(6, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new MedicationDetailsController();
                var task = controller.Get(patientId);
                IEnumerable<MedicationDetails> appointments = (IEnumerable<MedicationDetails>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void PostTest()
        {
            Task.Run(async () =>
            {
                mdt.MedicationName = "Testing";
                mdt.MedicationDosageTotal = "100mg";
                mdt.MedicationDosagePerPill = "10mg";
                mdt.MedicationInstructions = "Just testing";
                mdt.NumberOfPills = 10;
                mdt.DateFilled = DateTime.Parse("2021-04-14");
                mdt.QuantityOfFill = 20;
                mdt.RefillDate = DateTime.Parse("2021-05-14");
                mdt.PatientId = patientId;

                var controller = new MedicationDetailsController();
                var task = controller.Post(mdt);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<MedicationDetails>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void PutTest()
        {
            mdt.MedicationInstructions = "More testing";

            Task.Run(async () =>
            {
                var controller = new MedicationDetailsController();
                var task = controller.Put(mdt);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<MedicationDetails>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var controller = new MedicationDetailsController();
                var task = controller.Delete(mdt.Id);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<MedicationDetails>;
                Assert.IsNotNull(createdResult);
            });
        }
    }
}