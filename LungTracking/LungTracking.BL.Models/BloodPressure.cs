using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class BloodPressure
    {
        public Guid Id { get; set; }
        [DisplayName("Blood Pressure (Systolic)")]
        public int BPsystolic { get; set; }
        [DisplayName("Blood Pressure (Diastolic)")]
        public int BPdiastolic { get; set; }
        public int MAP { get; set; }
        [DisplayName("Beginning or End of Day")]
        public bool BeginningEnd { get; set; }
        [DisplayName("Time of Day")]
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }
    }
}
