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


        public async static Task<IEnumerable<Models.User>> Load()
        {
            try
            {
                List<User> users = new List<User>();

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
                    return users;
                }
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
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblUser tblUser = dc.tblUsers.FirstOrDefault(c => c.Id == userId);
                    Models.User user = new Models.User();

                    if (tblUser != null)
                    {
                        user.Id = tblUser.Id;
                        user.Username = tblUser.Username;
                        user.Password = tblUser.Password;
                        user.Role = tblUser.Role;
                        user.Email = tblUser.Email;
                        user.Created = tblUser.Created;
                        user.LastLogin = tblUser.LastLogin;
                        return user;
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

        public async static Task<int> Update(Models.User user, bool rollback = false)
        {
            try
            {
                IDbContextTransaction transaction = null;

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

        public static bool Login(User user)
        {
            // Check if a username was entered
            // Check if a password was entered
            // Check if username exists in database
            // Check if password matches
            // Log in if all 4 checks above passes
            try
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
                                if (tblUser.Password == user.Password || GetHash(tblUser.Password) == GetHash(user.Password) || tblUser.Password == GetHash(user.Password))
                                {
                                    user.Id = tblUser.Id;
                                    tblUser.LastLogin = DateTime.Now;
                                    return true;
                                }
                                else
                                {
                                    return false;
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
                    tblUser deleterow = (from dt in dc.tblUsers where dt.Id == id select dt).FirstOrDefault();
                    dc.tblUsers.Remove(deleterow);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int UpdatePassword(Guid id, string password, string newPassword, string confirmPassword)
        {

            if (newPassword == confirmPassword)
            {
                try
                {
                    using (LungTrackingEntities dc = new LungTrackingEntities())
                    {
                        tblUser updaterow = (from dt in dc.tblUsers where dt.Id == id select dt).FirstOrDefault();

                        if (GetHash(password) == GetHash(updaterow.Password) || password == updaterow.Password || GetHash(password) == updaterow.Password)
                        {
                            updaterow.Password = GetHash(newPassword);
                            return dc.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Password does not match existing user password.");
                        }
                    }
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
    }
}
