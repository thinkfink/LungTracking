using LungTracking.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            HttpClient client = InitializeClient();
            HttpResponseMessage response;
            string result;
            dynamic items;

            response = client.GetAsync("BloodPressure").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<BloodPressure> bloodPressures = items.ToObject<List<BloodPressure>>();

            return View(bloodPressures);
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
                HttpClient client = InitializeClient();
                BloodPressure bloodPressure = new BloodPressure
                {
                    Id = Guid.NewGuid(),
                    BPsystolic = Convert.ToInt32(collection["txtBPSNumber"].ToString()),
                    BPdiastolic = Convert.ToInt32(collection["txtBPDNumber"].ToString()),
                    TimeOfDay = DateTime.Now,
                    BeginningEnd = true,
                    PatientId = Guid.Parse("9563aae1-85d2-4724-a65f-8d7efefdb0b8")
                };
                string serializedObject = JsonConvert.SerializeObject(bloodPressure);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("BloodPressure/", content).Result;

                return RedirectToAction(nameof(Index), bloodPressure);
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
