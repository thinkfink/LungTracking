using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class MedicationTracking
    {
        public Guid Id { get; set; }
        public DateTime PillTakenTime { get; set; }
        public Guid MedicationId { get; set; }
        public Guid PatientId { get; set; }

        // for "stored procedure" functionality
        public string Alert { get; set; }
    }
}
