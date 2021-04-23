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
    public class utCalcMAP
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
        public void CalcMAPTest()
        {
            // Test the call with 140 systolic and 80 diastolic. Should result in 100

            double expected = 100;
            double actual = 0;

            var parameterBPsystolic = new SqlParameter
            {
                ParameterName = "BPsystolic",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = 140
            };

            var parameterBPdiastolic = new SqlParameter
            {
                ParameterName = "BPdiastolic",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = 80
            };

            var results = dc.Set<spCalcMAPResult>().FromSqlRaw("exec spCalcMAP @BPsystolic, @BPdiastolic", parameterBPsystolic, parameterBPdiastolic).ToList();

            foreach(var r in results)
            {
                actual = r.MAP;
            }

            Assert.AreEqual(expected, actual);
        }
    }
}
