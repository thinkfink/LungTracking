using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utMedicationTracking
    {
        Guid id = Guid.NewGuid();
        Guid medicationId = Guid.Parse("aa596935-5d6f-4a1d-940e-3c3afb6fd9c7");
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");

        protected LungTrackingEntities dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void Initialize()
        {
            dc = new LungTrackingEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void TransactionCleanUp()
        {
            transaction.Rollback();
            transaction.Dispose();
        }

        [TestMethod]
        public void LoadTest()
        {
            int expected = 300;
            int actual = 0;

            var medicationTrackings = dc.tblMedicationTrackings;

            actual = medicationTrackings.Count();

            Assert.AreEqual(expected, actual);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblMedicationTracking newrow = new tblMedicationTracking();
            newrow.Id = id;
            newrow.PillTakenTime = DateTime.Parse("2020-04-10 12:00:00");
            newrow.MedicationId = medicationId;
            newrow.PatientId = patientId;

            dc.tblMedicationTrackings.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblMedicationTracking existingrow = dc.tblMedicationTrackings.FirstOrDefault(r => r.Id == id);

            if (existingrow != null)
            {
                existingrow.PillTakenTime = DateTime.Parse("2020-04-10 13:00:00");
                dc.SaveChanges();
            }

            tblMedicationTracking row = dc.tblMedicationTrackings.FirstOrDefault(r => r.Id == id);

            Assert.AreEqual(existingrow.PillTakenTime, row.PillTakenTime);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblMedicationTracking row = dc.tblMedicationTrackings.FirstOrDefault(r => r.Id == id);

            if (row != null)
            {
                dc.tblMedicationTrackings.Remove(row);
                dc.SaveChanges();
            }

            tblMedicationTracking deletedrow = dc.tblMedicationTrackings.FirstOrDefault(r => r.Id == id);

            Assert.IsNull(deletedrow);
        }
    }
}
