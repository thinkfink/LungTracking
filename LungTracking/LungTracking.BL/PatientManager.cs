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
    public static class PatientManager
    {
        public async static Task<IEnumerable<Models.Patient>> Load()
        {
            try
            {
                List<Patient> patients = new List<Patient>();

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    dc.tblPatients
                        .ToList()
                        .ForEach(u => patients.Add(new Patient
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Sex = u.Sex,
                            DateOfBirth = u.DateOfBirth,
                            StreetAddress = u.StreetAddress,
                            City = u.City,
                            State = u.State,
                            PhoneNumber = u.PhoneNumber,
                            UserId = u.UserId
                        }));
                    return patients;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<Models.Patient> LoadByPatientId(Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPatient tblPatient = dc.tblPatients.FirstOrDefault(c => c.Id == patientId);
                    Models.Patient patient = new Models.Patient();

                    if (tblPatient != null)
                    {
                        patient.Id = tblPatient.Id;
                        patient.FirstName = tblPatient.FirstName;
                        patient.LastName = tblPatient.LastName;
                        patient.Sex = tblPatient.Sex;
                        patient.DateOfBirth = tblPatient.DateOfBirth;
                        patient.StreetAddress = tblPatient.StreetAddress;
                        patient.City = tblPatient.City;
                        patient.State = tblPatient.State;
                        patient.PhoneNumber = tblPatient.PhoneNumber;
                        patient.UserId = tblPatient.UserId;
                        return patient;
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

        public async static Task<IEnumerable<Models.Patient>> LoadByUserId(Guid userId)
        {
            try
            {
                if (userId != null)
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {

                        List<Patient> results = new List<Patient>();

                        var patients = (from dt in dc.tblPatients
                                          where dt.UserId == userId
                                          select new
                                          {
                                              dt.Id,
                                              dt.FirstName,
                                              dt.LastName,
                                              dt.Sex,
                                              dt.DateOfBirth,
                                              dt.StreetAddress,
                                              dt.City,
                                              dt.State,
                                              dt.PhoneNumber,
                                              dt.UserId
                                          }).ToList();

                        if (patients != null)
                        {
                            patients.ForEach(app => results.Add(new Patient
                            {
                                Id = app.Id,
                                FirstName = app.FirstName,
                                LastName = app.LastName,
                                Sex = app.Sex,
                                DateOfBirth = app.DateOfBirth,
                                StreetAddress = app.StreetAddress,
                                City = app.City,
                                State = app.State,
                                PhoneNumber = app.PhoneNumber,
                                UserId = app.UserId
                            }));
                            return results;
                        }
                        else
                        {
                            throw new Exception("Patient was not found.");
                        }
                    }
                }
                else
                {
                    throw new Exception("Please provide a user Id.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async static Task<Guid> Insert(string firstName, string lastName, string sex, DateTime dateOfBirth, string streetAddress, string city, string state, string phoneNumber, Guid userId, bool rollback = false)
        {
            try
            {
                Models.Patient patient = new Models.Patient
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Sex = sex,
                    DateOfBirth = dateOfBirth,
                    StreetAddress = streetAddress,
                    City = city,
                    State = state,
                    PhoneNumber = phoneNumber,
                    UserId = userId
                };
                await Insert(patient, rollback);
                return patient.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Insert(Models.Patient patient, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPatient newrow = new tblPatient();

                    newrow.Id = Guid.NewGuid();
                    newrow.FirstName = patient.FirstName;
                    newrow.LastName = patient.LastName;
                    newrow.Sex = patient.Sex;
                    newrow.DateOfBirth = patient.DateOfBirth;
                    newrow.StreetAddress = patient.StreetAddress;
                    newrow.City = patient.City;
                    newrow.State = patient.State;
                    newrow.PhoneNumber = patient.PhoneNumber;
                    newrow.UserId = patient.UserId;

                    patient.Id = newrow.Id;

                    dc.tblPatients.Add(newrow);
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

        public async static Task<int> Update(Models.Patient patient, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPatient row = (from dt in dc.tblPatients where dt.Id == patient.Id select dt).FirstOrDefault();
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        row.FirstName = patient.FirstName;
                        row.LastName = patient.LastName;
                        row.Sex = patient.Sex;
                        row.DateOfBirth = patient.DateOfBirth;
                        row.StreetAddress = patient.StreetAddress;
                        row.City = patient.City;
                        row.State = patient.State;
                        row.PhoneNumber = patient.PhoneNumber;
                        row.UserId = patient.UserId;

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
                        tblPatient row = dc.tblPatients.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.tblPatients.Remove(row);

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
