using LungTracking.BL;
using LungTracking.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace LungTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        readonly IGeneratePdf _generatePdf;

        public ReportController(IGeneratePdf generatePdf)
        {
            _generatePdf = generatePdf;
        }

        [HttpGet]
        //[Route("Get")]
        public async Task<IActionResult> Get()
        {
            List<Appointment> appointments = (List<Appointment>)await AppointmentManager.Load();
            List<BloodPressure> bloodPressures = (List<BloodPressure>)await BloodPressureManager.Load();
            List<BloodSugar> bloodSugars = (List<BloodSugar>)await BloodSugarManager.Load();
            List<Caregiver> caregivers = (List<Caregiver>)await CaregiverManager.Load();
            List<FEV1> fev1s = (List<FEV1>)await FEV1Manager.Load();
            List<MedicationDetails> medDetails = (List<MedicationDetails>)await MedicationDetailsManager.Load();
            List<MedicationTime> medTimes = (List<MedicationTime>)await MedicationTimeManager.Load();
            List<MedicationTracking> medTrackings = (List<MedicationTracking>)await MedicationTrackingManager.Load();
            List<Patient> patients = (List<Patient>)await PatientManager.Load();
            List<PEF> pefs = (List<PEF>)await PEFManager.Load();
            List<Provider> providers = (List<Provider>)await ProviderManager.Load();
            List<Pulse> pulses = (List<Pulse>)await PulseManager.Load();
            List<SleepWake> sleepWakes = (List<SleepWake>)await SleepWakeManager.Load();
            List<Temperature> temps = (List<Temperature>)await TemperatureManager.Load();
            List<User> users = (List<User>)await UserManager.Load();
            List<Weight> weights = (List<Weight>)await WeightManager.Load();

            Report report = new Report();
            report.AppointmentCount = appointments.Count;
            report.BloodPressureCount = bloodPressures.Count;
            report.BloodSugarCount = bloodSugars.Count;
            report.CaregiverCount = caregivers.Count;
            report.FEV1Count = fev1s.Count;
            report.MedicationDetailsCount = medDetails.Count;
            report.MedicationTimeCount = medTimes.Count;
            report.MedicationTrackingCount = medTrackings.Count;
            report.PatientCount = patients.Count;
            report.PEFCount = pefs.Count;
            report.ProviderCount = providers.Count;
            report.PulseCount = pulses.Count;
            report.SleepWakeCount = sleepWakes.Count;
            report.TemperatureCount = temps.Count;
            report.UserCount = users.Count;
            report.WeightCount = weights.Count;

            return await _generatePdf.GetPdf("Views/Report/Report.cshtml", report);

            /* return report
            try
            {
                return Ok(await ReportManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            */
        }
    }
}
