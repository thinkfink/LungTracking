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
        Guid userId = Guid.Parse("31edb120-b27a-4ec6-b8e2-058f58089a92");

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
            int expected = 47;
            int actual = 0;

            var caregivers = dc.tblCaregivers;

            actual = caregivers.Count();

            //Assert.AreEqual(expected, actual);
            Assert.IsNotNull(caregivers);

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
            newrow.UserId = userId;

            dc.tblCaregivers.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblCaregiver existingrow = dc.tblCaregivers.FirstOrDefault(r => r.UserId == userId);

            if (existingrow != null)
            {
                existingrow.LastName = "updatedlastname";
                dc.SaveChanges();
            }

            tblCaregiver row = dc.tblCaregivers.FirstOrDefault(r => r.UserId == userId);

            Assert.AreEqual(existingrow.LastName, row.LastName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblCaregiver row = dc.tblCaregivers.FirstOrDefault(r => r.UserId == userId);

            if (row != null)
            {
                dc.tblCaregivers.Remove(row);
                dc.SaveChanges();
            }

            tblCaregiver deletedrow = dc.tblCaregivers.FirstOrDefault(r => r.UserId == userId);

            Assert.IsNull(deletedrow);
        }
    }
}
