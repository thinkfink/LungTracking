using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

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
            List<SleepWake> sws = new List<SleepWake>();
            sws = SleepWakeManager.Load();
            int expected = 300;
            Assert.AreEqual(expected, sws.Count);
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            List<SleepWake> sws = new List<SleepWake>();
            sws = SleepWakeManager.LoadByPatientId(patientId);
            Assert.IsNotNull(sws);
        }

        [TestMethod]
        public void InsertTest()
        {
            SleepWake sw = new SleepWake();
            sw.SleepOrWake = true;
            sw.TimeOfDay = timeOfDay;
            sw.PatientId = patientId;

            int result = SleepWakeManager.Insert(sw);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            List<SleepWake> sws = SleepWakeManager.Load();

            SleepWake sw = sws.Where(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId).FirstOrDefault();

            sw.SleepOrWake = false;

            SleepWakeManager.Update(sw);

            SleepWake updatedsw = sws.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);

            Assert.AreEqual(sw.SleepOrWake, updatedsw.SleepOrWake);
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<SleepWake> sws = SleepWakeManager.Load();
            SleepWake sw = sws.Where(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId).FirstOrDefault();

            int results = SleepWakeManager.Delete(sw.Id);
            Assert.IsTrue(results > 0);
        }
    }
}
