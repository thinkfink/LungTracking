using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPJ.LungTracking.BL.Models
{
    public class Provider
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public char State { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public Guid UserId { get; set; }
    }
}