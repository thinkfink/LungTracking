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
    public static class SleepWakeManager
    {
        public async static Task<IEnumerable<Models.SleepWake>> Load()
        {
            try
            {
                List<SleepWake> sw = new List<SleepWake>();
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        dc.tblSleepWakes
                            .ToList()
                            .ForEach(u => sw.Add(new SleepWake
                            {
                                Id = u.Id,
                                SleepOrWake = u.SleepOrWake,
                                TimeOfDay = u.TimeOfDay,
                                PatientId = u.PatientId
                            }));
                    }
                });
                return sw;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<IEnumerable<Models.SleepWake>> LoadByPatientId(Guid patientId)
        {
            try
            {
                List<SleepWake> results = new List<SleepWake>();
                await Task.Run(() =>
                {
                    if (patientId != null)
                    {
                        using (LungTrackingEntities dc = new LungTrackingEntities())
                        {
                            var sw = (from dt in dc.tblSleepWakes
                                      where dt.PatientId == patientId
                                      select new
                                      {
                                          dt.Id,
                                          dt.SleepOrWake,
                                          dt.TimeOfDay,
                                          dt.PatientId
                                      }).ToList();

                            if (sw != null)
                            {
                                sw.ForEach(app => results.Add(new SleepWake
                                {
                                    Id = app.Id,
                                    SleepOrWake = app.SleepOrWake,
                                    TimeOfDay = app.TimeOfDay,
                                    PatientId = app.PatientId
                                }));
                            }
                            else
                            {
                                throw new Exception("SleepWake was not found.");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Please provide a patient Id.");
                    }
                });
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async static Task<Guid> Insert(bool sleepOrWake, DateTime timeOfDay, Guid patientId, bool rollback = false)
        {
            try
            {
                Models.SleepWake sw = new Models.SleepWake
                {
                    Id = Guid.NewGuid(),
                    SleepOrWake = sleepOrWake,
                    TimeOfDay = timeOfDay,
                    PatientId = patientId
                };
                await Insert(sw, rollback);
                return sw.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Insert(Models.SleepWake sw, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                int results = 0;
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        tblSleepWake newrow = new tblSleepWake();

                        newrow.Id = Guid.NewGuid();
                        newrow.SleepOrWake = sw.SleepOrWake;
                        newrow.TimeOfDay = sw.TimeOfDay;
                        newrow.PatientId = sw.PatientId;

                        sw.Id = newrow.Id;

                        dc.tblSleepWakes.Add(newrow);
                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                });
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Update(Models.SleepWake sw, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                int results = 0;
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblSleepWake row = (from dt in dc.tblSleepWakes where dt.Id == sw.Id select dt).FirstOrDefault();
                        int results = 0;
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            row.SleepOrWake = sw.SleepOrWake;
                            row.TimeOfDay = sw.TimeOfDay;
                            row.PatientId = sw.PatientId;

                            results = dc.SaveChanges();
                            if (rollback) transaction.Rollback();
                        }
                        else
                        {
                            throw new Exception("Row was not found");
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
                        tblSleepWake row = dc.tblSleepWakes.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.tblSleepWakes.Remove(row);

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
