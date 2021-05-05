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
    public class utPatientController
    {
        Patient patient = new Patient();
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");
        Guid userId = Guid.Parse("a203c2d8-733b-4cee-9bf5-b53d96bc0e16");

        [TestMethod]
        public void GetTest()
        {
            Task.Run(async () =>
            {
                var controller = new PatientController();
                var task = controller.Get();
                IEnumerable<Patient> appointments = (IEnumerable<Patient>)task;
                Assert.AreEqual(6, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByUserIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new PatientController();
                var task = controller.Get(userId);
                IEnumerable<Patient> appointments = (IEnumerable<Patient>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void PostTest()
        {
            Task.Run(async () =>
            {
                patient.FirstName = "newfirstname";
                patient.LastName = "newlastname";
                patient.Sex = "M";
                patient.DateOfBirth = DateTime.Parse("1980-01-01");
                patient.StreetAddress = "123 Test Street";
                patient.City = "Testopolis";
                patient.State = "WI";
                patient.PhoneNumber = "(555)555-2345";
                patient.UserId = Guid.Parse("a007c7f2-cbd7-48dd-8fd3-adb871b55c25");

                var controller = new PatientController();
                var task = controller.Post(patient);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Patient>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void PutTest()
        {
            patient.LastName = "updatedlastname";

            Task.Run(async () =>
            {
                var controller = new PatientController();
                var task = controller.Put(patient);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Patient>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var controller = new PatientController();
                var task = controller.Delete(patient.Id);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Patient>;
                Assert.IsNotNull(createdResult);
            });
        }
    }
}