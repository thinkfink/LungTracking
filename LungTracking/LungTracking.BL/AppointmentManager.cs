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
    public static class AppointmentManager
    {
        public async static Task<IEnumerable<Models.Appointment>> Load()
        {
            try
            {
                List<Appointment> appointments = new List<Appointment>();

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    dc.tblAppointments
                        .ToList()
                        .ForEach(u => appointments.Add(new Appointment
                        {
                            Id = u.Id,
                            Date = u.Date,
                            TimeStart = u.TimeStart,
                            TimeEnd = u.TimeEnd,
                            Description = u.Description,
                            Location = u.Location,
                            PatientId = u.PatientId
                        }));
                    return appointments;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<Models.Appointment> LoadByAppointmentId(Guid appointmentId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblAppointment tblAppointment = dc.tblAppointments.FirstOrDefault(c => c.Id == appointmentId);
                    Models.Appointment appointment = new Models.Appointment();

                    if (tblAppointment != null)
                    {
                        appointment.Id = tblAppointment.Id;
                        appointment.Date = tblAppointment.Date;
                        appointment.TimeStart = tblAppointment.TimeStart;
                        appointment.TimeEnd = tblAppointment.TimeEnd;
                        appointment.Description = tblAppointment.Description;
                        appointment.Location = tblAppointment.Location;
                        appointment.PatientId = tblAppointment.PatientId;
                        return appointment;
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

        public async static Task<IEnumerable<Models.Appointment>> LoadByPatientId(Guid patientId)
        {
            try
            {
                if (patientId != null)
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {

                        List<Appointment> results = new List<Appointment>();

                        var appointments = (from dt in dc.tblAppointments
                                            where dt.PatientId == patientId
                                            select new
                                            {
                                                dt.Id,
                                                dt.Date,
                                                dt.TimeStart,
                                                dt.TimeEnd,
                                                dt.Description,
                                                dt.Location,
                                                dt.PatientId
                                            }).ToList();

                        if (appointments != null)
                        {
                            appointments.ForEach(app => results.Add(new Appointment
                            {
                                Id = app.Id,
                                Date = app.Date,
                                TimeStart = app.TimeStart,
                                TimeEnd = app.TimeEnd,
                                Description = app.Description,
                                Location = app.Location,
                                PatientId = app.PatientId
                            }));
                            return results;
                        }
                        else
                        {
                            throw new Exception("Appointment was not found.");
                        }
                    }
                }
                else
                {
                    throw new Exception("Please provide a patient Id.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async static Task<Guid> Insert(DateTime date, TimeSpan timeStart, TimeSpan timeEnd, string description, string location, Guid patientId, bool rollback = false)
        {
            try
            {
                Models.Appointment appointment = new Models.Appointment
                {
                    Date = date,
                    TimeStart = timeStart,
                    TimeEnd = timeEnd,
                    Description = description,
                    Location = location,
                    PatientId = patientId
                };
                await Insert(appointment, rollback);
                return appointment.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Insert(Models.Appointment appointment, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblAppointment newrow = new tblAppointment();

                    newrow.Id = Guid.NewGuid();
                    newrow.Date = appointment.Date;
                    newrow.TimeStart = appointment.TimeStart;
                    newrow.TimeEnd = appointment.TimeEnd;
                    newrow.Description = appointment.Description;
                    newrow.Location = appointment.Location;
                    newrow.PatientId = appointment.PatientId;

                    appointment.Id = newrow.Id;

                    dc.tblAppointments.Add(newrow);
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

        public async static Task<int> Update(Models.Appointment appointment, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblAppointment row = (from dt in dc.tblAppointments where dt.Id == appointment.Id select dt).FirstOrDefault();
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        row.Date = appointment.Date;
                        row.TimeStart = appointment.TimeStart;
                        row.TimeEnd = appointment.TimeEnd;
                        row.Description = appointment.Description;
                        row.Location = appointment.Location;
                        row.PatientId = appointment.PatientId;

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

        public static int Delete(Guid id)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblAppointment deleterow = (from dt in dc.tblAppointments where dt.Id == id select dt).FirstOrDefault();
                    dc.tblAppointments.Remove(deleterow);
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
