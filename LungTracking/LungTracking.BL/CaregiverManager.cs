using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class CaregiverManager
    {
        public static List<Caregiver> Load()
        {
            using (LungTrackingEntities dc = new LungTrackingEntities())
            {
                List<Caregiver> caregivers = new List<Caregiver>();

                dc.tblCaregivers
                    .ToList()
                    .ForEach(u => caregivers.Add(new Caregiver
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        City = u.City,
                        State = u.State,
                        PhoneNumber = u.PhoneNumber,
                        UserId = u.UserId
                    }));
                return caregivers;
            }
        }
        public static int Insert(string firstName, string lastName, string city, string state, string phoneNumber, Guid userId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblCaregiver newcaregiver = new tblCaregiver
                    {
                        Id = Guid.NewGuid(),
                        FirstName = firstName,
                        LastName = lastName,
                        City = city,
                        State = state,
                        PhoneNumber = phoneNumber,
                        UserId = userId
                    };
                    dc.tblCaregivers.Add(newcaregiver);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Insert(Caregiver caregiver)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblCaregiver newcaregiver = new tblCaregiver
                    {
                        Id = Guid.NewGuid(),
                        FirstName = caregiver.FirstName,
                        LastName = caregiver.LastName,
                        City = caregiver.City,
                        State = caregiver.State,
                        PhoneNumber = caregiver.PhoneNumber,
                        UserId = caregiver.UserId
                    };
                    dc.tblCaregivers.Add(newcaregiver);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Guid id, string firstName, string lastName, string city, string state, string phoneNumber, Guid userId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblCaregiver updaterow = (from dt in dc.tblCaregivers where dt.Id == id select dt).FirstOrDefault();
                    updaterow.FirstName = firstName;
                    updaterow.LastName = lastName;
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

        public static int Update(Caregiver caregiver)
        {
            return Update(caregiver.Id, caregiver.FirstName, caregiver.LastName, caregiver.City, caregiver.State, caregiver.PhoneNumber, caregiver.UserId);
        }

        public static Caregiver LoadByCaregiverId(Guid caregiverId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblCaregiver row = (from dt in dc.tblCaregivers where dt.Id == caregiverId select dt).FirstOrDefault();
                    if (row != null)
                    {
                        return new Caregiver
                        {
                            Id = row.Id,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            City = row.City,
                            State = row.State,
                            PhoneNumber = row.PhoneNumber,
                            UserId = row.UserId
                        };
                    }
                    else
                    {
                        throw new Exception("Caregiver was not found.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static Caregiver LoadByUserId(Guid userId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblCaregiver row = (from dt in dc.tblCaregivers where dt.UserId == userId select dt).FirstOrDefault();
                    if (row != null)
                    {
                        return new Caregiver
                        {
                            Id = row.Id,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            City = row.City,
                            State = row.State,
                            PhoneNumber = row.PhoneNumber,
                            UserId = row.UserId
                        };
                    }
                    else
                    {
                        throw new Exception("Caregiver was not found.");
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
                    tblCaregiver deleterow = (from dt in dc.tblCaregivers where dt.Id == id select dt).FirstOrDefault();
                    dc.tblCaregivers.Remove(deleterow);
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
