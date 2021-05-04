using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.UI
{
    public partial class TblPatientCaregiverAccess
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid CaregiverId { get; set; }
    }
}
