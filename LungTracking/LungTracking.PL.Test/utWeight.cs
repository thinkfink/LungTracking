using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utWeight
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

            var weights = dc.tblWeights;

            actual = weights.Count();

            Assert.AreEqual(expected, actual);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblWeight newrow = new tblWeight();
            newrow.Id = id;
            newrow.WeightNumberInPounds = 100;
            newrow.TimeOfDay = DateTime.Parse("2021-04-07 23:30:00");
            newrow.PatientId = patientId;

            dc.tblWeights.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblWeight existingrow = dc.tblWeights.FirstOrDefault(r => r.Id == id);

            if (existingrow != null)
            {
                existingrow.WeightNumberInPounds = 110;
                dc.SaveChanges();
            }

            tblWeight row = dc.tblWeights.FirstOrDefault(r => r.Id == id);

            Assert.AreEqual(existingrow.WeightNumberInPounds, row.WeightNumberInPounds);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblWeight row = dc.tblWeights.FirstOrDefault(r => r.Id == id);

            if (row != null)
            {
                dc.tblWeights.Remove(row);
                dc.SaveChanges();
            }

            tblWeight deletedrow = dc.tblWeights.FirstOrDefault(r => r.Id == id);

            Assert.IsNull(deletedrow);
        }
    }
}
