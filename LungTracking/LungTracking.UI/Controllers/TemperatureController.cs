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
    public class TemperatureController : Controller
    {
        private readonly ILogger<TemperatureController> _logger;
        public TemperatureController(ILogger<TemperatureController> logger)
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

        // GET: TemperatureController
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                User currentUser = HttpContext.Session.GetObject<User>("user");
                User currentUserById = HttpContext.Session.GetObject<User>("userId");
                currentUser.Id = currentUserById.Id;

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
                string temperatureResult;
                dynamic temperatureItems;

                response = client.GetAsync("Temperature/" + patients[0].Id).Result;
                temperatureResult = response.Content.ReadAsStringAsync().Result;
                temperatureItems = (JArray)JsonConvert.DeserializeObject(temperatureResult);
                List<Temperature> temperatures = temperatureItems.ToObject<List<Temperature>>();
                _logger.LogInformation("Loaded " + temperatures.Count + " temperature records");

                return View(temperatures);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        // GET: TemperatureController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TemperatureController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TemperatureController/Create
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
                    Temperature temperature = new Temperature
                    {
                        Id = Guid.NewGuid(),
                        TempNumber = Convert.ToDecimal(collection["txtTempNumber"].ToString()),
                        TimeOfDay = DateTime.Now,
                        // BeginningEnd is passed through radio buttons on view
                        PatientId = patients[0].Id,
                        Alert = ""
                    };
                    string serializedObject = JsonConvert.SerializeObject(temperature);
                    var content = new StringContent(serializedObject);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PostAsync("Temperature/", content).Result;
                    _logger.LogInformation("Created temperature. TemperatureId: " + temperature.Id + " PEFNumber: " + temperature.TempNumber +
                                           " TimeOfDay:" + temperature.TimeOfDay + " BeginningEnd: " + temperature.BeginningEnd +
                                           " PatientId: " + temperature.PatientId + " Alert: " + temperature.Alert);

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

        // GET: TemperatureController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TemperatureController/Edit/5
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

        // GET: TemperatureController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TemperatureController/Delete/5
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
