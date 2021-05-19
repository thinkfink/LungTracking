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
    public class PEFController : Controller
    {
        private readonly ILogger<PEFController> _logger;
        public PEFController(ILogger<PEFController> logger)
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

        // GET: PEFController
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response;
                string result;
                dynamic items;

                response = client.GetAsync("PEF").Result;
                result = response.Content.ReadAsStringAsync().Result;
                items = (JArray)JsonConvert.DeserializeObject(result);
                List<PEF> pefs = items.ToObject<List<PEF>>();
                _logger.LogInformation("Loaded " + pefs.Count + " PEF records");

                return View(pefs);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        // GET: PEFController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PEFController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PEFController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
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
                PEF pef = new PEF
                {
                    Id = Guid.NewGuid(),
                    PEFNumber = Convert.ToDecimal(collection["txtPEFNumber"].ToString()),
                    TimeOfDay = DateTime.Now,
                    // BeginningEnd is passed through radio buttons on view
                    PatientId = patients[0].Id
                };
                string serializedObject = JsonConvert.SerializeObject(pef);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("PEF/", content).Result;
                _logger.LogInformation("Created PEF. PEFId: " + pef.Id + " PEFNumber: " + pef.PEFNumber +
                                       " TimeOfDay:" + pef.TimeOfDay + " BeginningEnd: " + pef.BeginningEnd +
                                       " PatientId: " + pef.PatientId);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: PEFController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PEFController/Edit/5
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

        // GET: PEFController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PEFController/Delete/5
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
