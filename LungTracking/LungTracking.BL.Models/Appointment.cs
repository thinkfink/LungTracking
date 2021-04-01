using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentTimeStart { get; set; }
        public DateTime AppointmentTimeEnd { get; set; }
        public string AppointmentDescription { get; set; }
        public string AppointmentLocation { get; set; }
        public Guid PatientId { get; set; }
    }
}
