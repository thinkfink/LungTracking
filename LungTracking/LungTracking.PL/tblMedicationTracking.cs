using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.PL
{
    public partial class tblMedicationTracking
    {
        public Guid Id { get; set; }
        public DateTime PillTakenTime { get; set; }
        public Guid MedicationId { get; set; }
        public Guid PatientId { get; set; }
    }
}
