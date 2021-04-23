﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class BloodPressure
    {
        public Guid Id { get; set; }
        public int BPsystolic { get; set; }
        public int BPdiastolic { get; set; }
        public int MAP { get; set; }
        public bool BeginningEnd { get; set; }
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }
    }
}
