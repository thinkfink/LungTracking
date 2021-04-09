using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

namespace LungTracking.BL.Test
{
    [TestClass]
    public class utAppointmentManager
    {
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            List<Appointment> appts = new List<Appointment>();
            appts = AppointmentManager.Load();
            int expected = 300;
            Assert.AreEqual(expected, appts.Count);
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            List<Appointment> appt = new List<Appointment>();
            appt = AppointmentManager.LoadByPatientId(patientId);
            Assert.IsNotNull(appt);
        }

        [TestMethod]
        public void InsertTest()
        {
            Appointment appt = new Appointment();
            appt.Date = DateTime.Parse("2021-05-01");
            appt.TimeStart = TimeSpan.Parse("11:00:00");
            appt.TimeEnd = TimeSpan.Parse("12:00:00");
            appt.Description = "Testing";
            appt.Location = "Test Facility";
            appt.PatientId = patientId;

            int result = AppointmentManager.Insert(appt);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            List<Appointment> appts = AppointmentManager.Load();

            Appointment appt = appts.Where(a => a.Location == "Test Facility").FirstOrDefault();

            appt.Location = "Updated Test Facility";

            AppointmentManager.Update(appt);

            Appointment updatedappt = appts.FirstOrDefault(a => a.Description == "Testing");

            Assert.AreEqual(appt.Location, updatedappt.Location);
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<Appointment> appts = AppointmentManager.Load();
            Appointment appt = appts.Where(a => a.Location == "Updated Test Facility").FirstOrDefault();

            int results = AppointmentManager.Delete(appt.Id);
            Assert.IsTrue(results > 0);
        }
    }
}
