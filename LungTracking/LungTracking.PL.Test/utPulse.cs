using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utPulse
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

            var pulses = dc.tblPulses;

            actual = pulses.Count();

            Assert.AreEqual(expected, actual);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblPulse newrow = new tblPulse();
            newrow.Id = id;
            newrow.PulseNumber = 100;
            newrow.BeginningEnd = true;
            newrow.TimeOfDay = DateTime.Parse("2021-04-07 23:30:00");
            newrow.PatientId = patientId;

            dc.tblPulses.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblPulse existingrow = dc.tblPulses.FirstOrDefault(r => r.Id == id);

            if (existingrow != null)
            {
                existingrow.PulseNumber = 110;
                dc.SaveChanges();
            }

            tblPulse row = dc.tblPulses.FirstOrDefault(r => r.Id == id);

            Assert.AreEqual(existingrow.PulseNumber, row.PulseNumber);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblPulse row = dc.tblPulses.FirstOrDefault(r => r.Id == id);

            if (row != null)
            {
                dc.tblPulses.Remove(row);
                dc.SaveChanges();
            }

            tblPulse deletedrow = dc.tblPulses.FirstOrDefault(r => r.Id == id);

            Assert.IsNull(deletedrow);
        }
    }
}
