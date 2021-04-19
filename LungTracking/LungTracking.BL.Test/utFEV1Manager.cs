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
    public class utFEV1Manager
    {
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                var task = await FEV1Manager.Load();
                IEnumerable<Models.FEV1> fev1s = task;
                Assert.AreEqual(300, fev1s.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var results = await FEV1Manager.LoadByPatientId(patientId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                FEV1 fev1 = new FEV1();
                fev1.FEV1Number = 3.3M;
                fev1.BeginningEnd = true;
                fev1.TimeOfDay = timeOfDay;
                fev1.PatientId = patientId;

                int results = await FEV1Manager.Insert(fev1);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = FEV1Manager.Load();
                IEnumerable<Models.FEV1> fev1s = task.Result;
                Models.FEV1 fev1 = fev1s.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);
                fev1.FEV1Number = 4.1M;
                var results = FEV1Manager.Update(fev1);
                Assert.IsTrue(results.Result > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = FEV1Manager.Load();
                IEnumerable<Models.FEV1> fev1s = task.Result;
                task.Wait();
                Models.FEV1 fev1 = fev1s.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);
                var results = FEV1Manager.Delete(fev1.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}