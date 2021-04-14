using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

namespace LungTracking.BL.Test
{
    [TestClass]
    public class utFEV1Manager
    {
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            List<FEV1> fev1s = new List<FEV1>();
            fev1s = FEV1Manager.Load();
            int expected = 300;
            Assert.AreEqual(expected, fev1s.Count);
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            List<FEV1> fev1s = new List<FEV1>();
            fev1s = FEV1Manager.LoadByPatientId(patientId);
            Assert.IsNotNull(fev1s);
        }

        [TestMethod]
        public void InsertTest()
        {
            FEV1 fev1 = new FEV1();
            fev1.FEV1Number = 3.3M;
            fev1.BeginningEnd = true;
            fev1.TimeOfDay = timeOfDay;
            fev1.PatientId = patientId;

            int result = FEV1Manager.Insert(fev1);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            List<FEV1> fev1s = FEV1Manager.Load();

            FEV1 fev1 = fev1s.Where(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId).FirstOrDefault();

            fev1.FEV1Number = 4.1M;

            FEV1Manager.Update(fev1);

            FEV1 updatedfev1 = fev1s.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);

            Assert.AreEqual(fev1.FEV1Number, updatedfev1.FEV1Number);
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<FEV1> fev1s = FEV1Manager.Load();
            FEV1 fev1 = fev1s.Where(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId).FirstOrDefault();

            int results = FEV1Manager.Delete(fev1.Id);
            Assert.IsTrue(results > 0);
        }
    }
}
