using System;
using System.Collections.Generic;

#nullable disable

namespace LungTracking.UI
{
    public partial class TblPatientProviderAccess
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid ProviderId { get; set; }
    }
}
