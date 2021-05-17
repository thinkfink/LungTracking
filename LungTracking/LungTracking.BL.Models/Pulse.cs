using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class Pulse
    {
        public Guid Id { get; set; }
        [DisplayName("Pulse")]

        public int PulseNumber { get; set; }
        [DisplayName("Beginning or End of Day")]

        public bool BeginningEnd { get; set; }
        [DisplayName("Time of Day")]

        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }
    }
}
