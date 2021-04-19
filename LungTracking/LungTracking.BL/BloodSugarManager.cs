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
    public static class BloodSugarManager
    {
        public async static Task<IEnumerable<Models.BloodSugar>> Load()
        {
            try
            {
                List<BloodSugar> bloodSugar = new List<BloodSugar>();
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        dc.tblBloodSugars
                            .ToList()
                            .ForEach(u => bloodSugar.Add(new BloodSugar
                            {
                                Id = u.Id,
                                BloodSugarNumber = u.BloodSugarNumber,
                                TimeOfDay = u.TimeOfDay,
                                UnitsOfInsulinGiven = u.UnitsOfInsulinGiven,
                                TypeOfInsulinGiven = u.TypeOfInsulinGiven,
                                Notes = u.Notes,
                                PatientId = u.PatientId
                            }));
                    }
                });
                return bloodSugar;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<IEnumerable<Models.BloodSugar>> LoadByPatientId(Guid patientId)
        {
            try
            {
                List<BloodSugar> results = new List<BloodSugar>();
                await Task.Run(() => 
                {
                    if (patientId != null)
                    {
                        using (LungTrackingEntities dc = new LungTrackingEntities())
                        {
                            var bloodSugar = (from dt in dc.tblBloodSugars
                                              where dt.PatientId == patientId
                                              select new
                                              {
                                                  dt.Id,
                                                  dt.BloodSugarNumber,
                                                  dt.TimeOfDay,
                                                  dt.UnitsOfInsulinGiven,
                                                  dt.TypeOfInsulinGiven,
                                                  dt.Notes,
                                                  dt.PatientId
                                              }).ToList();

                            if (bloodSugar != null)
                            {
                                bloodSugar.ForEach(app => results.Add(new BloodSugar
                                {
                                    Id = app.Id,
                                    BloodSugarNumber = app.BloodSugarNumber,
                                    TimeOfDay = app.TimeOfDay,
                                    UnitsOfInsulinGiven = app.UnitsOfInsulinGiven,
                                    TypeOfInsulinGiven = app.TypeOfInsulinGiven,
                                    Notes = app.Notes,
                                    PatientId = app.PatientId
                                }));
                            }
                            else
                            {
                                throw new Exception("BloodSugar was not found.");
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


        public async static Task<Guid> Insert(int bloodSugarNumber, DateTime timeOfDay, int unitsOfInsulin, string typeOfInsulin, string notes, Guid patientId, bool rollback = false)
        {
            try
            {
                Models.BloodSugar bloodSugar = new Models.BloodSugar
                {
                    Id = Guid.NewGuid(),
                    BloodSugarNumber = bloodSugarNumber,
                    TimeOfDay = timeOfDay,
                    UnitsOfInsulinGiven = unitsOfInsulin,
                    TypeOfInsulinGiven = typeOfInsulin,
                    Notes = notes,
                    PatientId = patientId
                };

                if (bloodSugarNumber < 50)
                {
                    bloodSugar.Alert = "Blood sugar critically low. Call 911!";
                }

                await Insert(bloodSugar, rollback);
                return bloodSugar.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Insert(Models.BloodSugar bloodSugar, bool rollback = false)
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

                        tblBloodSugar newrow = new tblBloodSugar();

                        newrow.Id = Guid.NewGuid();
                        newrow.BloodSugarNumber = bloodSugar.BloodSugarNumber;
                        newrow.TimeOfDay = bloodSugar.TimeOfDay;
                        newrow.UnitsOfInsulinGiven = bloodSugar.UnitsOfInsulinGiven;
                        newrow.TypeOfInsulinGiven = bloodSugar.TypeOfInsulinGiven;
                        newrow.Notes = bloodSugar.Notes;
                        newrow.PatientId = bloodSugar.PatientId;

                        bloodSugar.Id = newrow.Id;

                        dc.tblBloodSugars.Add(newrow);
                        int results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                });

                if (bloodSugar.BloodSugarNumber < 50)
                {
                    bloodSugar.Alert = "Blood sugar critically low. Call 911!";
                }

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Update(Models.BloodSugar bloodSugar, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                int results = 0;
                await Task.Run(() => 
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblBloodSugar row = (from dt in dc.tblBloodSugars where dt.Id == bloodSugar.Id select dt).FirstOrDefault();
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            row.BloodSugarNumber = bloodSugar.BloodSugarNumber;
                            row.TimeOfDay = bloodSugar.TimeOfDay;
                            row.UnitsOfInsulinGiven = bloodSugar.UnitsOfInsulinGiven;
                            row.TypeOfInsulinGiven = bloodSugar.TypeOfInsulinGiven;
                            row.Notes = bloodSugar.Notes;
                            row.PatientId = bloodSugar.PatientId;

                            results = dc.SaveChanges();
                            if (rollback) transaction.Rollback();
                        }
                        else
                        {
                            throw new Exception("Row was not found");
                        }
                    }
                });

                if (bloodSugar.BloodSugarNumber < 50)
                {
                    bloodSugar.Alert = "Blood sugar critically low. Call 911!";
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
                        tblBloodSugar row = dc.tblBloodSugars.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.tblBloodSugars.Remove(row);

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
