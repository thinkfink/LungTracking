using System;
using System.Collections.Generic;
using System.Text;

namespace LungTracking.Mobile.Models
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
    }
}
