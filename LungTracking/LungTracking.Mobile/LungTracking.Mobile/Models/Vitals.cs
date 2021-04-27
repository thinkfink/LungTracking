using System;
using System.Collections.Generic;
using System.Text;

namespace LungTracking.Mobile.Models
{
    public class Vitals
    {
        public Guid Id { get; set; }
        public decimal PEFNumber { get; set; }
        public decimal FEV1Number { get; set; }
        public int BPsystolic { get; set; }
        public int BPdiastolic { get; set; }
        public decimal PulseNumber { get; set; }
        public decimal TempNumber { get; set; }
        public Enum BeginningEnd { get; set; }
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }
    }
}
