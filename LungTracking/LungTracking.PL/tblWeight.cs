using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.PL
{
    public partial class tblWeight
    {
        public Guid Id { get; set; }
        public int WeightNumberInPounds { get; set; }
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }

        public virtual tblPatient Patient { get; set; }
    }
}
