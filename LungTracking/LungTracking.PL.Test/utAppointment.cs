using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utAppointment
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

            var appointments = dc.tblAppointments;

            actual = appointments.Count();

            Assert.AreEqual(expected, actual);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblAppointment newrow = new tblAppointment();
            newrow.Id = id;
            newrow.Date = DateTime.Parse("2020-04-10");
            newrow.TimeStart = TimeSpan.Parse("11:00:00");
            newrow.TimeEnd = TimeSpan.Parse("12:00:00");
            newrow.Description = "Doing some testing";
            newrow.Location = "Test Place";
            newrow.PatientId = patientId;

            dc.tblAppointments.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblAppointment existingrow = dc.tblAppointments.FirstOrDefault(r => r.Id == id);

            if (existingrow != null)
            {
                existingrow.Location = "Updated Test Place";
                dc.SaveChanges();
            }

            tblAppointment row = dc.tblAppointments.FirstOrDefault(r => r.Id == id);

            Assert.AreEqual(existingrow.Location, row.Location);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblAppointment row = dc.tblAppointments.FirstOrDefault(r => r.Id == id);

            if (row != null)
            {
                dc.tblAppointments.Remove(row);
                dc.SaveChanges();
            }

            tblAppointment deletedrow = dc.tblAppointments.FirstOrDefault(r => r.Id == id);

            Assert.IsNull(deletedrow);
        }
    }
}
