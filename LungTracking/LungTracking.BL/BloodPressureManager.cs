using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;
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
                    return bloodPressure;
                }
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
                if (patientId != null)
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {

                        List<BloodPressure> results = new List<BloodPressure>();

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
                            bloodPressure.ForEach(app => results.Add(new BloodPressure
                            {
                                Id = app.Id,
                                BPsystolic = app.Bpsystolic,
                                BPdiastolic = app.Bpdiastolic,
                                BeginningEnd = app.BeginningEnd,
                                TimeOfDay = app.TimeOfDay,
                                PatientId = app.PatientId
                            }));
                            return results;
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

                    return results;
                }
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
                        return results;
                    }
                    else
                    {
                        throw new Exception("Row was not found");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Delete(Guid id)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblBloodPressure deleterow = (from dt in dc.tblBloodPressures where dt.Id == id select dt).FirstOrDefault();
                    dc.tblBloodPressures.Remove(deleterow);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
