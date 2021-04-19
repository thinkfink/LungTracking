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
    public class utWeightManager
    {
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                var task = await WeightManager.Load();
                IEnumerable<Models.Weight> weights = task;
                Assert.AreEqual(300, weights.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var results = await WeightManager.LoadByPatientId(patientId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                Weight weight = new Weight();
                weight.WeightNumberInPounds = 120;
                weight.TimeOfDay = timeOfDay;
                weight.PatientId = patientId;

                int results = await WeightManager.Insert(weight);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = WeightManager.Load();
                IEnumerable<Models.Weight> weights = task.Result;
                Models.Weight weight = weights.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);
                weight.WeightNumberInPounds = 130;
                var results = WeightManager.Update(weight);
                Assert.IsTrue(results.Result > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = WeightManager.Load();
                IEnumerable<Models.Weight> weights = task.Result;
                task.Wait();
                Models.Weight weight = weights.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);
                var results = WeightManager.Delete(weight.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}