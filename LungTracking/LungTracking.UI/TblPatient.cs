using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.UI
{
    public partial class TblPatient
    {
        public TblPatient()
        {
            TblAppointments = new HashSet<TblAppointment>();
            TblBloodPressures = new HashSet<TblBloodPressure>();
            TblBloodSugars = new HashSet<TblBloodSugar>();
            TblFev1s = new HashSet<TblFev1>();
            TblMedicationDetails = new HashSet<TblMedicationDetail>();
            TblMedicationTimes = new HashSet<TblMedicationTime>();
            TblPefs = new HashSet<TblPef>();
            TblPulses = new HashSet<TblPulse>();
            TblSleepWakes = new HashSet<TblSleepWake>();
            TblTemperatures = new HashSet<TblTemperature>();
            TblWeights = new HashSet<TblWeight>();
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

        public virtual TblUser User { get; set; }
        public virtual ICollection<TblAppointment> TblAppointments { get; set; }
        public virtual ICollection<TblBloodPressure> TblBloodPressures { get; set; }
        public virtual ICollection<TblBloodSugar> TblBloodSugars { get; set; }
        public virtual ICollection<TblFev1> TblFev1s { get; set; }
        public virtual ICollection<TblMedicationDetail> TblMedicationDetails { get; set; }
        public virtual ICollection<TblMedicationTime> TblMedicationTimes { get; set; }
        public virtual ICollection<TblPef> TblPefs { get; set; }
        public virtual ICollection<TblPulse> TblPulses { get; set; }
        public virtual ICollection<TblSleepWake> TblSleepWakes { get; set; }
        public virtual ICollection<TblTemperature> TblTemperatures { get; set; }
        public virtual ICollection<TblWeight> TblWeights { get; set; }
    }
}
