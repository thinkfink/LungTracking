﻿using System;
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
                    return ppas;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<Models.PatientProviderAccess> LoadById(Guid patientId, Guid providerId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPatientProviderAccess tblPatientProviderAccess = dc.tblPatientProviderAccesses.FirstOrDefault(c => c.PatientId == patientId && c.ProviderId == providerId);
                    Models.PatientProviderAccess ppa = new Models.PatientProviderAccess();

                    if (tblPatientProviderAccess != null)
                    {
                        ppa.Id = tblPatientProviderAccess.Id;
                        ppa.PatientId = tblPatientProviderAccess.PatientId;
                        ppa.ProviderId = tblPatientProviderAccess.ProviderId;
                        return ppa;
                    }
                    else
                    {
                        throw new Exception("Could not find the row");
                    }
                }
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

                    return results;
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
                    tblPatientProviderAccess deleterow = (from dt in dc.tblPatientProviderAccesses where dt.Id == id select dt).FirstOrDefault();
                    dc.tblPatientProviderAccesses.Remove(deleterow);
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
