using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utTemperature
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

            var temperatures = dc.tblTemperatures;

            actual = temperatures.Count();

            Assert.AreEqual(expected, actual);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblTemperature newrow = new tblTemperature();
            newrow.Id = id;
            newrow.TempNumber = 98.6M;
            newrow.BeginningEnd = true;
            newrow.TimeOfDay = DateTime.Parse("2021-04-07 23:30:00");
            newrow.PatientId = patientId;

            dc.tblTemperatures.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblTemperature existingrow = dc.tblTemperatures.FirstOrDefault(r => r.Id == id);

            if (existingrow != null)
            {
                existingrow.TempNumber = 98.7M;
                dc.SaveChanges();
            }

            tblTemperature row = dc.tblTemperatures.FirstOrDefault(r => r.Id == id);

            Assert.AreEqual(existingrow.TempNumber, row.TempNumber);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblTemperature row = dc.tblTemperatures.FirstOrDefault(r => r.Id == id);

            if (row != null)
            {
                dc.tblTemperatures.Remove(row);
                dc.SaveChanges();
            }

            tblTemperature deletedrow = dc.tblTemperatures.FirstOrDefault(r => r.Id == id);

            Assert.IsNull(deletedrow);
        }
    }
}
