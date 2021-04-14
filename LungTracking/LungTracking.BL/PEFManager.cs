using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class PEFManager
    {
        public static List<PEF> Load()
        {
            using (LungTrackingEntities dc = new LungTrackingEntities())
            {
                List<PEF> pefs = new List<PEF>();

                dc.tblPefs
                    .ToList()
                    .ForEach(u => pefs.Add(new PEF
                    {
                        Id = u.Id,
                        PEFNumber = u.Pefnumber,
                        BeginningEnd = u.BeginningEnd,
                        TimeOfDay = u.TimeOfDay,
                        PatientId = u.PatientId
                    }));
                return pefs;
            }
        }
        public static int Insert(int pefNumber, bool beginningEnd, DateTime timeOfDay, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPef newPEF = new tblPef
                    {
                        Id = Guid.NewGuid(),
                        Pefnumber = pefNumber,
                        BeginningEnd = beginningEnd,
                        TimeOfDay = timeOfDay,
                        PatientId = patientId
                    };
                    dc.tblPefs.Add(newPEF);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(PEF pef)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPef newPEF = new tblPef
                    {
                        Id = Guid.NewGuid(),
                        Pefnumber = pef.PEFNumber,
                        BeginningEnd = pef.BeginningEnd,
                        TimeOfDay = pef.TimeOfDay,
                        PatientId = pef.PatientId
                    };
                    dc.tblPefs.Add(newPEF);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Guid id, decimal pefNumber, bool beginningEnd, DateTime timeOfDay, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPef updaterow = (from dt in dc.tblPefs where dt.Id == id select dt).FirstOrDefault();
                    updaterow.Pefnumber = pefNumber;
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

        public static int Update(PEF pef)
        {
            return Update(pef.Id, pef.PEFNumber, pef.BeginningEnd, pef.TimeOfDay, pef.PatientId); ;
        }

        public static List<PEF> LoadByPatientId(Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    List<PEF> pefs = new List<PEF>();

                    var results = (from pef in dc.tblPefs
                                   where pef.PatientId == patientId
                                   select new
                                   {
                                       pef.Id,
                                       pef.Pefnumber,
                                       pef.BeginningEnd,
                                       pef.TimeOfDay,
                                       pef.PatientId
                                   }).ToList();

                    results.ForEach(r => pefs.Add(new PEF
                    {
                        Id = r.Id,
                        PEFNumber = r.Pefnumber,
                        BeginningEnd = r.BeginningEnd,
                        TimeOfDay = r.TimeOfDay,
                        PatientId = r.PatientId
                    }));

                    return pefs;
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
                    tblPef deleterow = (from dt in dc.tblPefs where dt.Id == id select dt).FirstOrDefault();
                    dc.tblPefs.Remove(deleterow);
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
