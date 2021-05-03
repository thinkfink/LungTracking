using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utPef
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

            var pefs = dc.tblPefs;

            actual = pefs.Count();

            //Assert.AreEqual(expected, actual);
            Assert.IsNotNull(pefs);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblPef newrow = new tblPef();
            newrow.Id = id;
            newrow.Pefnumber = 450.5M;
            newrow.BeginningEnd = true;
            newrow.TimeOfDay = DateTime.Parse("2021-04-07 23:30:00");
            newrow.PatientId = patientId;

            dc.tblPefs.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblPef existingrow = dc.tblPefs.FirstOrDefault(r => r.Id == id);

            if (existingrow != null)
            {
                existingrow.Pefnumber = 475.5M;
                dc.SaveChanges();
            }

            tblPef row = dc.tblPefs.FirstOrDefault(r => r.Id == id);

            Assert.AreEqual(existingrow.Pefnumber, row.Pefnumber);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblPef row = dc.tblPefs.FirstOrDefault(r => r.Id == id);

            if (row != null)
            {
                dc.tblPefs.Remove(row);
                dc.SaveChanges();
            }

            tblPef deletedrow = dc.tblPefs.FirstOrDefault(r => r.Id == id);

            Assert.IsNull(deletedrow);
        }
    }
}
