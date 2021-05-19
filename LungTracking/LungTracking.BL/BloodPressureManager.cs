using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LungTracking.BL
{
    public static class BloodPressureManager
    {
        public async static Task<IEnumerable<Models.BloodPressure>> Load()
        {
            try
            {
                List<BloodPressure> bloodPressure = new List<BloodPressure>();
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        dc.tblBloodPressures
                            .ToList()
                            .ForEach(u => bloodPressure.Add(new BloodPressure
                            {
                                Id = u.Id,
                                BPsystolic = u.Bpsystolic,
                                BPdiastolic = u.Bpdiastolic,
                                BeginningEnd = u.BeginningEnd,
                                TimeOfDay = u.TimeOfDay,
                                PatientId = u.PatientId
                            }));
                    }
                });
                return bloodPressure;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<IEnumerable<Models.BloodPressure>> LoadByPatientId(Guid patientId)
        {
            try
            {
                List<BloodPressure> results = new List<BloodPressure>();
                await Task.Run(() =>
                {
                    if (patientId != null)
                    {
                        using (LungTrackingEntities dc = new LungTrackingEntities())
                        {
                            var bloodPressure = (from dt in dc.tblBloodPressures
                                                 where dt.PatientId == patientId
                                                 select new
                                                 {
                                                     dt.Id,
                                                     dt.Bpsystolic,
                                                     dt.Bpdiastolic,
                                                     dt.BeginningEnd,
                                                     dt.TimeOfDay,
                                                     dt.PatientId
                                                 }).ToList();

                            if (bloodPressure != null)
                            {
                                bloodPressure.ForEach(a => results.Add(new BloodPressure
                                {
                                    Id = a.Id,
                                    BPsystolic = a.Bpsystolic,
                                    BPdiastolic = a.Bpdiastolic,
                                    BeginningEnd = a.BeginningEnd,
                                    TimeOfDay = a.TimeOfDay,
                                    PatientId = a.PatientId
                                }));
                            }
                            else
                            {
                                throw new Exception("BloodPressure was not found.");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Please provide a patient Id.");
                    }
                });
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async static Task<Guid> Insert(int bpSystolic, int bpDiastolic, bool beginningEnd, DateTime timeOfDay, Guid patientId, bool rollback = false)
        {
            try
            {
                Models.BloodPressure bloodPressure = new Models.BloodPressure
                {
                    Id = Guid.NewGuid(),
                    BPsystolic = bpSystolic,
                    BPdiastolic = bpDiastolic,
                    BeginningEnd = beginningEnd,
                    TimeOfDay = timeOfDay,
                    PatientId = patientId
                };
                await Insert(bloodPressure, rollback);
                return bloodPressure.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Insert(Models.BloodPressure bloodPressure, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                int results = 0;
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        tblBloodPressure newrow = new tblBloodPressure();

                        newrow.Id = Guid.NewGuid();
                        newrow.Bpsystolic = bloodPressure.BPsystolic;
                        newrow.Bpdiastolic = bloodPressure.BPdiastolic;
                        newrow.BeginningEnd = bloodPressure.BeginningEnd;
                        newrow.TimeOfDay = bloodPressure.TimeOfDay;
                        newrow.PatientId = bloodPressure.PatientId;

                        bloodPressure.Id = newrow.Id;

                        dc.tblBloodPressures.Add(newrow);
                        int results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                });
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Update(Models.BloodPressure bloodPressure, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                int results = 0;
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblBloodPressure row = (from dt in dc.tblBloodPressures where dt.Id == bloodPressure.Id select dt).FirstOrDefault();
                        int results = 0;
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            row.Bpsystolic = bloodPressure.BPsystolic;
                            row.Bpdiastolic = bloodPressure.BPdiastolic;
                            row.BeginningEnd = bloodPressure.BeginningEnd;
                            row.TimeOfDay = bloodPressure.TimeOfDay;
                            row.PatientId = bloodPressure.PatientId;

                            results = dc.SaveChanges();
                            if (rollback) transaction.Rollback();
                        }
                        else
                        {
                            throw new Exception("Row was not found");
                        }
                    }
                });
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Delete(Guid id, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                int results = 0;
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblBloodPressure row = dc.tblBloodPressures.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.tblBloodPressures.Remove(row);

                            results = dc.SaveChanges();
                            if (rollback) transaction.Rollback();
                        }
                        else
                        {
                            throw new Exception("Row was not found.");
                        }
                    }
                });
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // stored procedure
        public static void CalcMAP(BL.Models.BloodPressure bloodPressure)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    var parameterBPsystolic = new SqlParameter
                    {
                        ParameterName = "BPsystolic",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Value = bloodPressure.BPsystolic
                    };

                    var parameterBPdiastolic = new SqlParameter
                    {
                        ParameterName = "BPdiastolic",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Value = bloodPressure.BPdiastolic
                    };

                    var results = dc.Set<spCalcMAPResult>().FromSqlRaw("exec spCalcMAP @BPsystolic, @BPdiastolic", parameterBPsystolic, parameterBPdiastolic).ToList();

                    foreach (var r in results)
                    {
                        bloodPressure.MAP = r.MAP;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
