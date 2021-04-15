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
    public static class PEFManager
    {
        public async static Task<IEnumerable<Models.PEF>> Load()
        {
            try
            {
                List<PEF> pef = new List<PEF>();
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        dc.tblPefs
                            .ToList()
                            .ForEach(u => pef.Add(new PEF
                            {
                                Id = u.Id,
                                PEFNumber = u.Pefnumber,
                                BeginningEnd = u.BeginningEnd,
                                TimeOfDay = u.TimeOfDay,
                                PatientId = u.PatientId
                            }));
                    }
                });
                return pef;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<IEnumerable<Models.PEF>> LoadByPatientId(Guid patientId)
        {
            try
            {
                List<PEF> results = new List<PEF>();
                await Task.Run(() =>
                {
                    if (patientId != null)
                    {
                        using (LungTrackingEntities dc = new LungTrackingEntities())
                        {
                            var pef = (from dt in dc.tblPefs
                                       where dt.PatientId == patientId
                                       select new
                                       {
                                           dt.Id,
                                           dt.Pefnumber,
                                           dt.BeginningEnd,
                                           dt.TimeOfDay,
                                           dt.PatientId
                                       }).ToList();

                            if (pef != null)
                            {
                                pef.ForEach(app => results.Add(new PEF
                                {
                                    Id = app.Id,
                                    PEFNumber = app.Pefnumber,
                                    BeginningEnd = app.BeginningEnd,
                                    TimeOfDay = app.TimeOfDay,
                                    PatientId = app.PatientId
                                }));
                            }
                            else
                            {
                                throw new Exception("PEF was not found.");
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


        public async static Task<Guid> Insert(decimal pefNumber, bool beginningEnd, DateTime timeOfDay, Guid patientId, bool rollback = false)
        {
            try
            {
                Models.PEF pef = new Models.PEF
                {
                    Id = Guid.NewGuid(),
                    PEFNumber = pefNumber,
                    BeginningEnd = beginningEnd,
                    TimeOfDay = timeOfDay,
                    PatientId = patientId
                };
                await Insert(pef, rollback);
                return pef.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Insert(Models.PEF pef, bool rollback = false)
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

                        tblPef newrow = new tblPef();

                        newrow.Id = Guid.NewGuid();
                        newrow.Pefnumber = pef.PEFNumber;
                        newrow.BeginningEnd = pef.BeginningEnd;
                        newrow.TimeOfDay = pef.TimeOfDay;
                        newrow.PatientId = pef.PatientId;

                        pef.Id = newrow.Id;

                        dc.tblPefs.Add(newrow);
                        int results = dc.SaveChanges();

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

        public async static Task<int> Update(Models.PEF pef, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                int results = 0;
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblPef row = (from dt in dc.tblPefs where dt.Id == pef.Id select dt).FirstOrDefault();
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            row.Pefnumber = pef.PEFNumber;
                            row.BeginningEnd = pef.BeginningEnd;
                            row.TimeOfDay = pef.TimeOfDay;
                            row.PatientId = pef.PatientId;

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
                        tblPef row = dc.tblPefs.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.tblPefs.Remove(row);

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
