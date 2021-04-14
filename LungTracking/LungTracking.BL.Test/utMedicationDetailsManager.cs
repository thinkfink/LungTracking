using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

namespace LungTracking.BL.Test
{
    [TestClass]
    public class utMedicationDetailsManager
    {
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            List<MedicationDetails> mdts = new List<MedicationDetails>();
            mdts = MedicationDetailsManager.Load();
            int expected = 301;
            Assert.AreEqual(expected, mdts.Count);
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            List<MedicationDetails> mdts = new List<MedicationDetails>();
            mdts = MedicationDetailsManager.LoadByPatientId(patientId);
            Assert.IsNotNull(mdts);
        }

        [TestMethod]
        public void InsertTest()
        {
            MedicationDetails mdt = new MedicationDetails();
            mdt.MedicationName = "Testing";
            mdt.MedicationDosageTotal = "100mg";
            mdt.MedicationDosagePerPill = "10mg";
            mdt.MedicationInstructions = "Just testing";
            mdt.NumberOfPills = 10;
            mdt.DateFilled = DateTime.Parse("2021-04-14");
            mdt.QuantityOfFill = 20;
            mdt.RefillDate = DateTime.Parse("2021-05-14");
            mdt.PatientId = patientId;

            int result = MedicationDetailsManager.Insert(mdt);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            List<MedicationDetails> mdts = MedicationDetailsManager.Load();

            MedicationDetails mdt = mdts.Where(a => a.MedicationInstructions == "Just testing").FirstOrDefault();

            mdt.MedicationInstructions = "More testing";

            MedicationDetailsManager.Update(mdt);

            MedicationDetails updatedmdt = mdts.FirstOrDefault(a => a.MedicationName == "Testing");

            Assert.AreEqual(mdt.MedicationInstructions, updatedmdt.MedicationInstructions);
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<MedicationDetails> mdts = MedicationDetailsManager.Load();
            MedicationDetails mdt = mdts.Where(a => a.MedicationInstructions == "More testing").FirstOrDefault();

            int results = MedicationDetailsManager.Delete(mdt.Id);
            Assert.IsTrue(results > 0);
        }
    }
}
