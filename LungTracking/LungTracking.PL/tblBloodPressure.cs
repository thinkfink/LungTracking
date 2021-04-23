using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.PL
{
    public partial class tblBloodPressure
    {
        public Guid Id { get; set; }
        public int Bpsystolic { get; set; }
        public int Bpdiastolic { get; set; }
        public decimal? Map { get; set; }
        public bool BeginningEnd { get; set; }
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }

        public virtual tblPatient Patient { get; set; }
    }
}
