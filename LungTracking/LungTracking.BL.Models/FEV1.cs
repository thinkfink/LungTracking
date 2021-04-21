using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class FEV1
    {
        public Guid Id { get; set; }
        public decimal FEV1Number { get; set; }
        public bool BeginningEnd { get; set; }
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }

        // for "stored procedure" functionality - warning at 10% drop
        public string Alert { get; set; }
    }
}
