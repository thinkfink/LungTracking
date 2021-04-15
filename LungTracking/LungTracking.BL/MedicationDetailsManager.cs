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
    public static class MedicationDetailsManager
    {
        public async static Task<IEnumerable<Models.MedicationDetails>> Load()
        {
            try
            {
                List<MedicationDetails> medDetails = new List<MedicationDetails>();
                await Task.Run(() => 
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        dc.tblMedicationDetails
                            .ToList()
                            .ForEach(u => medDetails.Add(new MedicationDetails
                            {
                                Id = u.Id,
                                MedicationName = u.MedicationName,
                                MedicationDosageTotal = u.MedicationDosageTotal,
                                MedicationDosagePerPill = u.MedicationDosagePerPill,
                                MedicationInstructions = u.MedicationInstructions,
                                NumberOfPills = u.NumberOfPills,
                                DateFilled = u.DateFilled,
                                QuantityOfFill = u.QuantityOfFill,
                                RefillDate = u.RefillDate,
                                PatientId = u.PatientId
                            }));
                    }
                });
                return medDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<Models.MedicationDetails> LoadByMedicationDetailsId(Guid medDetailsId)
        {
            try
            {
                Models.MedicationDetails medDetails = new Models.MedicationDetails();
                await Task.Run(() => 
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblMedicationDetail tblMedicationDetails = dc.tblMedicationDetails.FirstOrDefault(c => c.Id == medDetailsId);

                        if (tblMedicationDetails != null)
                        {
                            medDetails.Id = tblMedicationDetails.Id;
                            medDetails.MedicationName = tblMedicationDetails.MedicationName;
                            medDetails.MedicationDosageTotal = tblMedicationDetails.MedicationDosageTotal;
                            medDetails.MedicationDosagePerPill = tblMedicationDetails.MedicationDosagePerPill;
                            medDetails.MedicationInstructions = tblMedicationDetails.MedicationInstructions;
                            medDetails.NumberOfPills = tblMedicationDetails.NumberOfPills;
                            medDetails.DateFilled = tblMedicationDetails.DateFilled;
                            medDetails.QuantityOfFill = tblMedicationDetails.QuantityOfFill;
                            medDetails.RefillDate = tblMedicationDetails.RefillDate;
                            medDetails.PatientId = tblMedicationDetails.PatientId;
                        }
                        else
                        {
                            throw new Exception("Could not find the row");
                        }
                    }
                });
                return medDetails;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<IEnumerable<Models.MedicationDetails>> LoadByPatientId(Guid patientId)
        {
            try
            {
                List<MedicationDetails> results = new List<MedicationDetails>();
                await Task.Run(() => 
                {
                    if (patientId != null)
                    {
                        using (LungTrackingEntities dc = new LungTrackingEntities())
                        {



                            var medDetails = (from dt in dc.tblMedicationDetails
                                              where dt.PatientId == patientId
                                              select new
                                              {
                                                  dt.Id,
                                                  dt.MedicationName,
                                                  dt.MedicationDosageTotal,
                                                  dt.MedicationDosagePerPill,
                                                  dt.MedicationInstructions,
                                                  dt.NumberOfPills,
                                                  dt.DateFilled,
                                                  dt.QuantityOfFill,
                                                  dt.RefillDate,
                                                  dt.PatientId
                                              }).ToList();

                            if (medDetails != null)
                            {
                                medDetails.ForEach(app => results.Add(new MedicationDetails
                                {
                                    Id = app.Id,
                                    MedicationName = app.MedicationName,
                                    MedicationDosageTotal = app.MedicationDosageTotal,
                                    MedicationDosagePerPill = app.MedicationDosagePerPill,
                                    MedicationInstructions = app.MedicationInstructions,
                                    NumberOfPills = app.NumberOfPills,
                                    DateFilled = app.DateFilled,
                                    QuantityOfFill = app.QuantityOfFill,
                                    RefillDate = app.RefillDate,
                                    PatientId = app.PatientId
                                }));
                            }
                            else
                            {
                                throw new Exception("MedicationDetails was not found.");
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


        public async static Task<Guid> Insert(string medName, string medDosageTotal, string medDosagePerPill, string medInstructions, int numberOfPills, DateTime dateFilled, int quantityOfFill, DateTime refillDate, Guid patientId, bool rollback = false)
        {
            try
            {
                Models.MedicationDetails medDetails = new Models.MedicationDetails
                {
                    MedicationName = medName,
                    MedicationDosageTotal = medDosageTotal,
                    MedicationDosagePerPill = medDosagePerPill,
                    MedicationInstructions = medInstructions,
                    NumberOfPills = numberOfPills,
                    DateFilled = dateFilled,
                    QuantityOfFill = quantityOfFill,
                    RefillDate = refillDate,
                    PatientId = patientId
                };
                await Insert(medDetails, rollback);
                return medDetails.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Insert(Models.MedicationDetails medDetails, bool rollback = false)
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

                        tblMedicationDetail newrow = new tblMedicationDetail();

                        newrow.Id = Guid.NewGuid();
                        newrow.MedicationName = medDetails.MedicationName;
                        newrow.MedicationDosageTotal = medDetails.MedicationDosageTotal;
                        newrow.MedicationDosagePerPill = medDetails.MedicationDosagePerPill;
                        newrow.MedicationInstructions = medDetails.MedicationInstructions;
                        newrow.NumberOfPills = medDetails.NumberOfPills;
                        newrow.DateFilled = medDetails.DateFilled;
                        newrow.QuantityOfFill = medDetails.QuantityOfFill;
                        newrow.RefillDate = medDetails.RefillDate;
                        newrow.PatientId = medDetails.PatientId;

                        medDetails.Id = newrow.Id;

                        dc.tblMedicationDetails.Add(newrow);
                        int results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();

                        return results;
                    }
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Update(Models.MedicationDetails medDetails, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                int results = 0;
                await Task.Run(() => 
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblMedicationDetail row = (from dt in dc.tblMedicationDetails where dt.Id == medDetails.Id select dt).FirstOrDefault();
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            row.MedicationName = medDetails.MedicationName;
                            row.MedicationDosageTotal = medDetails.MedicationDosageTotal;
                            row.MedicationDosagePerPill = medDetails.MedicationDosagePerPill;
                            row.MedicationInstructions = medDetails.MedicationInstructions;
                            row.NumberOfPills = medDetails.NumberOfPills;
                            row.DateFilled = medDetails.DateFilled;
                            row.QuantityOfFill = medDetails.QuantityOfFill;
                            row.RefillDate = medDetails.RefillDate;
                            row.PatientId = medDetails.PatientId;

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
                        tblMedicationDetail row = dc.tblMedicationDetails.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.tblMedicationDetails.Remove(row);

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
