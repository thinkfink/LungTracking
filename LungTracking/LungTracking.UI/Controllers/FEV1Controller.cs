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
                HttpClient client = InitializeClient();
                HttpResponseMessage response;
                string result;
                dynamic items;

                response = client.GetAsync("FEV1").Result;
                result = response.Content.ReadAsStringAsync().Result;
                items = (JArray)JsonConvert.DeserializeObject(result);
                List<FEV1> fev1s = items.ToObject<List<FEV1>>();
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
                    HttpClient client = InitializeClient();
                    FEV1 fev1 = new FEV1
                    {
                        Id = Guid.NewGuid(),
                        FEV1Number = Convert.ToDecimal(collection["txtFEV1Number"].ToString()),
                        TimeOfDay = DateTime.Now,
                        // BeginningEnd is passed through radio buttons on view
                        PatientId = Guid.Parse("9563aae1-85d2-4724-a65f-8d7efefdb0b8"),
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
