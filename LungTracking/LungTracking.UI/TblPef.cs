using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.UI
{
    public partial class TblPef
    {
        public Guid Id { get; set; }
        public decimal Pefnumber { get; set; }
        public bool BeginningEnd { get; set; }
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }

        public virtual TblPatient Patient { get; set; }
    }
}
