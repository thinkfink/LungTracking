using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using LungTracking.PL;

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


        public static List<User> Load()
        {
            using (LungTrackingEntities dc = new LungTrackingEntities())
            {
                List<User> users = new List<User>();

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
        public static int Insert(string username, string password, int role, string email)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblUser newuser = new tblUser
                    {
                        Id = Guid.NewGuid(),
                        Username = username,
                        Password = GetHash(password),
                        Role = role,
                        Email = email,
                        Created = DateTime.Now,
                        LastLogin = DateTime.Now
                    };
                    dc.tblUsers.Add(newuser);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Insert(User user)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblUser newuser = new tblUser
                    {
                        Id = Guid.NewGuid(),
                        Username = user.Username,
                        Password = GetHash(user.Password),
                        Role = user.Role,
                        Email = user.Email,
                        Created = DateTime.Now,
                        LastLogin = DateTime.Now
                    };
                    dc.tblUsers.Add(newuser);
                    return dc.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Update(Guid id, string username, string password, int role, string email)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblUser updaterow = (from dt in dc.tblUsers where dt.Id == id select dt).FirstOrDefault();
                    updaterow.Username = username;
                    updaterow.Password = GetHash(password);
                    updaterow.Role = role;
                    updaterow.Email = email;
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Update(User user)
        {
            return Update(user.Id, user.Username, user.Password, user.Role, user.Email);
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
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static User LoadById(Guid id)
        {
            try
            {
                using (LungTrackingEntities dc = new LungTrackingEntities())
                {
                    tblUser row = (from dt in dc.tblUsers where dt.Id == id select dt).FirstOrDefault();
                    if (row != null)
                    {
                        return new User
                        {
                            Id = row.Id,
                            Username = row.Username,
                            Password = row.Password,
                            Role = row.Role,
                            Email = row.Email,
                            Created = row.Created,
                            LastLogin = (DateTime)row.LastLogin
                        };
                    }
                    else
                    {
                        throw new Exception("User was not found.");
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
                    tblUser deleterow = (from dt in dc.tblUsers where dt.Id == id select dt).FirstOrDefault();
                    dc.tblUsers.Remove(deleterow);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
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
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("New Password and Confirm Password do not match.");
            }
        }
    }
}
