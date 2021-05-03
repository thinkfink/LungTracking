using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utMedicationDetails
    {
        Guid id = Guid.NewGuid();
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
            int expected = 301;
            int actual = 0;

            var medicationDetails = dc.tblMedicationDetails;

            actual = medicationDetails.Count();

            //Assert.AreEqual(expected, actual);
            Assert.IsNotNull(medicationDetails);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblMedicationDetail newrow = new tblMedicationDetail();
            newrow.Id = id;
            newrow.MedicationName = "Testofen";
            newrow.MedicationDosageTotal = "200mg";
            newrow.MedicationDosagePerPill = "100mg";
            newrow.MedicationInstructions = "Take two pills daily";
            newrow.NumberOfPills = 200;
            newrow.DateFilled = DateTime.Parse("2021-04-01");
            newrow.QuantityOfFill = 200;
            newrow.RefillDate = DateTime.Parse("2021-05-01");
            newrow.PatientId = patientId;

            dc.tblMedicationDetails.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblMedicationDetail existingrow = dc.tblMedicationDetails.FirstOrDefault(r => r.Id == id);

            if (existingrow != null)
            {
                existingrow.MedicationInstructions = "Updated take three pills daily";
                dc.SaveChanges();
            }

            tblMedicationDetail row = dc.tblMedicationDetails.FirstOrDefault(r => r.Id == id);

            Assert.AreEqual(existingrow.MedicationInstructions, row.MedicationInstructions);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblMedicationDetail row = dc.tblMedicationDetails.FirstOrDefault(r => r.Id == id);

            if (row != null)
            {
                dc.tblMedicationDetails.Remove(row);
                dc.SaveChanges();
            }

            tblMedicationDetail deletedrow = dc.tblMedicationDetails.FirstOrDefault(r => r.Id == id);

            Assert.IsNull(deletedrow);
        }
    }
}
