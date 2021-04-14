using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class MedicationTimeManager
    {
        public static List<MedicationTime> Load()
        {
            using (LungTrackingEntities dc = new LungTrackingEntities())
            {
                List<MedicationTime> medTime = new List<MedicationTime>();

                dc.tblMedicationTimes
                    .ToList()
                    .ForEach(u => medTime.Add(new MedicationTime
                    {
                        Id = u.Id,
                        PillTime = u.PillTime,
                        MedicationId = u.MedicationId,
                        PatientId = u.PatientId
                    }));
                return medTime;
            }
        }
        public static int Insert(TimeSpan pillTime, Guid medId, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblMedicationTime newMedTime = new tblMedicationTime
                    {
                        Id = Guid.NewGuid(),
                        PillTime = pillTime,
                        MedicationId = medId,
                        PatientId = patientId
                    };
                    dc.tblMedicationTimes.Add(newMedTime);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(MedicationTime medTime)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblMedicationTime newMedTime = new tblMedicationTime
                    {
                        Id = Guid.NewGuid(),
                        PillTime = medTime.PillTime,
                        MedicationId = medTime.MedicationId,
                        PatientId = medTime.PatientId
                    };
                    dc.tblMedicationTimes.Add(newMedTime);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Guid id, TimeSpan pillTime, Guid medId, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblMedicationTime updaterow = (from dt in dc.tblMedicationTimes where dt.Id == id select dt).FirstOrDefault();
                    updaterow.PillTime = pillTime;
                    updaterow.MedicationId = medId;
                    updaterow.PatientId = patientId;
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Update(MedicationTime medTime)
        {
            return Update(medTime.Id, medTime.PillTime, medTime.MedicationId, medTime.PatientId);
        }

        public static List<MedicationTime> LoadByPatientId(Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    List<MedicationTime> medTime = new List<MedicationTime>();

                    var results = (from mdt in dc.tblMedicationTimes
                                   where mdt.PatientId == patientId
                                   select new
                                   {
                                       mdt.Id,
                                       mdt.PillTime,
                                       mdt.MedicationId,
                                       mdt.PatientId
                                   }).ToList();

                    results.ForEach(r => medTime.Add(new MedicationTime
                    {
                        Id = r.Id,
                        PillTime = r.PillTime,
                        MedicationId = r.MedicationId,
                        PatientId = r.PatientId
                    }));

                    return medTime;
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
                    tblMedicationTime deleterow = (from dt in dc.tblMedicationTimes where dt.Id == id select dt).FirstOrDefault();
                    dc.tblMedicationTimes.Remove(deleterow);
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
