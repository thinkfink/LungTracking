using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class AppointmentManager
    {
        public static List<Appointment> Load()
        {
            using (LungTrackingEntities dc = new LungTrackingEntities())
            {
                List<Appointment> appointments = new List<Appointment>();

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
        public static int Insert(string date, string timeStart, string timeEnd, string description, string location, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblAppointment newappointment = new tblAppointment
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Parse(date),
                        TimeStart = TimeSpan.Parse(timeStart),
                        TimeEnd = TimeSpan.Parse(timeEnd),
                        Description = description,
                        Location = location,
                        PatientId = patientId
                    };
                    dc.tblAppointments.Add(newappointment);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Insert(DateTime date, TimeSpan timeStart, TimeSpan timeEnd, string description, string location, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblAppointment newappointment = new tblAppointment
                    {
                        Id = Guid.NewGuid(),
                        Date = date,
                        TimeStart = timeStart,
                        TimeEnd = timeEnd,
                        Description = description,
                        Location = location,
                        PatientId = patientId
                    };
                    dc.tblAppointments.Add(newappointment);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Insert(Appointment appointment)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblAppointment newappointment = new tblAppointment
                    {
                        Id = Guid.NewGuid(),
                        Date = appointment.Date,
                        TimeStart = appointment.TimeStart,
                        TimeEnd = appointment.TimeEnd,
                        Description = appointment.Description,
                        Location = appointment.Location,
                        PatientId = appointment.PatientId
                    };
                    dc.tblAppointments.Add(newappointment);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Guid id, string date, string timeStart, string timeEnd, string description, string location, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblAppointment updaterow = (from dt in dc.tblAppointments where dt.Id == id select dt).FirstOrDefault();
                    updaterow.Date = DateTime.Parse(date);
                    updaterow.TimeStart = TimeSpan.Parse(timeStart);
                    updaterow.TimeEnd = TimeSpan.Parse(timeEnd);
                    updaterow.Description = description;
                    updaterow.Location = location;
                    updaterow.PatientId = patientId;
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Update(Guid id, DateTime date, TimeSpan timeStart, TimeSpan timeEnd, string description, string location, Guid patientId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblAppointment updaterow = (from dt in dc.tblAppointments where dt.Id == id select dt).FirstOrDefault();
                    updaterow.Date = date;
                    updaterow.TimeStart = timeStart;
                    updaterow.TimeEnd = timeEnd;
                    updaterow.Description = description;
                    updaterow.Location = location;
                    updaterow.PatientId = patientId;
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Update(Appointment appointment)
        {
            return Update(appointment.Id, appointment.Date, appointment.TimeStart, appointment.TimeEnd, appointment.Description, appointment.Location, appointment.PatientId);
        }

        public static Appointment LoadByAppointmentId(Guid appointmentId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblAppointment row = (from dt in dc.tblAppointments where dt.Id == appointmentId select dt).FirstOrDefault();
                    if (row != null)
                    {
                        return new Appointment
                        {
                            Id = row.Id,
                            Date = row.Date,
                            TimeStart = row.TimeStart,
                            TimeEnd = row.TimeEnd,
                            Description = row.Description,
                            Location = row.Location,
                            PatientId = row.PatientId
                        };
                    }
                    else
                    {
                        throw new Exception("Appointment was not found.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<Appointment> LoadByPatientId(Guid patientId)
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
                    tblAppointment deleterow = (from dt in dc.tblAppointments where dt.Id == id select dt).FirstOrDefault();
                    dc.tblAppointments.Remove(deleterow);
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
