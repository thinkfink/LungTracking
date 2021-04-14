using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

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
            List<Weight> weights = new List<Weight>();
            weights = WeightManager.Load();
            int expected = 300;
            Assert.AreEqual(expected, weights.Count);
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            List<Weight> weights = new List<Weight>();
            weights = WeightManager.LoadByPatientId(patientId);
            Assert.IsNotNull(weights);
        }

        [TestMethod]
        public void InsertTest()
        {
            Weight weight = new Weight();
            weight.WeightNumberInPounds = 120;
            weight.TimeOfDay = timeOfDay;
            weight.PatientId = patientId;

            int result = WeightManager.Insert(weight);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            List<Weight> weights = WeightManager.Load();

            Weight weight = weights.Where(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId).FirstOrDefault();

            weight.WeightNumberInPounds = 130;

            WeightManager.Update(weight);

            Weight updatedweight = weights.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);

            Assert.AreEqual(weight.WeightNumberInPounds, updatedweight.WeightNumberInPounds);
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<Weight> weights = WeightManager.Load();
            Weight weight = weights.Where(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId).FirstOrDefault();

            int results = WeightManager.Delete(weight.Id);
            Assert.IsTrue(results > 0);
        }
    }
}
