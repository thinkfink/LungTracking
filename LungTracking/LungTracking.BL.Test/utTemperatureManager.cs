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
    public class utTemperatureManager
    {
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                var task = await TemperatureManager.Load();
                IEnumerable<Models.Temperature> temps = task;
                Assert.AreEqual(300, temps.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var results = await TemperatureManager.LoadByPatientId(patientId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                Temperature temp = new Temperature();
                temp.TempNumber = 98.6M;
                temp.BeginningEnd = true;
                temp.TimeOfDay = timeOfDay;
                temp.PatientId = patientId;

                int results = await TemperatureManager.Insert(temp);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = TemperatureManager.Load();
                IEnumerable<Models.Temperature> temps = task.Result;
                Models.Temperature temp = temps.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);
                temp.TempNumber = 99.1M;
                var results = TemperatureManager.Update(temp);
                Assert.IsTrue(results.Result > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = TemperatureManager.Load();
                IEnumerable<Models.Temperature> temps = task.Result;
                task.Wait();
                Models.Temperature temp = temps.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);
                var results = TemperatureManager.Delete(temp.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}