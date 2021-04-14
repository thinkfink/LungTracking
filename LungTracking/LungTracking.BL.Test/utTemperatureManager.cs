using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

namespace LungTracking.BL.Test
{
    [TestClass]
    public class utTemperatureManager
    {
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            List<Temperature> temps = new List<Temperature>();
            temps = TemperatureManager.Load();
            int expected = 300;
            Assert.AreEqual(expected, temps.Count);
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            List<Temperature> temps = new List<Temperature>();
            temps = TemperatureManager.LoadByPatientId(patientId);
            Assert.IsNotNull(temps);
        }

        [TestMethod]
        public void InsertTest()
        {
            Temperature temp = new Temperature();
            temp.TempNumber = 98.6M;
            temp.BeginningEnd = true;
            temp.TimeOfDay = timeOfDay;
            temp.PatientId = patientId;

            int result = TemperatureManager.Insert(temp);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            List<Temperature> temps = TemperatureManager.Load();

            Temperature temp = temps.Where(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId).FirstOrDefault();

            temp.TempNumber = 99.1M;

            TemperatureManager.Update(temp);

            Temperature updatedtemp = temps.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);

            Assert.AreEqual(temp.TempNumber, updatedtemp.TempNumber);
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<Temperature> temps = TemperatureManager.Load();
            Temperature temp = temps.Where(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId).FirstOrDefault();

            int results = TemperatureManager.Delete(temp.Id);
            Assert.IsTrue(results > 0);
        }
    }
}
