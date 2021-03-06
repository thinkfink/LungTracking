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
    public static class PatientCaregiverAccessManager
    {
        public async static Task<IEnumerable<Models.PatientCaregiverAccess>> Load()
        {
            try
            {
                List<PatientCaregiverAccess> pcas = new List<PatientCaregiverAccess>();
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        dc.tblPatientCaregiverAccesses
                            .ToList()
                            .ForEach(u => pcas.Add(new PatientCaregiverAccess
                            {
                                Id = u.Id,
                                PatientId = u.PatientId,
                                CaregiverId = u.CaregiverId
                            }));
                    }
                });
                return pcas;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /*
        public async static Task<Models.PatientCaregiverAccess> LoadById(Guid patientId, Guid caregiverId)
        {
            try
            {
                Models.PatientCaregiverAccess pca = new Models.PatientCaregiverAccess();
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblPatientCaregiverAccess tblPatientCaregiverAccess = dc.tblPatientCaregiverAccesses.FirstOrDefault(c => c.PatientId == patientId && c.CaregiverId == caregiverId);


                        if (tblPatientCaregiverAccess != null)
                        {
                            pca.Id = tblPatientCaregiverAccess.Id;
                            pca.PatientId = tblPatientCaregiverAccess.PatientId;
                            pca.CaregiverId = tblPatientCaregiverAccess.CaregiverId;
                        }
                        else
                        {
                            throw new Exception("Could not find the row");
                        }
                    }
                });
                return pca;
            }
            catch (Exception)
            {
                throw;
            }
        }
        */

        public async static Task<List<Models.PatientCaregiverAccess>> LoadByCaregiverId(Guid caregiverId)
        {
            try
            {
                List<PatientCaregiverAccess> results = new List<PatientCaregiverAccess>();
                await Task.Run(() =>
                {
                    if (caregiverId != null)
                    {
                        using (LungTrackingEntities dc = new LungTrackingEntities())
                        {
                            var ppa = (from dt in dc.tblPatientCaregiverAccesses
                                       where dt.CaregiverId == caregiverId
                                       select new
                                       {
                                           dt.Id,
                                           dt.CaregiverId,
                                           dt.PatientId
                                       }).ToList();

                            if (ppa != null)
                            {
                                ppa.ForEach(app => results.Add(new PatientCaregiverAccess
                                {
                                    Id = app.Id,
                                    CaregiverId = app.CaregiverId,
                                    PatientId = app.PatientId
                                }));
                            }
                            else
                            {
                                throw new Exception("PatientCaregiverAccess was not found.");
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

        public async static Task<int> Insert(Models.PatientCaregiverAccess pca, bool rollback = false)
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

                        tblPatientCaregiverAccess newrow = new tblPatientCaregiverAccess();

                        newrow.Id = Guid.NewGuid();
                        newrow.PatientId = pca.PatientId;
                        newrow.CaregiverId = pca.CaregiverId;

                        pca.Id = newrow.Id;

                        dc.tblPatientCaregiverAccesses.Add(newrow);
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
                        tblPatientCaregiverAccess row = dc.tblPatientCaregiverAccesses.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.tblPatientCaregiverAccesses.Remove(row);

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
