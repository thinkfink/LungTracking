using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.UI
{
    public partial class TblMedicationTime
    {
        public Guid Id { get; set; }
        public DateTime PillTime { get; set; }
        public Guid MedicationId { get; set; }
        public Guid PatientId { get; set; }

        public virtual TblPatient Patient { get; set; }
    }
}
