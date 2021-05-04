using System;
using System.Collections.Generic;
using System.Text;

namespace LungTracking.Mobile.Models
{
    public class BloodSugar
    {
        public Guid Id { get; set; }
        public decimal BloodSugarNumber { get; set; }
        public DateTime TimeOfDay { get; set; }
        public int UnitsOfInsulinGiven { get; set; }
        public string TypeOfInsulinGiven { get; set; }
        public Guid PatientId { get; set; }
    }
}
