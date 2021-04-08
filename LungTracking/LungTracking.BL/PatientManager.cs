using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class PatientManager
    {
        public static List<Patient> Load()
        {
            using (LungTrackingEntities dc = new LungTrackingEntities())
            {
                List<Patient> patients = new List<Patient>();

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
        public static int Insert(string firstName, string lastName, string sex, string dateOfBirth, string streetAddress, string city, string state, string phoneNumber, Guid userId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPatient newpatient = new tblPatient
                    {
                        Id = Guid.NewGuid(),
                        FirstName = firstName,
                        LastName = lastName,
                        Sex = sex,
                        DateOfBirth = DateTime.Parse(dateOfBirth),
                        StreetAddress = streetAddress,
                        City = city,
                        State = state,
                        PhoneNumber = phoneNumber,
                        UserId = userId
                    };
                    dc.tblPatients.Add(newpatient);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Insert(Patient patient)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPatient newpatient = new tblPatient
                    {
                        Id = Guid.NewGuid(),
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        Sex = patient.Sex,
                        DateOfBirth = patient.DateOfBirth,
                        StreetAddress = patient.StreetAddress,
                        City = patient.City,
                        State = patient.State,
                        PhoneNumber = patient.PhoneNumber,
                        UserId = patient.UserId
                    };
                    dc.tblPatients.Add(newpatient);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Guid id, string firstName, string lastName, string sex, DateTime dateOfBirth, string streetAddress, string city, string state, string phoneNumber, Guid userId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPatient updaterow = (from dt in dc.tblPatients where dt.Id == id select dt).FirstOrDefault();
                    updaterow.FirstName = firstName;
                    updaterow.LastName = lastName;
                    updaterow.Sex = sex;
                    updaterow.DateOfBirth = dateOfBirth;
                    updaterow.StreetAddress = streetAddress;
                    updaterow.City = city;
                    updaterow.State = state;
                    updaterow.PhoneNumber = phoneNumber;
                    updaterow.UserId = userId;
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Update(Patient patient)
        {
            return Update(patient.Id, patient.FirstName, patient.LastName, patient.Sex, patient.DateOfBirth, patient.StreetAddress, patient.City, patient.State, patient.PhoneNumber, patient.UserId);
        }

        public static Patient LoadByPatientId(Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPatient row = (from dt in dc.tblPatients where dt.Id == patientId select dt).FirstOrDefault();
                    if (row != null)
                    {
                        return new Patient
                        {
                            Id = row.Id,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            Sex = row.Sex,
                            DateOfBirth = row.DateOfBirth,
                            StreetAddress = row.StreetAddress,
                            City = row.City,
                            State = row.State,
                            PhoneNumber = row.PhoneNumber,
                            UserId = row.UserId
                        };
                    }
                    else
                    {
                        throw new Exception("Patient was not found.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static Patient LoadByUserId(Guid userId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPatient row = (from dt in dc.tblPatients where dt.UserId == userId select dt).FirstOrDefault();
                    if (row != null)
                    {
                        return new Patient
                        {
                            Id = row.Id,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            Sex = row.Sex,
                            DateOfBirth = row.DateOfBirth,
                            StreetAddress = row.StreetAddress,
                            City = row.City,
                            State = row.State,
                            PhoneNumber = row.PhoneNumber,
                            UserId = row.UserId
                        };
                    }
                    else
                    {
                        throw new Exception("Patient was not found.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Delete(Guid id)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblPatient deleterow = (from dt in dc.tblPatients where dt.Id == id select dt).FirstOrDefault();
                    dc.tblPatients.Remove(deleterow);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
