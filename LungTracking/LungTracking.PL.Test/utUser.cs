using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utUser
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
            int expected = 250;
            int actual = 0;

            var users = dc.tblUsers;

            actual = users.Count();

            //Assert.AreEqual(expected, actual);
            Assert.IsNotNull(users);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblUser newrow = new tblUser();
            newrow.Id = Guid.NewGuid();
            newrow.Username = "newusername";
            newrow.Password = "newpassword";
            newrow.Role = 0;
            newrow.Email = "test123@test.com";
            newrow.Created = DateTime.Parse("2021-02-16 10:01:59");
            newrow.LastLogin = DateTime.Parse("2021-02-16 10:05:59");

            dc.tblUsers.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblUser existingrow = dc.tblUsers.FirstOrDefault(r => r.Username == "newusername");

            if (existingrow != null)
            {
                existingrow.Password = "updatedpassword";
                dc.SaveChanges();
            }

            tblUser row = dc.tblUsers.FirstOrDefault(r => r.Username == "newusername");

            Assert.AreEqual(existingrow.Password, row.Password);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblUser row = dc.tblUsers.FirstOrDefault(r => r.Username == "newusername");

            if (row != null)
            {
                dc.tblUsers.Remove(row);
                dc.SaveChanges();
            }

            tblUser deletedrow = dc.tblUsers.FirstOrDefault(r => r.Username == "newusername");

            Assert.IsNull(deletedrow);
        }
    }
}
