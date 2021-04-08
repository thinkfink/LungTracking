using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.PL
{
    public partial class tblPulse
    {
        public Guid Id { get; set; }
        public int PulseNumber { get; set; }
        public bool BeginningEnd { get; set; }
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }

        public virtual tblPatient Patient { get; set; }
    }
}
