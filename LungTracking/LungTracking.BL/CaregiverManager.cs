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
    public static class CaregiverManager
    {
        public async static Task<IEnumerable<Models.Caregiver>> Load()
        {
            try
            {
                List<Caregiver> caregivers = new List<Caregiver>();

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
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
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<Models.Caregiver> LoadByCaregiverId(Guid caregiverId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblCaregiver tblCaregiver = dc.tblCaregivers.FirstOrDefault(c => c.Id == caregiverId);
                    Models.Caregiver caregiver = new Models.Caregiver();

                    if (tblCaregiver != null)
                    {
                        caregiver.Id = tblCaregiver.Id;
                        caregiver.FirstName = tblCaregiver.FirstName;
                        caregiver.LastName = tblCaregiver.LastName;
                        caregiver.City = tblCaregiver.City;
                        caregiver.State = tblCaregiver.State;
                        caregiver.PhoneNumber = tblCaregiver.PhoneNumber;
                        caregiver.UserId = tblCaregiver.UserId;
                        return caregiver;
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

        public async static Task<IEnumerable<Models.Caregiver>> LoadByUserId(Guid userId)
        {
            try
            {
                if (userId != null)
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {

                        List<Caregiver> results = new List<Caregiver>();

                        var caregivers = (from dt in dc.tblCaregivers
                                            where dt.UserId == userId
                                            select new
                                            {
                                                dt.Id,
                                                dt.FirstName,
                                                dt.LastName,
                                                dt.City,
                                                dt.State,
                                                dt.PhoneNumber,
                                                dt.UserId
                                            }).ToList();

                        if (caregivers != null)
                        {
                            caregivers.ForEach(app => results.Add(new Caregiver
                            {
                                Id = app.Id,
                                FirstName = app.FirstName,
                                LastName = app.LastName,
                                City = app.City,
                                State = app.State,
                                PhoneNumber = app.PhoneNumber,
                                UserId = app.UserId
                            }));
                            return results;
                        }
                        else
                        {
                            throw new Exception("Caregiver was not found.");
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


        public async static Task<Guid> Insert(string firstName, string lastName, string city, string state, string phoneNumber, Guid userId, bool rollback = false)
        {
            try
            {
                Models.Caregiver caregiver = new Models.Caregiver
                {
                    FirstName = firstName,
                    LastName = lastName,
                    City = city,
                    State = state,
                    PhoneNumber = phoneNumber,
                    UserId = userId
                };
                await Insert(caregiver, rollback);
                return caregiver.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Insert(Models.Caregiver caregiver, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblCaregiver newrow = new tblCaregiver();

                    newrow.Id = Guid.NewGuid();
                    newrow.FirstName = caregiver.FirstName;
                    newrow.LastName = caregiver.LastName;
                    newrow.City = caregiver.City;
                    newrow.State = caregiver.State;
                    newrow.PhoneNumber = caregiver.PhoneNumber;
                    newrow.UserId = caregiver.UserId;

                    caregiver.Id = newrow.Id;

                    dc.tblCaregivers.Add(newrow);
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

        public async static Task<int> Update(Models.Caregiver caregiver, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblCaregiver row = (from dt in dc.tblCaregivers where dt.Id == caregiver.Id select dt).FirstOrDefault();
                    int results = 0;
                    if (row != null)
                    {
                        if (rollback) transaction = dc.Database.BeginTransaction();

                        row.FirstName = caregiver.FirstName;
                        row.LastName = caregiver.LastName;
                        row.City = caregiver.City;
                        row.State = caregiver.State;
                        row.PhoneNumber = caregiver.PhoneNumber;
                        row.UserId = caregiver.UserId;

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
                    tblCaregiver deleterow = (from dt in dc.tblCaregivers where dt.Id == id select dt).FirstOrDefault();
                    dc.tblCaregivers.Remove(deleterow);
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
