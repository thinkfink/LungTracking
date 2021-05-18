﻿using LungTracking.BL.Models;
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

                HttpClient client = InitializeClient();
                HttpResponseMessage response;
                string result;
                dynamic items;

                response = client.GetAsync("BloodPressure").Result;
                result = response.Content.ReadAsStringAsync().Result;
                items = (JArray)JsonConvert.DeserializeObject(result);
                List<BloodPressure> bloodPressures = items.ToObject<List<BloodPressure>>();
                _logger.LogInformation("Loaded " + bloodPressures.Count + " blood pressure records");

                return View(bloodPressures);
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

                    HttpClient client = InitializeClient();
                    BloodPressure bloodPressure = new BloodPressure
                    {
                        Id = Guid.NewGuid(),
                        BPsystolic = Convert.ToInt32(collection["txtBPSNumber"].ToString()),
                        BPdiastolic = Convert.ToInt32(collection["txtBPDNumber"].ToString()),
                        // BeginningEnd is passed through radio buttons on view
                        TimeOfDay = DateTime.Now,
                        PatientId = currentUser.Id
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
