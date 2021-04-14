using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

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
            List<Pulse> pulses = new List<Pulse>();
            pulses = PulseManager.Load();
            int expected = 300;
            Assert.AreEqual(expected, pulses.Count);
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            List<Pulse> pulses = new List<Pulse>();
            pulses = PulseManager.LoadByPatientId(patientId);
            Assert.IsNotNull(pulses);
        }

        [TestMethod]
        public void InsertTest()
        {
            Pulse temp = new Pulse();
            temp.PulseNumber = 90;
            temp.BeginningEnd = true;
            temp.TimeOfDay = timeOfDay;
            temp.PatientId = patientId;

            int result = PulseManager.Insert(temp);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            List<Pulse> pulses = PulseManager.Load();

            Pulse temp = pulses.Where(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId).FirstOrDefault();

            temp.PulseNumber = 100;

            PulseManager.Update(temp);

            Pulse updatedtemp = pulses.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);

            Assert.AreEqual(temp.PulseNumber, updatedtemp.PulseNumber);
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<Pulse> pulses = PulseManager.Load();
            Pulse temp = pulses.Where(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId).FirstOrDefault();

            int results = PulseManager.Delete(temp.Id);
            Assert.IsTrue(results > 0);
        }
    }
}
