using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.PL
{
    public partial class tblAppointment
    {
        public Guid Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTimeStart { get; set; }
        public TimeSpan AppointmentTimeEnd { get; set; }
        public string AppointmentDescription { get; set; }
        public string AppointmentLocation { get; set; }
        public Guid PatientId { get; set; }

        public virtual tblPatient Patient { get; set; }
    }
}
