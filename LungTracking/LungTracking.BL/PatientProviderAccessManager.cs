using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class PatientProviderAccessManager
    {
        public static List<PatientProviderAccess> Load()
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    List<PatientProviderAccess> patientProviderAccess = new List<PatientProviderAccess>();
                    foreach (tblPatientProviderAccess dt in dc.tblPatientProviderAccesses)
                    {
                        patientProviderAccess.Add(new PatientProviderAccess
                        {
                            Id = dt.Id,
                            PatientId = dt.PatientId,
                            ProviderId = dt.ProviderId
                        });
                    }
                    return patientProviderAccess;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static PatientProviderAccess LoadById(Guid patientId, Guid providerId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {

                    tblPatientProviderAccess row = (from dt in dc.tblPatientProviderAccesses where dt.PatientId == patientId && dt.ProviderId == providerId select dt).FirstOrDefault();

                    if (row != null)
                    {
                        return new PatientProviderAccess
                        {
                            Id = row.Id,
                            PatientId = row.PatientId,
                            ProviderId = row.ProviderId
                        };
                    }
                    else
                    {
                        throw new Exception("PatientProviderAccess was not found.");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(PatientProviderAccess patientProviderAccess)
        {
            Guid id;
            int result = Insert(out id, patientProviderAccess.PatientId, patientProviderAccess.ProviderId);
            patientProviderAccess.Id = id;
            return result;
        }

        public static int Insert(out Guid id, Guid patientId, Guid providerId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPatientProviderAccess newrow = new tblPatientProviderAccess();

                    id = Guid.NewGuid();
                    newrow.Id = id;
                    newrow.PatientId = patientId;
                    newrow.ProviderId = providerId;
                    dc.tblPatientProviderAccesses.Add(newrow);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Delete(Guid patientId, Guid providerId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPatientProviderAccess deleterow = (from dt in dc.tblPatientProviderAccesses where dt.PatientId == patientId && dt.ProviderId == providerId select dt).FirstOrDefault();
                    dc.tblPatientProviderAccesses.Remove(deleterow);
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
