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
    public class utBloodSugarManager
    {
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                var task = await BloodSugarManager.Load();
                IEnumerable<Models.BloodSugar> bss = task;
                Assert.AreEqual(300, bss.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var results = await BloodSugarManager.LoadByPatientId(patientId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                BloodSugar bs = new BloodSugar();
                bs.BloodSugarNumber = 120;
                bs.TimeOfDay = timeOfDay;
                bs.UnitsOfInsulinGiven = 1;
                bs.TypeOfInsulinGiven = "A";
                bs.Notes = "Testing";
                bs.PatientId = patientId;

                int results = await BloodSugarManager.Insert(bs);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = BloodSugarManager.Load();
                IEnumerable<Models.BloodSugar> bss = task.Result;
                Models.BloodSugar bs = bss.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);
                bs.Notes = "Additional testing";
                var results = BloodSugarManager.Update(bs);
                Assert.IsTrue(results.Result > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = BloodSugarManager.Load();
                IEnumerable<Models.BloodSugar> bss = task.Result;
                task.Wait();
                Models.BloodSugar bs = bss.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);
                var results = BloodSugarManager.Delete(bs.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}