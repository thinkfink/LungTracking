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
    public class PulseController : Controller
    {
        private readonly ILogger<PulseController> _logger;
        public PulseController(ILogger<PulseController> logger)
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

        // GET: PulseController
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response;
                string result;
                dynamic items;

                response = client.GetAsync("Pulse").Result;
                result = response.Content.ReadAsStringAsync().Result;
                items = (JArray)JsonConvert.DeserializeObject(result);
                List<Pulse> pulses = items.ToObject<List<Pulse>>();
                _logger.LogInformation("Loaded " + pulses.Count + " pulse records");

                return View(pulses);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        // GET: PulseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PulseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PulseController/Create
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
                    Pulse pulse = new Pulse
                    {
                        Id = Guid.NewGuid(),
                        PulseNumber = Convert.ToInt32(collection["txtPulseNumber"].ToString()),
                        TimeOfDay = DateTime.Now,
                        // BeginningEnd is passed through radio buttons on view
                        PatientId = patients[0].Id
                    };
                    string serializedObject = JsonConvert.SerializeObject(pulse);
                    var content = new StringContent(serializedObject);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PostAsync("Pulse/", content).Result;
                    _logger.LogInformation("Created pulse. PulseId: " + pulse.Id + " PEFNumber: " + pulse.PulseNumber +
                                           " TimeOfDay:" + pulse.TimeOfDay + " BeginningEnd: " + pulse.BeginningEnd +
                                           " PatientId: " + pulse.PatientId);

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

        // GET: PulseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PulseController/Edit/5
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

        // GET: PulseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PulseController/Delete/5
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
