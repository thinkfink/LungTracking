using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{

    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastLogin { get; set; }

        // Constructors
        public User() { }

        public User(string userName, string password)
        {
            // Logging In
            Username = userName;
            Password = password;
            LastLogin = DateTime.Now;
        }

        public User(Guid id, string username, string password, int role, string email)
        {
            // Updating user
            Id = id;
            Username = username;
            Password = password;
            Role = role;
            Email = email;
        }

        public User(string username, string password, int role, string email)
        {
            // Creating user
            Username = username;
            Password = password;
            Role = role;
            Email = email;
            Created = DateTime.Now;
            LastLogin = DateTime.Now;
        }

        // Variables for new password change
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

    }
}