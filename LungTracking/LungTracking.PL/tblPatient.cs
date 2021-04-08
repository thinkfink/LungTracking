using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.PL
{
    public partial class tblPatient
    {
        public tblPatient()
        {
            tblAppointments = new HashSet<tblAppointment>();
            tblBloodPressures = new HashSet<tblBloodPressure>();
            tblBloodSugars = new HashSet<tblBloodSugar>();
            tblFev1s = new HashSet<tblFev1>();
            tblMedicationDetails = new HashSet<tblMedicationDetail>();
            tblMedicationTimes = new HashSet<tblMedicationTime>();
            tblPefs = new HashSet<tblPef>();
            tblPulses = new HashSet<tblPulse>();
            tblSleepWakes = new HashSet<tblSleepWake>();
            tblTemperatures = new HashSet<tblTemperature>();
            tblWeights = new HashSet<tblWeight>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public Guid UserId { get; set; }

        public virtual tblUser User { get; set; }
        public virtual ICollection<tblAppointment> tblAppointments { get; set; }
        public virtual ICollection<tblBloodPressure> tblBloodPressures { get; set; }
        public virtual ICollection<tblBloodSugar> tblBloodSugars { get; set; }
        public virtual ICollection<tblFev1> tblFev1s { get; set; }
        public virtual ICollection<tblMedicationDetail> tblMedicationDetails { get; set; }
        public virtual ICollection<tblMedicationTime> tblMedicationTimes { get; set; }
        public virtual ICollection<tblPef> tblPefs { get; set; }
        public virtual ICollection<tblPulse> tblPulses { get; set; }
        public virtual ICollection<tblSleepWake> tblSleepWakes { get; set; }
        public virtual ICollection<tblTemperature> tblTemperatures { get; set; }
        public virtual ICollection<tblWeight> tblWeights { get; set; }
    }
}
