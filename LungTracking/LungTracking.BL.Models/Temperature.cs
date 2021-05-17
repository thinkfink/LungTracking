using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class Temperature
    {
        public Guid Id { get; set; }
        [DisplayName("Temperature")]

        public decimal TempNumber { get; set; }
        [DisplayName("Beginning or End of Day")]

        public bool BeginningEnd { get; set; }
        [DisplayName("Time of Day")]
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }

        // for "stored procedure" functionality - warning if temperature is 100 degrees or higher
        public string Alert { get; set; }
    }
}
