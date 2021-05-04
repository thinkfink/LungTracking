using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.UI
{
    public partial class TblUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastLogin { get; set; }

        public virtual TblCaregiver TblCaregiver { get; set; }
        public virtual TblPatient TblPatient { get; set; }
        public virtual TblProvider TblProvider { get; set; }
    }
}
