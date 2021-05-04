using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.UI
{
    public partial class TblWeight
    {
        public Guid Id { get; set; }
        public int WeightNumberInPounds { get; set; }
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }

        public virtual TblPatient Patient { get; set; }
    }
}
