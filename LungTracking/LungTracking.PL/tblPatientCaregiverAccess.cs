using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.PL
{
    public partial class tblPatientCaregiverAccess
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid CaregiverId { get; set; }
    }
}
