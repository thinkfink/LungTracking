using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.PL
{
    public partial class tblSleepWake
    {
        public Guid Id { get; set; }
        public bool SleepOrWake { get; set; }
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }

        public virtual tblPatient Patient { get; set; }
    }
}
