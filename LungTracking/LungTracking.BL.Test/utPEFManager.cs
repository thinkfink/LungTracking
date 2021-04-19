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
    public class utPEFManager
    {
        DateTime timeOfDay = DateTime.Parse("2021-04-14 11:00:00");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                var task = await PEFManager.Load();
                IEnumerable<Models.PEF> pefs = task;
                Assert.AreEqual(300, pefs.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var results = await PEFManager.LoadByPatientId(patientId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                PEF pef = new PEF();
                pef.PEFNumber = 330;
                pef.BeginningEnd = true;
                pef.TimeOfDay = timeOfDay;
                pef.PatientId = patientId;

                int results = await PEFManager.Insert(pef);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = PEFManager.Load();
                IEnumerable<Models.PEF> pefs = task.Result;
                Models.PEF pef = pefs.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);
                pef.PEFNumber = 340.1M;
                var results = PEFManager.Update(pef);
                Assert.IsTrue(results.Result > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = PEFManager.Load();
                IEnumerable<Models.PEF> pefs = task.Result;
                task.Wait();
                Models.PEF pef = pefs.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);
                var results = PEFManager.Delete(pef.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}
