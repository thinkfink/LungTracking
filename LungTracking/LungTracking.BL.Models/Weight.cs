using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class Weight
    {
        public Guid Id { get; set; }
        [DisplayName("Weight")]
        public int WeightNumberInPounds { get; set; }
        [DisplayName("Time of Day")]
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }
    }
}
