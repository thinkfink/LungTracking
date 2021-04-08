using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.PL
{
    public partial class tblProvider
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public string JobDescription { get; set; }
        public string ImagePath { get; set; }
        public Guid UserId { get; set; }

        public virtual tblUser User { get; set; }
    }
}
