using System;
using System.Collections.Generic;
using System.Text;

namespace LungTracking.Mobile.Models
{
    public class PEF
    {
        public Guid Id { get; set; }
        public decimal PEFNumber { get; set; }
        public Enum BeginningEnd { get; set; }
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }
    }
}
