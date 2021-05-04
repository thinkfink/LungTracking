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
    public static class MedicationTimeManager
    {
        public async static Task<IEnumerable<Models.MedicationTime>> Load()
        {
            
            try
            {
                List<MedicationTime> medTime = new List<MedicationTime>();
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        dc.tblMedicationTimes
                            .ToList()
                            .ForEach(u => medTime.Add(new MedicationTime
                            {
                                Id = u.Id,
                                PillTime = u.PillTime,
                                MedicationId = u.MedicationId,
                                PatientId = u.PatientId
                            }));
                    }
                });
                return medTime;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<IEnumerable<Models.MedicationTime>> LoadByPatientId(Guid patientId)
        {
            try
            {
                List<MedicationTime> results = new List<MedicationTime>();
                await Task.Run(() =>
                {
                    if (patientId != null)
                    {
                        using (LungTrackingEntities dc = new LungTrackingEntities())
                        {
                            var medTime = (from dt in dc.tblMedicationTimes
                                           where dt.PatientId == patientId
                                           select new
                                           {
                                               dt.Id,
                                               dt.PillTime,
                                               dt.MedicationId,
                                               dt.PatientId
                                           }).ToList();

                            if (medTime != null)
                            {
                                medTime.ForEach(app => results.Add(new MedicationTime
                                {
                                    Id = app.Id,
                                    PillTime = app.PillTime,
                                    MedicationId = app.MedicationId,
                                    PatientId = app.PatientId
                                }));
                            }
                            else
                            {
                                throw new Exception("MedicationTime was not found.");
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


        public async static Task<Guid> Insert(DateTime pillTime, Guid medId, Guid patientId, bool rollback = false)
        {
            try
            {
                Models.MedicationTime medTime = new Models.MedicationTime
                {
                    Id = Guid.NewGuid(),
                    PillTime = pillTime,
                    MedicationId = medId,
                    PatientId = patientId
                };
                await Insert(medTime, rollback);
                return medTime.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Insert(Models.MedicationTime medTime, bool rollback = false)
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

                        tblMedicationTime newrow = new tblMedicationTime();

                        newrow.Id = Guid.NewGuid();
                        newrow.PillTime = medTime.PillTime;
                        newrow.MedicationId = medTime.MedicationId;
                        newrow.PatientId = medTime.PatientId;

                        medTime.Id = newrow.Id;

                        dc.tblMedicationTimes.Add(newrow);
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

        public async static Task<int> Update(Models.MedicationTime medTime, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                int results = 0;
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblMedicationTime row = (from dt in dc.tblMedicationTimes where dt.Id == medTime.Id select dt).FirstOrDefault();

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            row.PillTime = medTime.PillTime;
                            row.MedicationId = medTime.MedicationId;
                            row.PatientId = medTime.PatientId;

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
                        tblMedicationTime row = dc.tblMedicationTimes.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.tblMedicationTimes.Remove(row);

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
    }
}
