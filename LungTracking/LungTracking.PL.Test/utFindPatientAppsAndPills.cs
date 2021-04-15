using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.PL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LungTracking.PL.Test
{
    [TestClass]
    public class utFindPatientAppsAndPills
    {
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
        public void FindAllPatientDataTest()
        {
            var parameterId = new SqlParameter
            {
                ParameterName = "Id",
                SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                Value = patientId
            };

            var results = dc.Set<spFindPatientAppsAndPills>().FromSqlRaw("exec spFindPatientAppsAndPills @Id", parameterId).ToList();

            Assert.IsNotNull(results);
        }
    }
}
