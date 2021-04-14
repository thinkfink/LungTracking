using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class FEV1Manager
    {
        public static List<FEV1> Load()
        {
            using (LungTrackingEntities dc = new LungTrackingEntities())
            {
                List<FEV1> fev1 = new List<FEV1>();

                dc.tblFev1s
                    .ToList()
                    .ForEach(u => fev1.Add(new FEV1
                    {
                        Id = u.Id,
                        FEV1Number = u.Fev1number,
                        BeginningEnd = u.BeginningEnd,
                        TimeOfDay = u.TimeOfDay,
                        PatientId = u.PatientId
                    }));
                return fev1;
            }
        }
        public static int Insert(int fev1Number, bool beginningEnd, DateTime timeOfDay, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblFev1 newFEV1 = new tblFev1
                    {
                        Id = Guid.NewGuid(),
                        Fev1number = fev1Number,
                        BeginningEnd = beginningEnd,
                        TimeOfDay = timeOfDay,
                        PatientId = patientId
                    };
                    dc.tblFev1s.Add(newFEV1);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(FEV1 fev1)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblFev1 newFEV1 = new tblFev1
                    {
                        Id = Guid.NewGuid(),
                        Fev1number = fev1.FEV1Number,
                        BeginningEnd = fev1.BeginningEnd,
                        TimeOfDay = fev1.TimeOfDay,
                        PatientId = fev1.PatientId
                    };
                    dc.tblFev1s.Add(newFEV1);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Guid id, decimal fev1Number, bool beginningEnd, DateTime timeOfDay, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblFev1 updaterow = (from dt in dc.tblFev1s where dt.Id == id select dt).FirstOrDefault();
                    updaterow.Fev1number = fev1Number;
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

        public static int Update(FEV1 fev1)
        {
            return Update(fev1.Id, fev1.FEV1Number, fev1.BeginningEnd, fev1.TimeOfDay, fev1.PatientId); ;
        }

        public static List<FEV1> LoadByPatientId(Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    List<FEV1> fev1 = new List<FEV1>();

                    var results = (from bs in dc.tblFev1s
                                   where bs.PatientId == patientId
                                   select new
                                   {
                                       bs.Id,
                                       bs.Fev1number,
                                       bs.BeginningEnd,
                                       bs.TimeOfDay,
                                       bs.PatientId
                                   }).ToList();

                    results.ForEach(r => fev1.Add(new FEV1
                    {
                        Id = r.Id,
                        FEV1Number = r.Fev1number,
                        BeginningEnd = r.BeginningEnd,
                        TimeOfDay = r.TimeOfDay,
                        PatientId = r.PatientId
                    }));

                    return fev1;
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
                    tblFev1 deleterow = (from dt in dc.tblFev1s where dt.Id == id select dt).FirstOrDefault();
                    dc.tblFev1s.Remove(deleterow);
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
