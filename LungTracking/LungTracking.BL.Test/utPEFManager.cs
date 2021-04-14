using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

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
            List<PEF> pefs = new List<PEF>();
            pefs = PEFManager.Load();
            int expected = 300;
            Assert.AreEqual(expected, pefs.Count);
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            List<PEF> pefs = new List<PEF>();
            pefs = PEFManager.LoadByPatientId(patientId);
            Assert.IsNotNull(pefs);
        }

        [TestMethod]
        public void InsertTest()
        {
            PEF pef = new PEF();
            pef.PEFNumber = 330;
            pef.BeginningEnd = true;
            pef.TimeOfDay = timeOfDay;
            pef.PatientId = patientId;

            int result = PEFManager.Insert(pef);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            List<PEF> pefs = PEFManager.Load();

            PEF pef = pefs.Where(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId).FirstOrDefault();

            pef.PEFNumber = 340.1M;

            PEFManager.Update(pef);

            PEF updatedpef = pefs.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);

            Assert.AreEqual(pef.PEFNumber, updatedpef.PEFNumber);
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<PEF> pefs = PEFManager.Load();
            PEF pef = pefs.Where(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId).FirstOrDefault();

            int results = PEFManager.Delete(pef.Id);
            Assert.IsTrue(results > 0);
        }
    }
}
