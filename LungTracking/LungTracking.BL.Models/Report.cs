using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungTracking.BL.Models
{
    public class Report
    {
        public int AppointmentCount { get; set; }
        public int BloodPressureCount { get; set; }
        public int BloodSugarCount { get; set; }
        public int CaregiverCount { get; set; }
        public int FEV1Count { get; set; }
        public int MedicationDetailsCount { get; set; }
        public int MedicationTimeCount { get; set; }
        public int MedicationTrackingCount { get; set; }
        public int PatientCount { get; set; }
        public int PEFCount { get; set; }
        public int ProviderCount { get; set; }
        public int PulseCount { get; set; }
        public int SleepWakeCount { get; set; }
        public int TemperatureCount { get; set; }
        public int UserCount { get; set; }
        public int WeightCount { get; set; }
    }
}
