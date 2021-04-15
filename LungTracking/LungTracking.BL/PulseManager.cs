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
    public static class PulseManager
    {
        public async static Task<IEnumerable<Models.Pulse>> Load()
        {
            try
            {
                List<Pulse> pulse = new List<Pulse>();

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    dc.tblPulses
                        .ToList()
                        .ForEach(u => pulse.Add(new Pulse
                        {
                            Id = u.Id,
                            PulseNumber = u.PulseNumber,
                            BeginningEnd = u.BeginningEnd,
                            TimeOfDay = u.TimeOfDay,
                            PatientId = u.PatientId
                        }));
                    return pulse;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<IEnumerable<Models.Pulse>> LoadByPatientId(Guid patientId)
        {
            try
            {
                if (patientId != null)
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {

                        List<Pulse> results = new List<Pulse>();

                        var pulse = (from dt in dc.tblPulses
                                   where dt.PatientId == patientId
                                   select new
                                   {
                                       dt.Id,
                                       dt.PulseNumber,
                                       dt.BeginningEnd,
                                       dt.TimeOfDay,
                                       dt.PatientId
                                   }).ToList();

                        if (pulse != null)
                        {
                            pulse.ForEach(app => results.Add(new Pulse
                            {
                                Id = app.Id,
                                PulseNumber = app.PulseNumber,
                                BeginningEnd = app.BeginningEnd,
                                TimeOfDay = app.TimeOfDay,
                                PatientId = app.PatientId
                            }));
                            return results;
                        }
                        else
                        {
                            throw new Exception("Pulse was not found.");
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


        public async static Task<Guid> Insert(int pulseNumber, bool beginningEnd, DateTime timeOfDay, Guid patientId, bool rollback = false)
        {
            try
            {
                Models.Pulse pulse = new Models.Pulse
                {
                    Id = Guid.NewGuid(),
                    PulseNumber = pulseNumber,
                    BeginningEnd = beginningEnd,
                    TimeOfDay = timeOfDay,
                    PatientId = patientId
                };
                await Insert(pulse, rollback);
                return pulse.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Insert(Models.Pulse pulse, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPulse newrow = new tblPulse();

                    newrow.Id = Guid.NewGuid();
                    newrow.PulseNumber = pulse.PulseNumber;
                    newrow.BeginningEnd = pulse.BeginningEnd;
                    newrow.TimeOfDay = pulse.TimeOfDay;
                    newrow.PatientId = pulse.PatientId;

                    pulse.Id = newrow.Id;

                    dc.tblPulses.Add(newrow);
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

        public async static Task<int> Update(Models.Pulse pulse, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPulse row = (from dt in dc.tblPulses where dt.Id == pulse.Id select dt).FirstOrDefault();
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        row.PulseNumber = pulse.PulseNumber;
                        row.BeginningEnd = pulse.BeginningEnd;
                        row.TimeOfDay = pulse.TimeOfDay;
                        row.PatientId = pulse.PatientId;

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

        public async static Task<int> Delete(Guid id, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                int results = 0;
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblPulse row = dc.tblPulses.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.tblPulses.Remove(row);

                            results = dc.SaveChanges();
                            if (rollback) transaction.Rollback();
                        }
                        else
                        {
                            throw new Exception("Row was not found.");
                        }
                    }
                });
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
