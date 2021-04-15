using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.PL
{
    public class spFindPatientAppsAndPills
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
		public string Sex { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime AppointmentDate { get; set; }
		public TimeSpan AppointmentTimeStart { get; set; }
		public TimeSpan AppointmentTimeEnd { get; set; }
		public string AppointmentDescription { get; set; }
		public string AppointmentLocation { get; set; }
		public string MedicationName { get; set; }
		public string MedicationDosageTotal { get; set; }
		public string MedicationDosagePerPill { get; set; }
		public string MedicationInstructions { get; set; }
		public int MedicationNumberOfPills { get; set; }
		public int MedicationQuantityOfFill { get; set; }
		public DateTime MedicationRefillDate { get; set; }
		public TimeSpan PillTime { get; set; }
		public Guid MedicationTimeMedicationId { get; set; }
	}
}
