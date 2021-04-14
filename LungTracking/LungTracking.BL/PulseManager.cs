using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class PulseManager
    {
        public static List<Pulse> Load()
        {
            using (LungTrackingEntities dc = new LungTrackingEntities())
            {
                List<Pulse> pulses = new List<Pulse>();

                dc.tblPulses
                    .ToList()
                    .ForEach(u => pulses.Add(new Pulse
                    {
                        Id = u.Id,
                        PulseNumber = u.PulseNumber,
                        BeginningEnd = u.BeginningEnd,
                        TimeOfDay = u.TimeOfDay,
                        PatientId = u.PatientId
                    }));
                return pulses;
            }
        }
        public static int Insert(int pulseNumber, bool beginningEnd, DateTime timeOfDay, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPulse newPulse = new tblPulse
                    {
                        Id = Guid.NewGuid(),
                        PulseNumber = pulseNumber,
                        BeginningEnd = beginningEnd,
                        TimeOfDay = timeOfDay,
                        PatientId = patientId
                    };
                    dc.tblPulses.Add(newPulse);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(Pulse pulse)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPulse newPulse = new tblPulse
                    {
                        Id = Guid.NewGuid(),
                        PulseNumber = pulse.PulseNumber,
                        BeginningEnd = pulse.BeginningEnd,
                        TimeOfDay = pulse.TimeOfDay,
                        PatientId = pulse.PatientId
                    };
                    dc.tblPulses.Add(newPulse);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Guid id, int pulseNumber, bool beginningEnd, DateTime timeOfDay, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPulse updaterow = (from dt in dc.tblPulses where dt.Id == id select dt).FirstOrDefault();
                    updaterow.PulseNumber = pulseNumber;
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

        public static int Update(Pulse pulse)
        {
            return Update(pulse.Id, pulse.PulseNumber, pulse.BeginningEnd, pulse.TimeOfDay, pulse.PatientId);
        }

        public static List<Pulse> LoadByPatientId(Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    List<Pulse> pulses = new List<Pulse>();

                    var results = (from pulse in dc.tblPulses
                                   where pulse.PatientId == patientId
                                   select new
                                   {
                                       pulse.Id,
                                       pulse.PulseNumber,
                                       pulse.BeginningEnd,
                                       pulse.TimeOfDay,
                                       pulse.PatientId
                                   }).ToList();

                    results.ForEach(r => pulses.Add(new Pulse
                    {
                        Id = r.Id,
                        PulseNumber = r.PulseNumber,
                        BeginningEnd = r.BeginningEnd,
                        TimeOfDay = r.TimeOfDay,
                        PatientId = r.PatientId
                    }));

                    return pulses;
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
                    tblPulse deleterow = (from dt in dc.tblPulses where dt.Id == id select dt).FirstOrDefault();
                    dc.tblPulses.Remove(deleterow);
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
