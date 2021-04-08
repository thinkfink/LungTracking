using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utCaregiver
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
            int expected = 46;
            int actual = 0;

            var caregivers = dc.tblCaregivers;

            actual = caregivers.Count();

            Assert.AreEqual(expected, actual);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblCaregiver newrow = new tblCaregiver();
            newrow.Id = Guid.NewGuid();
            newrow.FirstName = "newfirstname";
            newrow.LastName = "newlastname";
            newrow.City = "Testopolis";
            newrow.State = "WI";
            newrow.PhoneNumber = "(555)555-2345";
            newrow.UserId = Guid.Parse("31edb120-b27a-4ec6-b8e2-058f58089a92");

            dc.tblCaregivers.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblCaregiver existingrow = dc.tblCaregivers.FirstOrDefault(r => r.FirstName == "newfirstname");

            if (existingrow != null)
            {
                existingrow.LastName = "updatedlastname";
                dc.SaveChanges();
            }

            tblCaregiver row = dc.tblCaregivers.FirstOrDefault(r => r.FirstName == "newfirstname");

            Assert.AreEqual(existingrow.LastName, row.LastName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblCaregiver row = dc.tblCaregivers.FirstOrDefault(r => r.FirstName == "newfirstname");

            if (row != null)
            {
                dc.tblCaregivers.Remove(row);
                dc.SaveChanges();
            }

            tblCaregiver deletedrow = dc.tblCaregivers.FirstOrDefault(r => r.FirstName == "newfirstname");

            Assert.IsNull(deletedrow);
        }
    }
}
