using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.PL
{
    public partial class tblUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastLogin { get; set; }

        public virtual tblCaregiver tblCaregiver { get; set; }
        public virtual tblPatient tblPatient { get; set; }
        public virtual tblProvider tblProvider { get; set; }
    }
}
