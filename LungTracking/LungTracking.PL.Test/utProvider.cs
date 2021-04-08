using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utProvider
    {
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
            int expected = 50;
            int actual = 0;

            var providers = dc.tblProviders;

            actual = providers.Count();

            Assert.AreEqual(expected, actual);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblProvider newrow = new tblProvider();
            newrow.Id = Guid.NewGuid();
            newrow.FirstName = "newfirstname";
            newrow.LastName = "newlastname";
            newrow.City = "Testopolis";
            newrow.State = "WI";
            newrow.PhoneNumber = "(555)555-2345";
            newrow.JobDescription = "Doctor of Testing";
            newrow.ImagePath = "doctor.jpg";
            newrow.UserId = Guid.Parse("798c8bdc-86ef-4935-ad2f-b8df92eeafc4");

            dc.tblProviders.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblProvider existingrow = dc.tblProviders.FirstOrDefault(r => r.FirstName == "newfirstname");

            if (existingrow != null)
            {
                existingrow.LastName = "updatedlastname";
                dc.SaveChanges();
            }

            tblProvider row = dc.tblProviders.FirstOrDefault(r => r.FirstName == "newfirstname");

            Assert.AreEqual(existingrow.LastName, row.LastName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblProvider row = dc.tblProviders.FirstOrDefault(r => r.FirstName == "newfirstname");

            if (row != null)
            {
                dc.tblProviders.Remove(row);
                dc.SaveChanges();
            }

            tblProvider deletedrow = dc.tblProviders.FirstOrDefault(r => r.FirstName == "newfirstname");

            Assert.IsNull(deletedrow);
        }
    }
}
