using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.UI
{
    public partial class TblBloodSugar
    {
        public Guid Id { get; set; }
        public int BloodSugarNumber { get; set; }
        public DateTime TimeOfDay { get; set; }
        public int UnitsOfInsulinGiven { get; set; }
        public string TypeOfInsulinGiven { get; set; }
        public string Notes { get; set; }
        public Guid PatientId { get; set; }

        public virtual TblPatient Patient { get; set; }
    }
}
