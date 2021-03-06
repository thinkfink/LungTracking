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
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        var pts = (from dt in dc.tblPatients
                                          join du in dc.tblUsers on dt.UserId equals du.Id
                                          where dt.UserId == du.Id
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
                                              dt.UserId,
                                              du.Username,
                                              du.Password,
                                              du.Role,
                                              du.Email,
                                              du.Created,
                                              du.LastLogin
                                          }).ToList();

                        if (pts != null)
                        {
                            pts.ForEach(app => patients.Add(new Patient
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
                                UserId = app.UserId,
                                Username = app.Username,
                                Password = app.Password,
                                Role = app.Role,
                                Email = app.Email,
                                Created = app.Created,
                                LastLogin = app.LastLogin
                            }));
                        }

                        /*
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
                        */
                    }
                });
                return patients;
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
                Models.Patient patient = new Models.Patient();
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblPatient tblPatient = dc.tblPatients.FirstOrDefault(c => c.Id == patientId);

                        if (tblPatient != null)
                        {
                            patient.Id = tblPatient.Id;
                            patient.FirstName = tblPatient.FirstName;
                            patient.LastName = tblPatient.LastName;
                            patient.City = tblPatient.City;
                            patient.State = tblPatient.State;
                            patient.PhoneNumber = tblPatient.PhoneNumber;
                            patient.UserId = tblPatient.UserId;
                        }
                        else
                        {
                            throw new Exception("Could not find the patient row");
                        }

                        tblUser tblUser = dc.tblUsers.FirstOrDefault(c => c.Id == patient.UserId);

                        if (tblUser != null)
                        {
                            patient.Username = tblUser.Username;
                            patient.Password = tblUser.Password;
                            patient.Role = tblUser.Role;
                            patient.Email = tblUser.Email;
                            patient.Created = tblUser.Created;
                            patient.LastLogin = tblUser.LastLogin;
                        }
                        else
                        {
                            throw new Exception("Could not find the user row");
                        }
                    }
                });
                return patient;
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
                List<Patient> results = new List<Patient>();
                await Task.Run(() =>
                {
                    if (userId != null)
                    {
                        using (LungTrackingEntities dc = new LungTrackingEntities())
                        {

                            var patients = (from dt in dc.tblPatients
                                              join du in dc.tblUsers on dt.UserId equals du.Id
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
                                                  dt.UserId,
                                                  du.Username,
                                                  du.Password,
                                                  du.Role,
                                                  du.Email,
                                                  du.Created,
                                                  du.LastLogin
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
                                    UserId = app.UserId,
                                    Username = app.Username,
                                    Password = app.Password,
                                    Role = app.Role,
                                    Email = app.Email,
                                    Created = app.Created,
                                    LastLogin = app.LastLogin
                                }));
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
                });
                return results;
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
                int results = 0;
                await Task.Run(() =>
                {
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

        public async static Task<int> Update(Models.Patient patient, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                int results = 0;
                await Task.Run(() =>
                {
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
