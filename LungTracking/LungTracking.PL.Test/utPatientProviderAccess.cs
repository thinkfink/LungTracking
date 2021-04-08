using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LungTracking.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utPatientProviderAccess
    {
        Guid patientId = Guid.Parse("dbb77cb3-c7b1-42d5-ae1e-0e2c66fc0c4c");
        Guid providerId = Guid.Parse("bf27f470-06f9-418d-bde9-18be4c4c11cd");

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

            var patientProviderAccesss = dc.tblPatientProviderAccesses;

            actual = patientProviderAccesss.Count();

            Assert.AreEqual(expected, actual);

            dc = null;
        }

        [TestMethod]
        public void InsertTest()
        {
            tblPatientProviderAccess newrow = new tblPatientProviderAccess();
            newrow.Id = Guid.NewGuid();
            newrow.PatientId = patientId;
            newrow.ProviderId = providerId;

            dc.tblPatientProviderAccesses.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            InsertTest();

            tblPatientProviderAccess row = dc.tblPatientProviderAccesses.FirstOrDefault(r => r.PatientId == patientId && r.ProviderId == providerId);

            if (row != null)
            {
                dc.tblPatientProviderAccesses.Remove(row);
                dc.SaveChanges();
            }

            tblPatientProviderAccess deletedrow = dc.tblPatientProviderAccesses.FirstOrDefault(r => r.PatientId == patientId && r.ProviderId == providerId);

            Assert.IsNull(deletedrow);
        }
    }
}
