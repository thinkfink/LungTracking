using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class MedicationDetails
    {
        public Guid Id { get; set; }
        public string MedicationName { get; set; }
        public string MedicationDosageTotal { get; set; }
        public string MedicationDosagePerPill { get; set; }
        public string MedicationInstructions { get; set; }
        public int NumberOfPills { get; set; }
        public DateTime DateFilled { get; set; }
        public int QuantityOfFill { get; set; }
        public DateTime RefillDate { get; set; }
        public Guid PatientId { get; set; }

        // for "stored procedure" functionality - reminder for if it's 7 or fewer days before refill date 
        public string Reminder { get; set; }
    }
}
