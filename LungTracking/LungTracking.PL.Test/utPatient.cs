using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utPatient
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
            int expected = 90;
            int actual = 0;

            var patients = dc.tblPatients;

            actual = patients.Count();

            Assert.AreEqual(expected, actual);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblPatient newrow = new tblPatient();
            newrow.Id = Guid.NewGuid();
            newrow.FirstName = "newfirstname";
            newrow.LastName = "newlastname";
            newrow.Sex = "M";
            newrow.DateOfBirth = DateTime.Parse("1980-01-01");
            newrow.StreetAddress = "123 Test Street";
            newrow.City = "Testopolis";
            newrow.State = "WI";
            newrow.PhoneNumber = "(555)555-2345";
            newrow.UserId = Guid.Parse("a007c7f2-cbd7-48dd-8fd3-adb871b55c25");

            dc.tblPatients.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblPatient existingrow = dc.tblPatients.FirstOrDefault(r => r.FirstName == "newfirstname");

            if (existingrow != null)
            {
                existingrow.LastName = "updatedlastname";
                dc.SaveChanges();
            }

            tblPatient row = dc.tblPatients.FirstOrDefault(r => r.FirstName == "newfirstname");

            Assert.AreEqual(existingrow.LastName, row.LastName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblPatient row = dc.tblPatients.FirstOrDefault(r => r.FirstName == "newfirstname");

            if (row != null)
            {
                dc.tblPatients.Remove(row);
                dc.SaveChanges();
            }

            tblPatient deletedrow = dc.tblPatients.FirstOrDefault(r => r.FirstName == "newfirstname");

            Assert.IsNull(deletedrow);
        }
    }
}
