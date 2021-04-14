using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

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
            List<BloodSugar> bss = new List<BloodSugar>();
            bss = BloodSugarManager.Load();
            int expected = 300;
            Assert.AreEqual(expected, bss.Count);
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            List<BloodSugar> bss = new List<BloodSugar>();
            bss = BloodSugarManager.LoadByPatientId(patientId);
            Assert.IsNotNull(bss);
        }

        [TestMethod]
        public void InsertTest()
        {
            BloodSugar bs = new BloodSugar();
            bs.BloodSugarNumber = 120;
            bs.TimeOfDay = timeOfDay;
            bs.UnitsOfInsulinGiven = 1;
            bs.TypeOfInsulinGiven = "A";
            bs.Notes = "Testing";
            bs.PatientId = patientId;

            int result = BloodSugarManager.Insert(bs);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            List<BloodSugar> bss = BloodSugarManager.Load();

            BloodSugar bs = bss.Where(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId).FirstOrDefault();

            bs.Notes = "Additional testing";

            BloodSugarManager.Update(bs);

            BloodSugar updatedbs = bss.FirstOrDefault(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId);

            Assert.AreEqual(bs.Notes, updatedbs.Notes);
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<BloodSugar> bss = BloodSugarManager.Load();
            BloodSugar bs = bss.Where(a => a.TimeOfDay == timeOfDay && a.PatientId == patientId).FirstOrDefault();

            int results = BloodSugarManager.Delete(bs.Id);
            Assert.IsTrue(results > 0);
        }
    }
}
