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
    public static class UserManager
    {
        private static string GetHash(string password)
        {
            using (var hash = new System.Security.Cryptography.SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(hash.ComputeHash(hashbytes));
            }
        }

        public class LoginFailureException : Exception
        {
            public LoginFailureException() : base("Cannot log in with these credentials.Your IP address has been saved.")
            {

            }

            public LoginFailureException(string message) : base(message)
            {

            }
        }

        public async static Task<IEnumerable<Models.User>> Load()
        {
            try
            {
                List<User> users = new List<User>();
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        dc.tblUsers
                        .ToList()
                        .ForEach(u => users.Add(new User
                        {
                            Id = u.Id,
                            Username = u.Username,
                            Password = u.Password,
                            Role = u.Role,
                            Email = u.Email,
                            Created = u.Created,
                            LastLogin = u.LastLogin
                        }));
                    }
                });
                return users;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<Models.User> LoadByUserId(Guid userId)
        {
            try
            {
                Models.User user = new Models.User();
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblUser tblUser = dc.tblUsers.FirstOrDefault(c => c.Id == userId);

                        if (tblUser != null)
                        {
                            user.Id = tblUser.Id;
                            user.Username = tblUser.Username;
                            user.Password = tblUser.Password;
                            user.Role = tblUser.Role;
                            user.Email = tblUser.Email;
                            user.Created = tblUser.Created;
                            user.LastLogin = tblUser.LastLogin;
                        }
                        else
                        {
                            throw new Exception("Could not find the row");
                        }
                    }
                });
                return user;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<Guid> Insert(string username, string password, int role, string email, bool rollback = false)
        {
            try
            {
                Models.User user = new Models.User
                {
                    Username = username,
                    Password = GetHash(password),
                    Role = role,
                    Email = email,
                    Created = DateTime.Now,
                    LastLogin = DateTime.Now
                };
                await Insert(user, rollback);
                return user.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> Insert(Models.User user, bool rollback = false)
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

                        tblUser newrow = new tblUser();

                        newrow.Id = Guid.NewGuid();
                        newrow.Username = user.Username;
                        newrow.Password = user.Password;
                        newrow.Role = user.Role;
                        newrow.Email = user.Email;
                        newrow.Created = user.Created;
                        newrow.LastLogin = user.LastLogin;

                        user.Id = newrow.Id;

                        dc.tblUsers.Add(newrow);
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

        public async static Task<int> Update(Models.User user, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;
                int results = 0;
                await Task.Run(() =>
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblUser row = (from dt in dc.tblUsers where dt.Id == user.Id select dt).FirstOrDefault();
                        int results = 0;
                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            row.Username = user.Username;
                            row.Password = user.Password;
                            row.Role = user.Role;
                            row.Email = user.Email;
                            row.Created = user.Created;
                            row.LastLogin = user.LastLogin;

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

        public static void Seed()
        {
            // Hardcoding a couple of users with hashed passwords.
            User user = new User { Username = "tfink", Password = "tyler", LastLogin = DateTime.Now };
            Insert(user);
        }

        public async static Task<bool> Login(User user, bool rollback = false)
        {
            // Check if a username was entered
            // Check if a password was entered
            // Check if username exists in database
            // Check if password matches
            // Log in if all 4 checks above passes
            try
            {
                IDbContextTransaction transaction = null;
                bool loggedIn = false;
                await Task.Run(() =>
                {
                    if (!string.IsNullOrEmpty(user.Username))
                    {
                        if (!string.IsNullOrEmpty(user.Password))
                        {
                            using (LungTrackingEntities dc = new LungTrackingEntities())
                            {
                                tblUser tblUser = dc.tblUsers.FirstOrDefault(u => u.Username == user.Username);
                                if (tblUser != null)
                                {
                                    if (rollback) transaction = dc.Database.BeginTransaction();

                                    if (tblUser.Password == user.Password || GetHash(tblUser.Password) == GetHash(user.Password) || tblUser.Password == GetHash(user.Password))
                                    {
                                        user.Id = tblUser.Id;
                                        tblUser.LastLogin = DateTime.Now;
                                        loggedIn = true;

                                        if (rollback) transaction.Rollback();
                                    }
                                    else
                                    {
                                        loggedIn = false;
                                    }
                                }
                                else
                                {
                                    throw new Exception("Username could not be found.");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Please enter your password");
                        }
                    }
                    else
                    {
                        throw new Exception("Please enter your username");
                    }
                });
                return loggedIn;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async static Task<int> UpdatePassword(Guid id, string password, string newPassword, string confirmPassword, bool rollback = false)
        {

            if (newPassword == confirmPassword)
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

                            tblUser updaterow = (from dt in dc.tblUsers where dt.Id == id select dt).FirstOrDefault();

                            if (GetHash(password) == GetHash(updaterow.Password) || password == updaterow.Password || GetHash(password) == updaterow.Password)
                            {
                                updaterow.Password = GetHash(newPassword);
                                results = dc.SaveChanges();
                                if (rollback) transaction.Rollback();
                            }
                            else
                            {
                                throw new Exception("Password does not match existing user password.");
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
            else
            {
                throw new Exception("New Password and Confirm Password do not match.");
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
                        tblUser row = dc.tblUsers.FirstOrDefault(c => c.Id == id);

                        if (row != null)
                        {
                            if (rollback) transaction = dc.Database.BeginTransaction();

                            dc.tblUsers.Remove(row);

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
