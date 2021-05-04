using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.UI
{
    public partial class TblTemperature
    {
        public Guid Id { get; set; }
        public decimal TempNumber { get; set; }
        public bool BeginningEnd { get; set; }
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }

        public virtual TblPatient Patient { get; set; }
    }
}
