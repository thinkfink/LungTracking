using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class SleepWakeManager
    {
        public static List<SleepWake> Load()
        {
            using (LungTrackingEntities dc = new LungTrackingEntities())
            {
                List<SleepWake> sws = new List<SleepWake>();

                dc.tblSleepWakes
                    .ToList()
                    .ForEach(u => sws.Add(new SleepWake
                    {
                        Id = u.Id,
                        SleepOrWake = u.SleepOrWake,
                        TimeOfDay = u.TimeOfDay,
                        PatientId = u.PatientId
                    }));
                return sws;
            }
        }
        public static int Insert(bool sleepOrWake, DateTime timeOfDay, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblSleepWake newSleepWake = new tblSleepWake
                    {
                        Id = Guid.NewGuid(),
                        SleepOrWake = sleepOrWake,
                        TimeOfDay = timeOfDay,
                        PatientId = patientId
                    };
                    dc.tblSleepWakes.Add(newSleepWake);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(SleepWake sw)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblSleepWake newSleepWake = new tblSleepWake
                    {
                        Id = Guid.NewGuid(),
                        SleepOrWake = sw.SleepOrWake,
                        TimeOfDay = sw.TimeOfDay,
                        PatientId = sw.PatientId
                    };
                    dc.tblSleepWakes.Add(newSleepWake);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Guid id, bool sleepOrWake, DateTime timeOfDay, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblSleepWake updaterow = (from dt in dc.tblSleepWakes where dt.Id == id select dt).FirstOrDefault();
                    updaterow.SleepOrWake = sleepOrWake;
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

        public static int Update(SleepWake sw)
        {
            return Update(sw.Id, sw.SleepOrWake, sw.TimeOfDay, sw.PatientId);
        }

        public static List<SleepWake> LoadByPatientId(Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    List<SleepWake> sws = new List<SleepWake>();

                    var results = (from sw in dc.tblSleepWakes
                                   where sw.PatientId == patientId
                                   select new
                                   {
                                       sw.Id,
                                       sw.SleepOrWake,
                                       sw.TimeOfDay,
                                       sw.PatientId
                                   }).ToList();

                    results.ForEach(r => sws.Add(new SleepWake
                    {
                        Id = r.Id,
                        SleepOrWake = r.SleepOrWake,
                        TimeOfDay = r.TimeOfDay,
                        PatientId = r.PatientId
                    }));

                    return sws;
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
                    tblSleepWake deleterow = (from dt in dc.tblSleepWakes where dt.Id == id select dt).FirstOrDefault();
                    dc.tblSleepWakes.Remove(deleterow);
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
