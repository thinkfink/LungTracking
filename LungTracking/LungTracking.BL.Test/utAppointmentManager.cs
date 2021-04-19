using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LungTracking.BL.Test
{
    [TestClass]
    public class utAppointmentManager
    {
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () => 
            {
                var task = await AppointmentManager.Load();
                IEnumerable<Models.Appointment> appointments = task;
                Assert.AreEqual(300, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            Task.Run(async () => 
            {
                var results = await AppointmentManager.LoadByPatientId(patientId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () => 
            {
                Appointment appt = new Appointment();
                appt.Date = DateTime.Parse("2021-05-01");
                appt.TimeStart = TimeSpan.Parse("11:00:00");
                appt.TimeEnd = TimeSpan.Parse("12:00:00");
                appt.Description = "Testing";
                appt.Location = "Test Facility";
                appt.PatientId = patientId;

                int results = await AppointmentManager.Insert(appt);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () => 
            { 
                var task = AppointmentManager.Load();
                IEnumerable<Models.Appointment> appts = task.Result;
                Models.Appointment appt = appts.FirstOrDefault(a => a.Location == "Test Facility");
                appt.Location = "Updated Test Facility";
                var results = AppointmentManager.Update(appt);
                Assert.IsTrue(results.Result > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () => 
            {
                var task = AppointmentManager.Load();
                IEnumerable<Models.Appointment> appts = task.Result;
                task.Wait();
                Models.Appointment appt = appts.FirstOrDefault(a => a.Location == "Updated Test Facility");
                var results = AppointmentManager.Delete(appt.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}
