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
    public class utSleepWakeManager
    {
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                var task = await SleepWakeManager.Load();
                IEnumerable<Models.SleepWake> sws = task;
                Assert.AreEqual(300, sws.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var results = await SleepWakeManager.LoadByPatientId(patientId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                SleepWake sw = new SleepWake();
                sw.SleepOrWake = true;
                sw.TimeOfDay = timeOfDay;
                sw.PatientId = patientId;

                int results = await SleepWakeManager.Insert(sw);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = SleepWakeManager.Load();
                IEnumerable<Models.SleepWake> sws = task.Result;
                Models.SleepWake sw = sws.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);
                sw.SleepOrWake = false;
                var results = SleepWakeManager.Update(sw);
                Assert.IsTrue(results.Result > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = SleepWakeManager.Load();
                IEnumerable<Models.SleepWake> sws = task.Result;
                task.Wait();
                Models.SleepWake sw = sws.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);
                var results = SleepWakeManager.Delete(sw.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}