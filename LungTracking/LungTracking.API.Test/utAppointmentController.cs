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
    public class utAppointmentController
    {
        Appointment appt = new Appointment();
        Guid appointmentId = Guid.Parse("66dde9a9-0cab-4096-a15a-57c2749b60a0");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void GetTest()
        {
            Task.Run(async () =>
            {
                var controller = new AppointmentController();
                var task = controller.Get();
                IEnumerable<Appointment> appointments = (IEnumerable<Appointment>)task;
                Assert.AreEqual(6, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByAppointmentIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new AppointmentController();
                var task = controller.GetByAppointmentId(appointmentId);
                IEnumerable<Appointment> appointments = (IEnumerable<Appointment>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new AppointmentController();
                var task = controller.GetByPatientId(patientId);
                IEnumerable<Appointment> appointments = (IEnumerable<Appointment>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void PostTest()
        {
            Task.Run(async () =>
            {
                appt.Date = DateTime.Parse("2021-05-01");
                appt.TimeStart = TimeSpan.Parse("11:00:00");
                appt.TimeEnd = TimeSpan.Parse("12:00:00");
                appt.Description = "Testing";
                appt.Location = "Test Facility";
                appt.PatientId = patientId;

                var controller = new AppointmentController();
                var task = controller.Post(appt);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Appointment>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void PutTest()
        {
            appt.Location = "Updated Test Facility";

            Task.Run(async () =>
            {
                var controller = new AppointmentController();
                var task = controller.Put(appt);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Appointment>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var controller = new AppointmentController();
                var task = controller.Delete(appt.Id);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Appointment>;
                Assert.IsNotNull(createdResult);
            });
        }
    }
}