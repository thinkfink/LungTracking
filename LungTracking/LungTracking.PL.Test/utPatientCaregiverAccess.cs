using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utPatientCaregiverAccess
    {
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");
        Guid caregiverId = Guid.Parse("f8cd0f12-04a4-4bb7-afed-425e95018ecc");

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
            int expected = 40;
            int actual = 0;

            var patientCaregiverAccesss = dc.tblPatientCaregiverAccesses;

            actual = patientCaregiverAccesss.Count();

            Assert.AreEqual(expected, actual);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblPatientCaregiverAccess newrow = new tblPatientCaregiverAccess();
            newrow.Id = Guid.NewGuid();
            newrow.PatientId = patientId;
            newrow.CaregiverId = caregiverId;

            dc.tblPatientCaregiverAccesses.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblPatientCaregiverAccess row = dc.tblPatientCaregiverAccesses.FirstOrDefault(r => r.PatientId == patientId && r.CaregiverId == caregiverId);

            if (row != null)
            {
                dc.tblPatientCaregiverAccesses.Remove(row);
                dc.SaveChanges();
            }

            tblPatientCaregiverAccess deletedrow = dc.tblPatientCaregiverAccesses.FirstOrDefault(r => r.PatientId == patientId && r.CaregiverId == caregiverId);

            Assert.IsNull(deletedrow);
        }
    }
}
