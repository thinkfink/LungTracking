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
    public static class PatientProviderAccessManager
    {
        public async static Task<IEnumerable<Models.PatientProviderAccess>> Load()
        {
            try
            {
                List<PatientProviderAccess> ppas = new List<PatientProviderAccess>();
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        dc.tblPatientProviderAccesses
                            .ToList()
                            .ForEach(u => ppas.Add(new PatientProviderAccess
                            {
                                Id = u.Id,
                                PatientId = u.PatientId,
                                ProviderId = u.ProviderId
                            }));
                    }
                });
                return ppas;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /*
        public async static Task<Models.PatientProviderAccess> LoadById(Guid patientId, Guid providerId)
        {
            try
            {
                Models.PatientProviderAccess ppa = new Models.PatientProviderAccess();
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblPatientProviderAccess tblPatientProviderAccess = dc.tblPatientProviderAccesses.FirstOrDefault(c => c.PatientId == patientId && c.ProviderId == providerId);


                        if (tblPatientProviderAccess != null)
                        {
                            ppa.Id = tblPatientProviderAccess.Id;
                            ppa.PatientId = tblPatientProviderAccess.PatientId;
                            ppa.ProviderId = tblPatientProviderAccess.ProviderId;
                        }
                        else
                        {
                            throw new Exception("Could not find the row");
                        }
                    }
                });
                return ppa;
            }
            catch (Exception)
            {
                throw;
            }
        }
        */

        public async static Task<List<Models.PatientProviderAccess>> LoadByProviderId(Guid providerId)
        {
            try
            {
                List<PatientProviderAccess> results = new List<PatientProviderAccess>();
                await Task.Run(() =>
                {
                    if (providerId != null)
                    {
                        using (LungTrackingEntities dc = new LungTrackingEntities())
                        {
                            var ppa = (from dt in dc.tblPatientProviderAccesses
                                         where dt.ProviderId == providerId
                                         select new
                                         {
                                             dt.Id,
                                             dt.ProviderId,
                                             dt.PatientId
                                         }).ToList();

                            if (ppa != null)
                            {
                                ppa.ForEach(app => results.Add(new PatientProviderAccess
                                {
                                    Id = app.Id,
                                    ProviderId = app.ProviderId,
                                    PatientId = app.PatientId
                                }));
                            }
                            else
                            {
                                throw new Exception("PatientProviderAccess was not found.");
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

        public async static Task<int> Insert(Models.PatientProviderAccess ppa, bool rollback = false)
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

                        tblPatientProviderAccess newrow = new tblPatientProviderAccess();

                        newrow.Id = Guid.NewGuid();
                        newrow.PatientId = ppa.PatientId;
                        newrow.ProviderId = ppa.ProviderId;

                        ppa.Id = newrow.Id;

                        dc.tblPatientProviderAccesses.Add(newrow);
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
                        tblPatientProviderAccess row = dc.tblPatientProviderAccesses.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.tblPatientProviderAccesses.Remove(row);

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
