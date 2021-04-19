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
    public class utPulseManager
    {
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                var task = await PulseManager.Load();
                IEnumerable<Models.Pulse> pulses = task;
                Assert.AreEqual(300, pulses.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var results = await PulseManager.LoadByPatientId(patientId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                Pulse pulse = new Pulse();
                pulse.PulseNumber = 90;
                pulse.BeginningEnd = true;
                pulse.TimeOfDay = timeOfDay;
                pulse.PatientId = patientId;

                int results = await PulseManager.Insert(pulse);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = PulseManager.Load();
                IEnumerable<Models.Pulse> pulses = task.Result;
                Models.Pulse pulse = pulses.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);
                pulse.PulseNumber = 100;
                var results = PulseManager.Update(pulse);
                Assert.IsTrue(results.Result > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = PulseManager.Load();
                IEnumerable<Models.Pulse> pulses = task.Result;
                task.Wait();
                Models.Pulse pulse = pulses.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);
                var results = PulseManager.Delete(pulse.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}