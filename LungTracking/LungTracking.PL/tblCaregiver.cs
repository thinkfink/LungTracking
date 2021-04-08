using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.PL
{
    public partial class tblCaregiver
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public Guid UserId { get; set; }

        public virtual tblUser User { get; set; }
    }
}
