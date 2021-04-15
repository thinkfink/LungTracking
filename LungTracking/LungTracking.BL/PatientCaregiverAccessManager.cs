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
    public static class PatientCaregiverAccessManager
    {
        public async static Task<IEnumerable<Models.PatientCaregiverAccess>> Load()
        {
            try
            {
                List<PatientCaregiverAccess> pcas = new List<PatientCaregiverAccess>();

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
                    return pcas;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<Models.PatientCaregiverAccess> LoadById(Guid patientId, Guid caregiverId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPatientCaregiverAccess tblPatientCaregiverAccess = dc.tblPatientCaregiverAccesses.FirstOrDefault(c => c.PatientId == patientId && c.CaregiverId == caregiverId);
                    Models.PatientCaregiverAccess pca = new Models.PatientCaregiverAccess();

                    if (tblPatientCaregiverAccess != null)
                    {
                        pca.Id = tblPatientCaregiverAccess.Id;
                        pca.PatientId = tblPatientCaregiverAccess.PatientId;
                        pca.CaregiverId = tblPatientCaregiverAccess.CaregiverId;
                        return pca;
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

        public async static Task<int> Insert(Models.PatientCaregiverAccess pca, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

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
                    tblPatientCaregiverAccess deleterow = (from dt in dc.tblPatientCaregiverAccesses where dt.Id == id select dt).FirstOrDefault();
                    dc.tblPatientCaregiverAccesses.Remove(deleterow);
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
