using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.UI
{
    public partial class TblMedicationDetail
    {
        public Guid Id { get; set; }
        public string MedicationName { get; set; }
        public string MedicationDosageTotal { get; set; }
        public string MedicationDosagePerPill { get; set; }
        public string MedicationInstructions { get; set; }
        public int NumberOfPills { get; set; }
        public DateTime DateFilled { get; set; }
        public int QuantityOfFill { get; set; }
        public DateTime RefillDate { get; set; }
        public Guid PatientId { get; set; }

        public virtual TblPatient Patient { get; set; }
    }
}
