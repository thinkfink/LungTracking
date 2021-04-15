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
    public static class TemperatureManager
    {
        public async static Task<IEnumerable<Models.Temperature>> Load()
        {
            try
            {
                List<Temperature> temp = new List<Temperature>();

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    dc.tblTemperatures
                        .ToList()
                        .ForEach(u => temp.Add(new Temperature
                        {
                            Id = u.Id,
                            TempNumber = u.TempNumber,
                            BeginningEnd = u.BeginningEnd,
                            TimeOfDay = u.TimeOfDay,
                            PatientId = u.PatientId
                        }));
                    return temp;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<IEnumerable<Models.Temperature>> LoadByPatientId(Guid patientId)
        {
            try
            {
                if (patientId != null)
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {

                        List<Temperature> results = new List<Temperature>();

                        var temp = (from dt in dc.tblTemperatures
                                     where dt.PatientId == patientId
                                     select new
                                     {
                                         dt.Id,
                                         dt.TempNumber,
                                         dt.BeginningEnd,
                                         dt.TimeOfDay,
                                         dt.PatientId
                                     }).ToList();

                        if (temp != null)
                        {
                            temp.ForEach(app => results.Add(new Temperature
                            {
                                Id = app.Id,
                                TempNumber = app.TempNumber,
                                BeginningEnd = app.BeginningEnd,
                                TimeOfDay = app.TimeOfDay,
                                PatientId = app.PatientId
                            }));
                            return results;
                        }
                        else
                        {
                            throw new Exception("Temperature was not found.");
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


        public async static Task<Guid> Insert(int tempNumber, bool beginningEnd, DateTime timeOfDay, Guid patientId, bool rollback = false)
        {
            try
            {
                Models.Temperature temp = new Models.Temperature
                {
                    Id = Guid.NewGuid(),
                    TempNumber = tempNumber,
                    BeginningEnd = beginningEnd,
                    TimeOfDay = timeOfDay,
                    PatientId = patientId
                };
                await Insert(temp, rollback);
                return temp.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Insert(Models.Temperature temp, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblTemperature newrow = new tblTemperature();

                    newrow.Id = Guid.NewGuid();
                    newrow.TempNumber = temp.TempNumber;
                    newrow.BeginningEnd = temp.BeginningEnd;
                    newrow.TimeOfDay = temp.TimeOfDay;
                    newrow.PatientId = temp.PatientId;

                    temp.Id = newrow.Id;

                    dc.tblTemperatures.Add(newrow);
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

        public async static Task<int> Update(Models.Temperature temp, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblTemperature row = (from dt in dc.tblTemperatures where dt.Id == temp.Id select dt).FirstOrDefault();
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        row.TempNumber = temp.TempNumber;
                        row.BeginningEnd = temp.BeginningEnd;
                        row.TimeOfDay = temp.TimeOfDay;
                        row.PatientId = temp.PatientId;

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
                        tblTemperature row = dc.tblTemperatures.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.tblTemperatures.Remove(row);

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
