using LungTracking.BL.Models;
using LungTracking.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LungTracking.UI.Controllers
{
    public class BloodPressureController : Controller
    {
        private readonly ILogger<BloodPressureController> _logger;
        public BloodPressureController(ILogger<BloodPressureController> logger)
        {
            _logger = logger;
        }
        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://3928f303f9a2.ngrok.io/");
            client.BaseAddress = new Uri("https://localhost:44323/");
            return client;
        }

        // GET: BloodPressureController
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                User currentUser = HttpContext.Session.GetObject<User>("user");
                User currentUserById = HttpContext.Session.GetObject<User>("userId");
                currentUser.Id = currentUserById.Id;

                int role = currentUserById.Role;

                if (role == 0)
                {
                    HttpClient patientClient = InitializeClient();
                    HttpResponseMessage patientResponse;
                    string patientResult;
                    dynamic patientItems;

                    patientResponse = patientClient.GetAsync("Patient/" + currentUser.Id).Result;
                    patientResult = patientResponse.Content.ReadAsStringAsync().Result;
                    patientItems = (JArray)JsonConvert.DeserializeObject(patientResult);
                    List<Patient> patients = patientItems.ToObject<List<Patient>>();

                    HttpClient client = InitializeClient();
                    HttpResponseMessage response;
                    string bpResult;
                    dynamic bpItems;

                    response = client.GetAsync("BloodPressure/" + patients[0].Id).Result;
                    bpResult = response.Content.ReadAsStringAsync().Result;
                    bpItems = (JArray)JsonConvert.DeserializeObject(bpResult);
                    List<BloodPressure> bloodPressures = bpItems.ToObject<List<BloodPressure>>();
                    _logger.LogInformation("Loaded " + bloodPressures.Count + " blood pressure records");

                    return View(bloodPressures);
                }
                else if (role == 1)
                {
                    HttpClient providerClient = InitializeClient();
                    HttpResponseMessage providerResponse;
                    string providerResult;
                    dynamic providerItems;

                    providerResponse = providerClient.GetAsync("Provider/" + currentUser.Id).Result;
                    providerResult = providerResponse.Content.ReadAsStringAsync().Result;
                    providerItems = (JArray)JsonConvert.DeserializeObject(providerResult);
                    List<Patient> providers = providerItems.ToObject<List<Patient>>();

                    HttpClient patientProviderClient = InitializeClient();
                    HttpResponseMessage patientProviderResponse;
                    string patientProviderResult;
                    dynamic patientProviderItems;

                    patientProviderResponse = patientProviderClient.GetAsync("PatientProviderAccess/" + providers[0].Id).Result;
                    patientProviderResult = patientProviderResponse.Content.ReadAsStringAsync().Result;
                    patientProviderItems = (JArray)JsonConvert.DeserializeObject(patientProviderResult);
                    List<Patient> patients = patientProviderItems.ToObject<List<Patient>>();

                    HttpClient patientsClient = InitializeClient();
                    HttpResponseMessage patientsResponse;
                    string patientsResult;
                    dynamic patientsItems;

                    patientsResponse = patientsClient.GetAsync("BloodPressure/" + patients[0].Id).Result;
                    patientsResult = patientsResponse.Content.ReadAsStringAsync().Result;
                    patientsItems = (JArray)JsonConvert.DeserializeObject(patientsResult);
                    List<BloodPressure> bloodPressures = patientsItems.ToObject<List<BloodPressure>>();
                    _logger.LogInformation("Loaded " + bloodPressures.Count + " blood pressure records");
                }
                return View();
                
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        // GET: BloodPressureController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BloodPressureController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BloodPressureController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                if (Authenticate.IsAuthenticated(HttpContext))
                {
                    User currentUser = HttpContext.Session.GetObject<User>("user");
                    User currentUserById = HttpContext.Session.GetObject<User>("userId");
                    currentUser.Id = currentUserById.Id;

                    HttpClient patientClient = InitializeClient();
                    HttpResponseMessage patientResponse;
                    string result;
                    dynamic items;

                    patientResponse = patientClient.GetAsync("Patient/" + currentUser.Id).Result;
                    result = patientResponse.Content.ReadAsStringAsync().Result;
                    items = (JArray)JsonConvert.DeserializeObject(result);
                    List<Patient> patients = items.ToObject<List<Patient>>();

                    HttpClient client = InitializeClient();
                    BloodPressure bloodPressure = new BloodPressure
                    {
                        Id = Guid.NewGuid(),
                        BPsystolic = Convert.ToInt32(collection["txtBPSNumber"].ToString()),
                        BPdiastolic = Convert.ToInt32(collection["txtBPDNumber"].ToString()),
                        // BeginningEnd is passed through radio buttons on view
                        TimeOfDay = DateTime.Now,
                        PatientId = patients[0].Id
                    };
                    string serializedObject = JsonConvert.SerializeObject(bloodPressure);
                    var content = new StringContent(serializedObject);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PostAsync("BloodPressure/", content).Result;
                    _logger.LogInformation("Created blood pressure. BloodPressureId: " + bloodPressure.Id + " BPsystolic: " + bloodPressure.BPsystolic +
                                           " BPdiastolic:" + bloodPressure.BPdiastolic + " BeginningEnd: " + bloodPressure.BeginningEnd +
                                           " TimeOfDay: " + bloodPressure.TimeOfDay + " PatientId: " + bloodPressure.PatientId);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: BloodPressureController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BloodPressureController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BloodPressureController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BloodPressureController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
