using System;
using System.Collections.Generic;
using System.Text;

namespace LungTracking.Mobile.Models
{
    public class FEV1
    {
        public Guid Id { get; set; }
        public decimal FEV1Number { get; set; }
        public bool BeginningEnd { get; set; }
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }
        public string Alert { get; set; }
    }
}
