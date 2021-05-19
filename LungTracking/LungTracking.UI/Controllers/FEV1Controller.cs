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
    public class FEV1Controller : Controller
    {
        private readonly ILogger<FEV1Controller> _logger;
        public FEV1Controller(ILogger<FEV1Controller> logger)
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

        // GET: FEV1Controller
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
                string fev1Result;
                dynamic fev1Items;

                response = client.GetAsync("FEV1/" + patients[0].Id).Result;
                fev1Result = response.Content.ReadAsStringAsync().Result;
                fev1Items = (JArray)JsonConvert.DeserializeObject(fev1Result);
                List<FEV1> fev1s = fev1Items.ToObject<List<FEV1>>();
                _logger.LogInformation("Loaded " + fev1s.Count + " FEV1 records");

                return View(fev1s);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        // GET: FEV1Controller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FEV1Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FEV1Controller/Create
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
                    FEV1 fev1 = new FEV1
                    {
                        Id = Guid.NewGuid(),
                        FEV1Number = Convert.ToDecimal(collection["txtFEV1Number"].ToString()),
                        TimeOfDay = DateTime.Now,
                        // BeginningEnd is passed through radio buttons on view
                        PatientId = patients[0].Id,
                        Alert = ""
                    };
                    string serializedObject = JsonConvert.SerializeObject(fev1);
                    var content = new StringContent(serializedObject);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PostAsync("FEV1/", content).Result;
                    _logger.LogInformation("Created FEV1. FEV1Id: " + fev1.Id + " FEV1Number: " + fev1.FEV1Number +
                                           " TimeOfDay:" + fev1.TimeOfDay + " BeginningEnd: " + fev1.BeginningEnd +
                                           " PatientId: " + fev1.PatientId + " Alert: " + fev1.Alert);

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

        // GET: FEV1Controller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FEV1Controller/Edit/5
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

        // GET: FEV1Controller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FEV1Controller/Delete/5
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
