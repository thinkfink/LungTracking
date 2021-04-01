﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class SleepWake
    {
        public Guid Id { get; set; }
        public Enum SleepOrWake { get; set; }
        public DateTime TimeOfDay { get; set; }
        public Guid PatientId { get; set; }
    }
}
