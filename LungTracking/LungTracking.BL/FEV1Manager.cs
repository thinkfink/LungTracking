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
    public static class FEV1Manager
    {
        public async static Task<IEnumerable<Models.FEV1>> Load()
        {
            try
            {
                List<FEV1> fev1 = new List<FEV1>();
                await Task.Run(() => 
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
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
                    }
                });
                return fev1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<IEnumerable<Models.FEV1>> LoadByPatientId(Guid patientId)
        {
            try
            {
                List<FEV1> results = new List<FEV1>();
                await Task.Run(() => 
                {
                    if (patientId != null)
                    {
                        using (LungTrackingEntities dc = new LungTrackingEntities())
                        {



                            var fev1 = (from dt in dc.tblFev1s
                                        where dt.PatientId == patientId
                                        select new
                                        {
                                            dt.Id,
                                            dt.Fev1number,
                                            dt.BeginningEnd,
                                            dt.TimeOfDay,
                                            dt.PatientId
                                        }).ToList();

                            if (fev1 != null)
                            {
                                fev1.ForEach(app => results.Add(new FEV1
                                {
                                    Id = app.Id,
                                    FEV1Number = app.Fev1number,
                                    BeginningEnd = app.BeginningEnd,
                                    TimeOfDay = app.TimeOfDay,
                                    PatientId = app.PatientId
                                }));
                            }
                            else
                            {
                                throw new Exception("FEV1 was not found.");
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

        public async static Task<IEnumerable<Models.FEV1>> LoadRecentByPatientId(Guid patientId)
        {
            try
            {
                List<FEV1> results = new List<FEV1>();
                await Task.Run(() =>
                {
                    if (patientId != null)
                    {
                        using (LungTrackingEntities dc = new LungTrackingEntities())
                        {
                            var fev1 = (from dt in dc.tblFev1s
                                        where dt.PatientId == patientId
                                        orderby dt.TimeOfDay descending
                                        select new
                                        {
                                            dt.Id,
                                            dt.Fev1number,
                                            dt.BeginningEnd,
                                            dt.TimeOfDay,
                                            dt.PatientId
                                        }).Take(2).ToList();

                            if (fev1 != null)
                            {
                                fev1.ForEach(app => results.Add(new FEV1
                                {
                                    Id = app.Id,
                                    FEV1Number = app.Fev1number,
                                    BeginningEnd = app.BeginningEnd,
                                    TimeOfDay = app.TimeOfDay,
                                    PatientId = app.PatientId
                                }));
                            }
                            else
                            {
                                throw new Exception("FEV1 was not found.");
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


        public async static Task<Guid> Insert(decimal fev1Number, bool beginningEnd, DateTime timeOfDay, Guid patientId, bool rollback = false)
        {
            try
            {
                Models.FEV1 fev1 = new Models.FEV1
                {
                    Id = Guid.NewGuid(),
                    FEV1Number = fev1Number,
                    BeginningEnd = beginningEnd,
                    TimeOfDay = timeOfDay,
                    PatientId = patientId
                };

                /*// grab recent FEV1 entries and put them into an array
                IEnumerable<Models.FEV1> fev1List = (IEnumerable<FEV1>)LoadRecentByPatientId(patientId);
                List<FEV1> fev1Array = fev1List.ToList();

                // compare fev1Number values between entered entry and recent entries - check if 10% drop
                if (((fev1Array[0].FEV1Number - fev1.FEV1Number) / 100) <= -0.1M )
                {
                    // check if user's entry constitutes a second consecutive 10% drop
                    if (((fev1Array[1].FEV1Number - fev1Array[0].FEV1Number) / 100) < -0.1M)
                    {
                        fev1.Alert = "Warning: FEV1 dropped by 10% or more for two consecutive entries. Call your transplant coordinator.";
                    }
                    else
                    {
                        fev1.Alert = "Warning: FEV1 dropped by 10% or more since last entry.";
                    }
                }*/

                await Insert(fev1, rollback);
                return fev1.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Insert(Models.FEV1 fev1, bool rollback = false)
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

                        tblFev1 newrow = new tblFev1();

                        newrow.Id = Guid.NewGuid();
                        newrow.Fev1number = fev1.FEV1Number;
                        newrow.BeginningEnd = fev1.BeginningEnd;
                        newrow.TimeOfDay = fev1.TimeOfDay;
                        newrow.PatientId = fev1.PatientId;

                        fev1.Id = newrow.Id;

                        dc.tblFev1s.Add(newrow);
                        int results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                });

                // grab recent FEV1 entries and put them into an array
                IEnumerable<Models.FEV1> fev1List = (IEnumerable<FEV1>)LoadRecentByPatientId(fev1.PatientId);
                List<FEV1> fev1Array = fev1List.ToList();

                // compare fev1Number values between entered entry and recent entries - check if 10% drop
                if (((fev1Array[0].FEV1Number - fev1.FEV1Number) / 100) <= -0.1M)
                {
                    // check if user's entry constitutes a second consecutive 10% drop
                    if (((fev1Array[1].FEV1Number - fev1Array[0].FEV1Number) / 100) < -0.1M)
                    {
                        fev1.Alert = "Warning: FEV1 dropped by 10% or more for two consecutive entries. Call your transplant coordinator.";
                    }
                    else
                    {
                        fev1.Alert = "Warning: FEV1 dropped by 10% or more since last entry.";
                    }
                }

                return results;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Update(Models.FEV1 fev1, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                int results = 0;
                await Task.Run(() => 
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblFev1 row = (from dt in dc.tblFev1s where dt.Id == fev1.Id select dt).FirstOrDefault();
                        int results = 0;
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            row.Fev1number = fev1.FEV1Number;
                            row.BeginningEnd = fev1.BeginningEnd;
                            row.TimeOfDay = fev1.TimeOfDay;
                            row.PatientId = fev1.PatientId;

                            results = dc.SaveChanges();
                            if (rollback) transaction.Rollback();
                        }
                        else
                        {
                            throw new Exception("Row was not found");
                        }
                    }
                });

                // grab recent FEV1 entries and put them into an array
                IEnumerable<Models.FEV1> fev1List = (IEnumerable<FEV1>)LoadRecentByPatientId(fev1.PatientId);
                List<FEV1> fev1Array = fev1List.ToList();

                // compare fev1Number values between entered entry and recent entries - check if 10% drop
                if (((fev1Array[0].FEV1Number - fev1.FEV1Number) / 100) <= -0.1M)
                {
                    // check if user's entry constitutes a second consecutive 10% drop
                    if (((fev1Array[1].FEV1Number - fev1Array[0].FEV1Number) / 100) < -0.1M)
                    {
                        fev1.Alert = "Warning: FEV1 dropped by 10% or more for two consecutive entries. Call your transplant coordinator.";
                    }
                    else
                    {
                        fev1.Alert = "Warning: FEV1 dropped by 10% or more since last entry.";
                    }
                }

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
                        tblFev1 row = dc.tblFev1s.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.tblFev1s.Remove(row);

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
