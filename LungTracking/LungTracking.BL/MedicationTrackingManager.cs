using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class MedicationTrackingManager
    {
        public static List<MedicationTracking> Load()
        {
            using (LungTrackingEntities dc = new LungTrackingEntities())
            {
                List<MedicationTracking> medTracking = new List<MedicationTracking>();

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
        public static int Insert(DateTime pillTakenTime, Guid medId, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblMedicationTracking newMedTracking = new tblMedicationTracking
                    {
                        Id = Guid.NewGuid(),
                        PillTakenTime = pillTakenTime,
                        MedicationId = medId,
                        PatientId = patientId
                    };
                    dc.tblMedicationTrackings.Add(newMedTracking);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Insert(MedicationTracking medTracking)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblMedicationTracking newMedTracking = new tblMedicationTracking
                    {
                        Id = Guid.NewGuid(),
                        PillTakenTime = medTracking.PillTakenTime,
                        MedicationId = medTracking.MedicationId,
                        PatientId = medTracking.PatientId
                    };
                    dc.tblMedicationTrackings.Add(newMedTracking);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Guid id, DateTime pillTakenTime, Guid medId, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblMedicationTracking updaterow = (from dt in dc.tblMedicationTrackings where dt.Id == id select dt).FirstOrDefault();
                    updaterow.PillTakenTime = pillTakenTime;
                    updaterow.MedicationId = medId;
                    updaterow.PatientId = patientId;
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Update(MedicationTracking medTracking)
        {
            return Update(medTracking.Id, medTracking.PillTakenTime, medTracking.MedicationId, medTracking.PatientId);
        }

        public static List<MedicationTracking> LoadByPatientId(Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    List<MedicationTracking> medTracking = new List<MedicationTracking>();

                    var results = (from mdt in dc.tblMedicationTrackings
                                   where mdt.PatientId == patientId
                                   select new
                                   {
                                       mdt.Id,
                                       mdt.PillTakenTime,
                                       mdt.MedicationId,
                                       mdt.PatientId
                                   }).ToList();

                    results.ForEach(r => medTracking.Add(new MedicationTracking
                    {
                        Id = r.Id,
                        PillTakenTime = r.PillTakenTime,
                        MedicationId = r.MedicationId,
                        PatientId = r.PatientId
                    }));

                    return medTracking;
                }
            }
            catch (Exception ex)
            {

                throw ex;
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
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
