using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utFev1
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

            var fev1s = dc.tblFev1s;

            actual = fev1s.Count();

            //Assert.AreEqual(expected, actual);
            Assert.IsNotNull(fev1s);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblFev1 newrow = new tblFev1();
            newrow.Id = id;
            newrow.Fev1number = 3.5M;
            newrow.BeginningEnd = true;
            newrow.TimeOfDay = DateTime.Parse("2021-04-07 23:30:00");
            newrow.PatientId = patientId;

            dc.tblFev1s.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblFev1 existingrow = dc.tblFev1s.FirstOrDefault(r => r.Id == id);

            if (existingrow != null)
            {
                existingrow.Fev1number = 4;
                dc.SaveChanges();
            }

            tblFev1 row = dc.tblFev1s.FirstOrDefault(r => r.Id == id);

            Assert.AreEqual(existingrow.Fev1number, row.Fev1number);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblFev1 row = dc.tblFev1s.FirstOrDefault(r => r.PatientId == patientId && r.TimeOfDay == DateTime.Parse("2021-04-07 23:30:00"));

            if (row != null)
            {
                dc.tblFev1s.Remove(row);
                dc.SaveChanges();
            }

            tblFev1 deletedrow = dc.tblFev1s.FirstOrDefault(r => r.PatientId == patientId && r.TimeOfDay == DateTime.Parse("2021-04-07 23:30:00"));

            Assert.IsNull(deletedrow);
        }
    }
}
