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
    public static class MedicationTrackingManager
    {
        public async static Task<IEnumerable<Models.MedicationTracking>> Load()
        {
            try
            {
                List<MedicationTracking> medTracking = new List<MedicationTracking>();

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    dc.tblMedicationTrackings
                        .ToList()
                        .ForEach(u => medTracking.Add(new MedicationTracking
                        {
                            Id = u.Id,
                            PillTakenTime = u.PillTakenTime,
                            MedicationId = u.MedicationId,
                            PatientId = u.PatientId
                        }));
                    return medTracking;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<IEnumerable<Models.MedicationTracking>> LoadByPatientId(Guid patientId)
        {
            try
            {
                if (patientId != null)
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {

                        List<MedicationTracking> results = new List<MedicationTracking>();

                        var medTracking = (from dt in dc.tblMedicationTrackings
                                       where dt.PatientId == patientId
                                       select new
                                       {
                                           dt.Id,
                                           dt.PillTakenTime,
                                           dt.MedicationId,
                                           dt.PatientId
                                       }).ToList();

                        if (medTracking != null)
                        {
                            medTracking.ForEach(app => results.Add(new MedicationTracking
                            {
                                Id = app.Id,
                                PillTakenTime = app.PillTakenTime,
                                MedicationId = app.MedicationId,
                                PatientId = app.PatientId
                            }));
                            return results;
                        }
                        else
                        {
                            throw new Exception("MedicationTracking was not found.");
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


        public async static Task<Guid> Insert(DateTime pillTracking, Guid medId, Guid patientId, bool rollback = false)
        {
            try
            {
                Models.MedicationTracking medTracking = new Models.MedicationTracking
                {
                    Id = Guid.NewGuid(),
                    PillTakenTime = pillTracking,
                    MedicationId = medId,
                    PatientId = patientId
                };
                await Insert(medTracking, rollback);
                return medTracking.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Insert(Models.MedicationTracking medTracking, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMedicationTracking newrow = new tblMedicationTracking();

                    newrow.Id = Guid.NewGuid();
                    newrow.PillTakenTime = medTracking.PillTakenTime;
                    newrow.MedicationId = medTracking.MedicationId;
                    newrow.PatientId = medTracking.PatientId;

                    medTracking.Id = newrow.Id;

                    dc.tblMedicationTrackings.Add(newrow);
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

        public async static Task<int> Update(Models.MedicationTracking medTracking, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblMedicationTracking row = (from dt in dc.tblMedicationTrackings where dt.Id == medTracking.Id select dt).FirstOrDefault();
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        row.PillTakenTime = medTracking.PillTakenTime;
                        row.MedicationId = medTracking.MedicationId;
                        row.PatientId = medTracking.PatientId;

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
                    tblMedicationTracking deleterow = (from dt in dc.tblMedicationTrackings where dt.Id == id select dt).FirstOrDefault();
                    dc.tblMedicationTrackings.Remove(deleterow);
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
