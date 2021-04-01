using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class BloodSugar
    {
        public Guid Id { get; set; }
        public decimal BloodSugarNumber { get; set; }
        public DateTime TimeOfDay { get; set; }
        public int UnitsOfInsulinGiven { get; set; }
        public Guid PatientId { get; set; }
    }
}
