using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

namespace LungTracking.BL
{
    public static class ProviderManager
    {
        public static List<Provider> Load()
        {
            using (LungTrackingEntities dc = new LungTrackingEntities())
            {
                List<Provider> providers = new List<Provider>();

                dc.tblProviders
                    .ToList()
                    .ForEach(u => providers.Add(new Provider
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        City = u.City,
                        State = u.State,
                        PhoneNumber = u.PhoneNumber,
                        JobDescription = u.JobDescription,
                        ImagePath = u.ImagePath,
                        UserId = u.UserId
                    }));
                return providers;
            }
        }
        public static int Insert(string firstName, string lastName, string city, string state, string phoneNumber, string jobDescription, string imagePath, Guid userId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblProvider newprovider = new tblProvider
                    {
                        Id = Guid.NewGuid(),
                        FirstName = firstName,
                        LastName = lastName,
                        City = city,
                        State = state,
                        PhoneNumber = phoneNumber,
                        JobDescription = jobDescription,
                        ImagePath = imagePath,
                        UserId = userId
                    };
                    dc.tblProviders.Add(newprovider);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Insert(Provider provider)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblProvider newprovider = new tblProvider
                    {
                        Id = Guid.NewGuid(),
                        FirstName = provider.FirstName,
                        LastName = provider.LastName,
                        City = provider.City,
                        State = provider.State,
                        PhoneNumber = provider.PhoneNumber,
                        JobDescription = provider.JobDescription,
                        ImagePath = provider.ImagePath,
                        UserId = provider.UserId
                    };
                    dc.tblProviders.Add(newprovider);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Guid id, string firstName, string lastName, string city, string state, string phoneNumber, string jobDescription, string imagePath, Guid userId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblProvider updaterow = (from dt in dc.tblProviders where dt.Id == id select dt).FirstOrDefault();
                    updaterow.FirstName = firstName;
                    updaterow.LastName = lastName;
                    updaterow.City = city;
                    updaterow.State = state;
                    updaterow.PhoneNumber = phoneNumber;
                    updaterow.JobDescription = jobDescription;
                    updaterow.ImagePath = imagePath;
                    updaterow.UserId = userId;
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int Update(Provider provider)
        {
            return Update(provider.Id, provider.FirstName, provider.LastName, provider.City, provider.State, provider.PhoneNumber, provider.JobDescription, provider.ImagePath, provider.UserId);
        }

        public static Provider LoadByProviderId(Guid providerId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblProvider row = (from dt in dc.tblProviders where dt.Id == providerId select dt).FirstOrDefault();
                    if (row != null)
                    {
                        return new Provider
                        {
                            Id = row.Id,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            City = row.City,
                            State = row.State,
                            PhoneNumber = row.PhoneNumber,
                            JobDescription = row.JobDescription,
                            ImagePath = row.ImagePath,
                            UserId = row.UserId
                        };
                    }
                    else
                    {
                        throw new Exception("Provider was not found.");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Provider LoadByUserId(Guid userId)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblProvider row = (from dt in dc.tblProviders where dt.UserId == userId select dt).FirstOrDefault();
                    if (row != null)
                    {
                        return new Provider
                        {
                            Id = row.Id,
                            FirstName = row.FirstName,
                            LastName = row.LastName,
                            City = row.City,
                            State = row.State,
                            PhoneNumber = row.PhoneNumber,
                            JobDescription = row.JobDescription,
                            ImagePath = row.ImagePath,
                            UserId = row.UserId
                        };
                    }
                    else
                    {
                        throw new Exception("Provider was not found.");
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
                    tblProvider deleterow = (from dt in dc.tblProviders where dt.Id == id select dt).FirstOrDefault();
                    dc.tblProviders.Remove(deleterow);
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
