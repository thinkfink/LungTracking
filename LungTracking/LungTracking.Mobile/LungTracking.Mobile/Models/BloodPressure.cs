using System;
using System.Collections.Generic;
using System.Text;

namespace LungTracking.Mobile.Models
{
    public class BloodPressure
    {
        public Guid Id { get; set; }
        public int BPsystolic { get; set; }
        public int BPdiastolic { get; set; }
        public Enum BeginningEnd { get; set; }
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }
    }
}
