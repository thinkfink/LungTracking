using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class Temperature
    {
        public Guid Id { get; set; }
        public decimal TempNumber { get; set; }
        public bool BeginningEnd { get; set; }
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }

        // for "stored procedure" functionality - warning if temperature is 100 degrees or higher
        public string Alert { get; set; }
    }
}
