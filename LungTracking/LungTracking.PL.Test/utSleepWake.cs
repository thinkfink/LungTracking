using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utSleepWake
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

            var sleepWakes = dc.tblSleepWakes;

            actual = sleepWakes.Count();

            //Assert.AreEqual(expected, actual);
            Assert.IsNotNull(sleepWakes);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblSleepWake newrow = new tblSleepWake();
            newrow.Id = id;
            newrow.SleepOrWake = true;
            newrow.TimeOfDay = DateTime.Parse("2021-04-07 23:30:00");
            newrow.PatientId = patientId;

            dc.tblSleepWakes.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblSleepWake existingrow = dc.tblSleepWakes.FirstOrDefault(r => r.Id == id);

            if (existingrow != null)
            {
                existingrow.SleepOrWake = false;
                dc.SaveChanges();
            }

            tblSleepWake row = dc.tblSleepWakes.FirstOrDefault(r => r.Id == id);

            Assert.AreEqual(existingrow.SleepOrWake, row.SleepOrWake);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblSleepWake row = dc.tblSleepWakes.FirstOrDefault(r => r.Id == id);

            if (row != null)
            {
                dc.tblSleepWakes.Remove(row);
                dc.SaveChanges();
            }

            tblSleepWake deletedrow = dc.tblSleepWakes.FirstOrDefault(r => r.Id == id);

            Assert.IsNull(deletedrow);
        }
    }
}
