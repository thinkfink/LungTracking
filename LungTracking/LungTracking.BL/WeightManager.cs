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
    public static class WeightManager
    {
        public async static Task<IEnumerable<Models.Weight>> Load()
        {
            try
            {
                List<Weight> weight = new List<Weight>();
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        dc.tblWeights
                            .ToList()
                            .ForEach(u => weight.Add(new Weight
                            {
                                Id = u.Id,
                                WeightNumberInPounds = u.WeightNumberInPounds,
                                TimeOfDay = u.TimeOfDay,
                                PatientId = u.PatientId
                            }));
                    }
                });
                return weight;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<IEnumerable<Models.Weight>> LoadByPatientId(Guid patientId)
        {
            try
            {
                List<Weight> results = new List<Weight>();
                await Task.Run(() =>
                {
                    if (patientId != null)
                    {
                        using (LungTrackingEntities dc = new LungTrackingEntities())
                        {
                            var weight = (from dt in dc.tblWeights
                                          where dt.PatientId == patientId
                                          select new
                                          {
                                              dt.Id,
                                              dt.WeightNumberInPounds,
                                              dt.TimeOfDay,
                                              dt.PatientId
                                          }).ToList();

                            if (weight != null)
                            {
                                weight.ForEach(app => results.Add(new Weight
                                {
                                    Id = app.Id,
                                    WeightNumberInPounds = app.WeightNumberInPounds,
                                    TimeOfDay = app.TimeOfDay,
                                    PatientId = app.PatientId
                                }));
                            }
                            else
                            {
                                throw new Exception("Weight was not found.");
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


        public async static Task<Guid> Insert(int weightNumber, DateTime timeOfDay, Guid patientId, bool rollback = false)
        {
            try
            {
                Models.Weight weight = new Models.Weight
                {
                    Id = Guid.NewGuid(),
                    WeightNumberInPounds = weightNumber,
                    TimeOfDay = timeOfDay,
                    PatientId = patientId
                };
                await Insert(weight, rollback);
                return weight.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Insert(Models.Weight weight, bool rollback = false)
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

                        tblWeight newrow = new tblWeight();

                        newrow.Id = Guid.NewGuid();
                        newrow.WeightNumberInPounds = weight.WeightNumberInPounds;
                        newrow.TimeOfDay = weight.TimeOfDay;
                        newrow.PatientId = weight.PatientId;

                        weight.Id = newrow.Id;

                        dc.tblWeights.Add(newrow);
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

        public async static Task<int> Update(Models.Weight weight, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                int results = 0;
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblWeight row = (from dt in dc.tblWeights where dt.Id == weight.Id select dt).FirstOrDefault();
                        int results = 0;
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            row.WeightNumberInPounds = weight.WeightNumberInPounds;
                            row.TimeOfDay = weight.TimeOfDay;
                            row.PatientId = weight.PatientId;

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
                        tblWeight row = dc.tblWeights.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.tblWeights.Remove(row);

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
