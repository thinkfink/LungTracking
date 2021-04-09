using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class PatientCaregiverAccessManager
    {
        public static List<PatientCaregiverAccess> Load()
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    List<PatientCaregiverAccess> patientCaregiverAccess = new List<PatientCaregiverAccess>();
                    foreach (tblPatientCaregiverAccess dt in dc.tblPatientCaregiverAccesses)
                    {
                        patientCaregiverAccess.Add(new PatientCaregiverAccess
                        {
                            Id = dt.Id,
                            PatientId = dt.PatientId,
                            CaregiverId = dt.CaregiverId
                        });
                    }
                    return patientCaregiverAccess;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static PatientCaregiverAccess LoadById(Guid patientId, Guid caregiverId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {

                    tblPatientCaregiverAccess row = (from dt in dc.tblPatientCaregiverAccesses where dt.PatientId == patientId && dt.CaregiverId == caregiverId select dt).FirstOrDefault();

                    if (row != null)
                    {
                        return new PatientCaregiverAccess
                        {
                            Id = row.Id,
                            PatientId = row.PatientId,
                            CaregiverId = row.CaregiverId
                        };
                    }
                    else
                    {
                        throw new Exception("PatientCaregiverAccess was not found.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Insert(PatientCaregiverAccess patientCaregiverAccess)
        {
            Guid id;
            int result = Insert(out id, patientCaregiverAccess.PatientId, patientCaregiverAccess.CaregiverId);
            patientCaregiverAccess.Id = id;
            return result;
        }

        public static int Insert(out Guid id, Guid patientId, Guid caregiverId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPatientCaregiverAccess newrow = new tblPatientCaregiverAccess();

                    id = Guid.NewGuid();
                    newrow.Id = id;
                    newrow.PatientId = patientId;
                    newrow.CaregiverId = caregiverId;
                    dc.tblPatientCaregiverAccesses.Add(newrow);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Delete(Guid patientId, Guid caregiverId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPatientCaregiverAccess deleterow = (from dt in dc.tblPatientCaregiverAccesses where dt.PatientId == patientId && dt.CaregiverId == caregiverId select dt).FirstOrDefault();
                    dc.tblPatientCaregiverAccesses.Remove(deleterow);
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
