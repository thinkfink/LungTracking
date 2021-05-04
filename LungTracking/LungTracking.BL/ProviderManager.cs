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
    public static class ProviderManager
    {
        public async static Task<IEnumerable<Models.Provider>> Load()
        {
            try
            {
                List<Provider> providers = new List<Provider>();
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        var pviders = (from dt in dc.tblProviders
                                       join du in dc.tblUsers on dt.UserId equals du.Id
                                       where dt.UserId == du.Id
                                       select new
                                       {
                                           dt.Id,
                                           dt.FirstName,
                                           dt.LastName,
                                           dt.City,
                                           dt.State,
                                           dt.PhoneNumber,
                                           dt.JobDescription,
                                           dt.ImagePath,
                                           dt.UserId,
                                           du.Username,
                                           du.Password,
                                           du.Role,
                                           du.Email,
                                           du.Created,
                                           du.LastLogin
                                       }).ToList();

                        if (pviders != null)
                        {
                            pviders.ForEach(app => providers.Add(new Provider
                            {
                                Id = app.Id,
                                FirstName = app.FirstName,
                                LastName = app.LastName,
                                City = app.City,
                                State = app.State,
                                PhoneNumber = app.PhoneNumber,
                                JobDescription = app.JobDescription,
                                ImagePath = app.ImagePath,
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
                        */
                    }
                });
                return providers;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<Models.Provider> LoadByProviderId(Guid providerId)
        {
            try
            {
                Models.Provider provider = new Models.Provider();
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblProvider tblProvider = dc.tblProviders.FirstOrDefault(c => c.Id == providerId);

                        if (tblProvider != null)
                        {
                            provider.Id = tblProvider.Id;
                            provider.FirstName = tblProvider.FirstName;
                            provider.LastName = tblProvider.LastName;
                            provider.City = tblProvider.City;
                            provider.State = tblProvider.State;
                            provider.PhoneNumber = tblProvider.PhoneNumber;
                            provider.UserId = tblProvider.UserId;
                        }
                        else
                        {
                            throw new Exception("Could not find the provider row");
                        }

                        tblUser tblUser = dc.tblUsers.FirstOrDefault(c => c.Id == provider.UserId);

                        if (tblUser != null)
                        {
                            provider.Username = tblUser.Username;
                            provider.Password = tblUser.Password;
                            provider.Role = tblUser.Role;
                            provider.Email = tblUser.Email;
                            provider.Created = tblUser.Created;
                            provider.LastLogin = tblUser.LastLogin;
                        }
                        else
                        {
                            throw new Exception("Could not find the user row");
                        }
                    }
                });
                return provider;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<IEnumerable<Models.Provider>> LoadByUserId(Guid userId)
        {
            try
            {
                List<Provider> results = new List<Provider>();
                await Task.Run(() =>
                {
                    if (userId != null)
                    {
                        using (LungTrackingEntities dc = new LungTrackingEntities())
                        {
                            var providers = (from dt in dc.tblProviders
                                              join du in dc.tblUsers on dt.UserId equals du.Id
                                              where dt.UserId == userId
                                              select new
                                              {
                                                  dt.Id,
                                                  dt.FirstName,
                                                  dt.LastName,
                                                  dt.City,
                                                  dt.State,
                                                  dt.PhoneNumber,
                                                  dt.JobDescription,
                                                  dt.ImagePath,
                                                  dt.UserId,
                                                  du.Username,
                                                  du.Password,
                                                  du.Role,
                                                  du.Email,
                                                  du.Created,
                                                  du.LastLogin
                                              }).ToList();

                            if (providers != null)
                            {
                                providers.ForEach(app => results.Add(new Provider
                                {
                                    Id = app.Id,
                                    FirstName = app.FirstName,
                                    LastName = app.LastName,
                                    City = app.City,
                                    State = app.State,
                                    PhoneNumber = app.PhoneNumber,
                                    JobDescription = app.JobDescription,
                                    ImagePath = app.ImagePath,
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
                                throw new Exception("Provider was not found.");
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


        public async static Task<Guid> Insert(string firstName, string lastName, string city, string state, string phoneNumber, string jobDescription, string imagePath, Guid userId, bool rollback = false)
        {
            try
            {
                Models.Provider provider = new Models.Provider
                {
                    FirstName = firstName,
                    LastName = lastName,
                    City = city,
                    State = state,
                    PhoneNumber = phoneNumber,
                    JobDescription = jobDescription,
                    ImagePath = imagePath,
                    UserId = userId
                };
                await Insert(provider, rollback);
                return provider.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Insert(Models.Provider provider, bool rollback = false)
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

                        tblProvider newrow = new tblProvider();

                        newrow.Id = Guid.NewGuid();
                        newrow.FirstName = provider.FirstName;
                        newrow.LastName = provider.LastName;
                        newrow.City = provider.City;
                        newrow.State = provider.State;
                        newrow.PhoneNumber = provider.PhoneNumber;
                        newrow.JobDescription = provider.JobDescription;
                        newrow.ImagePath = provider.ImagePath;
                        newrow.UserId = provider.UserId;

                        provider.Id = newrow.Id;

                        dc.tblProviders.Add(newrow);
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

        public async static Task<int> Update(Models.Provider provider, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                int results = 0;
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblProvider row = (from dt in dc.tblProviders where dt.Id == provider.Id select dt).FirstOrDefault();
                        int results = 0;
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            row.FirstName = provider.FirstName;
                            row.LastName = provider.LastName;
                            row.City = provider.City;
                            row.State = provider.State;
                            row.PhoneNumber = provider.PhoneNumber;
                            row.JobDescription = provider.JobDescription;
                            row.ImagePath = provider.ImagePath;
                            row.UserId = provider.UserId;

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
                        tblProvider row = dc.tblProviders.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.tblProviders.Remove(row);

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
