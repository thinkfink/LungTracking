using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class PatientCaregiverAccess
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid CaregiverId { get; set; }
    }
}
