using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class BloodPressureManager
    {
        public static List<BloodPressure> Load()
        {
            using (LungTrackingEntities dc = new LungTrackingEntities())
            {
                List<BloodPressure> bloodPressure = new List<BloodPressure>();

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
        public static int Insert(int bpSystolic, int bpDiastolic, bool beginningEnd, DateTime timeOfDay, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblBloodPressure newBloodPressure = new tblBloodPressure
                    {
                        Id = Guid.NewGuid(),
                        Bpsystolic = bpSystolic,
                        Bpdiastolic = bpDiastolic,
                        BeginningEnd = beginningEnd,
                        TimeOfDay = timeOfDay,
                        PatientId = patientId
                    };
                    dc.tblBloodPressures.Add(newBloodPressure);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(BloodPressure bloodPressure)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblBloodPressure newBloodPressure = new tblBloodPressure
                    {
                        Id = Guid.NewGuid(),
                        Bpsystolic = bloodPressure.BPsystolic,
                        Bpdiastolic = bloodPressure.BPdiastolic,
                        BeginningEnd = bloodPressure.BeginningEnd,
                        TimeOfDay = bloodPressure.TimeOfDay,
                        PatientId = bloodPressure.PatientId
                    };
                    dc.tblBloodPressures.Add(newBloodPressure);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Guid id, int bpSystolic, int bpDiastolic, bool beginningEnd, DateTime timeOfDay, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblBloodPressure updaterow = (from dt in dc.tblBloodPressures where dt.Id == id select dt).FirstOrDefault();
                    updaterow.Bpsystolic = bpSystolic;
                    updaterow.Bpdiastolic = bpDiastolic;
                    updaterow.BeginningEnd = beginningEnd;
                    updaterow.TimeOfDay = timeOfDay;
                    updaterow.PatientId = patientId;
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Update(BloodPressure bloodPressure)
        {
            return Update(bloodPressure.Id, bloodPressure.BPsystolic, bloodPressure.BPdiastolic, bloodPressure.BeginningEnd, bloodPressure.TimeOfDay, bloodPressure.PatientId);
        }

        public static List<BloodPressure> LoadByPatientId(Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    List<BloodPressure> bloodPressure = new List<BloodPressure>();

                    var results = (from bp in dc.tblBloodPressures
                                   where bp.PatientId == patientId
                                   select new
                                   {
                                       bp.Id,
                                       bp.Bpsystolic,
                                       bp.Bpdiastolic,
                                       bp.BeginningEnd,
                                       bp.TimeOfDay,
                                       bp.PatientId
                                   }).ToList();

                    results.ForEach(r => bloodPressure.Add(new BloodPressure
                    {
                        Id = r.Id,
                        BPsystolic = r.Bpsystolic,
                        BPdiastolic = r.Bpdiastolic,
                        BeginningEnd = r.BeginningEnd,
                        TimeOfDay = r.TimeOfDay,
                        PatientId = r.PatientId
                    }));

                    return bloodPressure;
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
