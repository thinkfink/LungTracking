using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class MedicationTime
    {
        public Guid Id { get; set; }
        public DateTime PillTime { get; set; }
        public Guid MedicationId { get; set; }
        public Guid PatientId { get; set; }
    }
}
