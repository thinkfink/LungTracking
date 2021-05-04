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
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        dc.tblAppointments
                            .ToList()
                            .ForEach(u => appointments.Add(new Appointment
                            {
                                Id = u.Id,
                                StartDateTime = u.StartDateTime,
                                EndDateTime = u.EndDateTime,
                                Description = u.Description,
                                Location = u.Location,
                                PatientId = u.PatientId
                            }));
                    }
                });
                return appointments;
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
                Models.Appointment appointment = new Models.Appointment();
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblAppointment tblAppointment = dc.tblAppointments.FirstOrDefault(c => c.Id == appointmentId);

                        if (tblAppointment != null)
                        {
                            appointment.Id = tblAppointment.Id;
                            appointment.StartDateTime = tblAppointment.StartDateTime;
                            appointment.EndDateTime = tblAppointment.EndDateTime;
                            appointment.Description = tblAppointment.Description;
                            appointment.Location = tblAppointment.Location;
                            appointment.PatientId = tblAppointment.PatientId;
                        }
                        else
                        {
                            throw new Exception("Could not find the row");
                        }
                    }
                });
                return appointment;
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
                List<Appointment> results = new List<Appointment>();
                await Task.Run(() =>
                {
                    if (patientId != null)
                    {
                        using (LungTrackingEntities dc = new LungTrackingEntities())
                        {
                            var appointments = (from dt in dc.tblAppointments
                                                where dt.PatientId == patientId
                                                select new
                                                {
                                                    dt.Id,
                                                    dt.StartDateTime,
                                                    dt.EndDateTime,
                                                    dt.Description,
                                                    dt.Location,
                                                    dt.PatientId
                                                }).ToList();

                            if (appointments != null)
                            {
                                appointments.ForEach(app => results.Add(new Appointment
                                {
                                    Id = app.Id,
                                    StartDateTime = app.StartDateTime,
                                    EndDateTime = app.EndDateTime,
                                    Description = app.Description,
                                    Location = app.Location,
                                    PatientId = app.PatientId
                                }));
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
                });
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async static Task<Guid> Insert(DateTime startDateTime, DateTime endDateTime, string description, string location, Guid patientId, bool rollback = false)
        {
            try
            {
                Models.Appointment appointment = new Models.Appointment
                {
                    StartDateTime = startDateTime,
                    EndDateTime = endDateTime,
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
                int results = 0;
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        tblAppointment newrow = new tblAppointment();

                        newrow.Id = Guid.NewGuid();
                        newrow.StartDateTime = appointment.StartDateTime;
                        newrow.EndDateTime = appointment.EndDateTime;
                        newrow.Description = appointment.Description;
                        newrow.Location = appointment.Location;
                        newrow.PatientId = appointment.PatientId;

                        appointment.Id = newrow.Id;

                        dc.tblAppointments.Add(newrow);
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

        public async static Task<int> Update(Models.Appointment appointment, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                int results = 0;
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblAppointment row = (from dt in dc.tblAppointments where dt.Id == appointment.Id select dt).FirstOrDefault();
                        int results = 0;
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            row.StartDateTime = appointment.StartDateTime;
                            row.EndDateTime = appointment.EndDateTime;
                            row.Description = appointment.Description;
                            row.Location = appointment.Location;
                            row.PatientId = appointment.PatientId;

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
                        tblAppointment row = dc.tblAppointments.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.tblAppointments.Remove(row);

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
