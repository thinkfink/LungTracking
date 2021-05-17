using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class BloodSugar
    {
        public Guid Id { get; set; }
        [DisplayName("Blood Sugar Number")]
        public int BloodSugarNumber { get; set; }
        [DisplayName("Time of Day")]
        public DateTime TimeOfDay { get; set; }
        [DisplayName("Units of Insulin Given")]
        public int UnitsOfInsulinGiven { get; set; }
        [DisplayName("Type of Insulin")]
        public string TypeOfInsulinGiven { get; set; }
        public string Notes { get; set; }
        public Guid PatientId { get; set; }

        // for "stored procedure" functionality - warning if blood sugar is below 50
        public string Alert { get; set; }
    }
}
