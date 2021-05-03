using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utBloodPressure
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

            var bloodPressures = dc.tblBloodPressures;

            actual = bloodPressures.Count();

            //Assert.AreEqual(expected, actual);
            Assert.IsNotNull(bloodPressures);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblBloodPressure newrow = new tblBloodPressure();
            newrow.Id = id;
            newrow.Bpsystolic = 120;
            newrow.Bpdiastolic = 80;
            newrow.BeginningEnd = true;
            newrow.TimeOfDay = DateTime.Parse("2021-04-07 23:30:00");
            newrow.PatientId = patientId;

            dc.tblBloodPressures.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblBloodPressure existingrow = dc.tblBloodPressures.FirstOrDefault(r => r.Id == id);

            if (existingrow != null)
            {
                existingrow.Bpsystolic = 110;
                dc.SaveChanges();
            }

            tblBloodPressure row = dc.tblBloodPressures.FirstOrDefault(r => r.Id == id);

            Assert.AreEqual(existingrow.Bpsystolic, row.Bpsystolic);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblBloodPressure row = dc.tblBloodPressures.FirstOrDefault(r => r.PatientId == patientId && r.TimeOfDay == DateTime.Parse("2021-04-07 23:30:00"));

            if (row != null)
            {
                dc.tblBloodPressures.Remove(row);
                dc.SaveChanges();
            }

            tblBloodPressure deletedrow = dc.tblBloodPressures.FirstOrDefault(r => r.PatientId == patientId && r.TimeOfDay == DateTime.Parse("2021-04-07 23:30:00"));

            Assert.IsNull(deletedrow);
        }
    }
}
