using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.PL
{
    public partial class tblMedicationTime
    {
        public Guid Id { get; set; }
        public DateTime PillTime { get; set; }
        public Guid MedicationId { get; set; }
        public Guid PatientId { get; set; }

        public virtual tblPatient Patient { get; set; }
    }
}
