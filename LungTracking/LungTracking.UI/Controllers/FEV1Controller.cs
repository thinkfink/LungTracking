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
    public class FEV1Controller : Controller
    {
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
            HttpClient client = InitializeClient();
            HttpResponseMessage response;
            string result;
            dynamic items;

            response = client.GetAsync("FEV1").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<FEV1> fev1s = items.ToObject<List<FEV1>>();

            return View(fev1s);
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
                HttpClient client = InitializeClient();
                FEV1 fev1 = new FEV1
                {
                    Id = Guid.NewGuid(),
                    FEV1Number = Convert.ToDecimal(collection["txtFEV1Number"].ToString()),
                    TimeOfDay = DateTime.Now,
                    BeginningEnd = true,
                    PatientId = Guid.Parse("9563aae1-85d2-4724-a65f-8d7efefdb0b8"),
                    Alert = ""
                };
                string serializedObject = JsonConvert.SerializeObject(fev1);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("FEV1/", content).Result;

                return RedirectToAction(nameof(Index), fev1);
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
