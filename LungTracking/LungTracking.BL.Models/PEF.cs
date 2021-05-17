using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class PEF
    {
        public Guid Id { get; set; }
        [DisplayName("PEF Number")]

        public decimal PEFNumber { get; set; }
        [DisplayName("Beginning or End of Day")]

        public bool BeginningEnd { get; set; }
        [DisplayName("Time of Day")]

        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }
    }
}
