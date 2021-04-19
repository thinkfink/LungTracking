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
    public class utMedicationDetailsManager
    {
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                var task = await MedicationDetailsManager.Load();
                IEnumerable<Models.MedicationDetails> mdts = task;
                Assert.AreEqual(301, mdts.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByPatientIdTest()
        {
            Task.Run(async () =>
            {
                var results = await MedicationDetailsManager.LoadByPatientId(patientId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
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

                int results = await MedicationDetailsManager.Insert(mdt);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = MedicationDetailsManager.Load();
                IEnumerable<Models.MedicationDetails> mdts = task.Result;
                Models.MedicationDetails mdt = mdts.FirstOrDefault(a => a.MedicationInstructions == "Just testing");
                mdt.MedicationInstructions = "More testing";
                var results = MedicationDetailsManager.Update(mdt);
                Assert.IsTrue(results.Result > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = MedicationDetailsManager.Load();
                IEnumerable<Models.MedicationDetails> mdts = task.Result;
                task.Wait();
                Models.MedicationDetails mdt = mdts.FirstOrDefault(a => a.MedicationInstructions == "More testing");
                var results = MedicationDetailsManager.Delete(mdt.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}