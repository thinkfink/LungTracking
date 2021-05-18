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
    public class BloodSugarController : Controller
    {
        private readonly ILogger<BloodSugarController> _logger;
        public BloodSugarController(ILogger<BloodSugarController> logger)
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

        // GET: BloodSugarController
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response;
                string result;
                dynamic items;

                response = client.GetAsync("BloodSugar").Result;
                result = response.Content.ReadAsStringAsync().Result;
                items = (JArray)JsonConvert.DeserializeObject(result);
                List<BloodSugar> bloodSugars = items.ToObject<List<BloodSugar>>();
                _logger.LogInformation("Loaded " + bloodSugars.Count + " blood sugar records");

                return View(bloodSugars);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        // GET: BloodSugarController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BloodSugarController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BloodSugarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                if (Authenticate.IsAuthenticated(HttpContext))
                {
                    HttpClient client = InitializeClient();
                    BloodSugar bloodSugar = new BloodSugar
                    {
                        Id = Guid.NewGuid(),
                        BloodSugarNumber = Convert.ToInt32(collection["txtBloodSugarNumber"].ToString()),
                        TimeOfDay = DateTime.Now,
                        UnitsOfInsulinGiven = Convert.ToInt32(collection["txtUnitsOfInsulinGiven"].ToString()),
                        TypeOfInsulinGiven = collection["txtTypeOfInsulinGiven"].ToString(),
                        Notes = collection["txtNotes"].ToString(),
                        PatientId = Guid.Parse("9563aae1-85d2-4724-a65f-8d7efefdb0b8")
                    };
                    string serializedObject = JsonConvert.SerializeObject(bloodSugar);
                    var content = new StringContent(serializedObject);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PostAsync("BloodSugar/", content).Result;
                    _logger.LogInformation("Created blood sugar. BloodSugarId: " + bloodSugar.Id + " BloodSugarNumber: " + bloodSugar.BloodSugarNumber +
                                           " TimeOfDay:" + bloodSugar.TimeOfDay + " UnitsOfInsulinGiven: " + bloodSugar.UnitsOfInsulinGiven +
                                           " TypeOfInsulinGiven: " + bloodSugar.TypeOfInsulinGiven + " Notes: " + bloodSugar.Notes + " PatientId: " + bloodSugar.PatientId);

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

        // GET: BloodSugarController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BloodSugarController/Edit/5
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

        // GET: BloodSugarController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BloodSugarController/Delete/5
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
