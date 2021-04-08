using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utBloodSugar
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
            int expected = 300;
            int actual = 0;

            var medicationTrackings = dc.tblBloodSugars;

            actual = medicationTrackings.Count();

            Assert.AreEqual(expected, actual);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblBloodSugar newrow = new tblBloodSugar();
            newrow.Id = id;
            newrow.BloodSugarNumber = 120;
            newrow.TimeOfDay = DateTime.Parse("2021-04-07 23:30:00");
            newrow.UnitsOfInsulinGiven = 1;
            newrow.TypeOfInsulinGiven = "A";
            newrow.Notes = "Testing";
            newrow.PatientId = patientId;

            dc.tblBloodSugars.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblBloodSugar existingrow = dc.tblBloodSugars.FirstOrDefault(r => r.Id == id);

            if (existingrow != null)
            {
                existingrow.BloodSugarNumber = 110;
                dc.SaveChanges();
            }

            tblBloodSugar row = dc.tblBloodSugars.FirstOrDefault(r => r.Id == id);

            Assert.AreEqual(existingrow.BloodSugarNumber, row.BloodSugarNumber);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblBloodSugar row = dc.tblBloodSugars.FirstOrDefault(r => r.Id == id);

            if (row != null)
            {
                dc.tblBloodSugars.Remove(row);
                dc.SaveChanges();
            }

            tblBloodSugar deletedrow = dc.tblBloodSugars.FirstOrDefault(r => r.Id == id);

            Assert.IsNull(deletedrow);
        }
    }
}
