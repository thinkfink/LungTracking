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
    public class PEFController : Controller
    {
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
            HttpClient client = InitializeClient();
            HttpResponseMessage response;
            string result;
            dynamic items;

            response = client.GetAsync("PEF").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<PEF> pefs = items.ToObject<List<PEF>>();

            return View(pefs);
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
                HttpClient client = InitializeClient();
                PEF pef = new PEF
                {
                    Id = Guid.NewGuid(),
                    PEFNumber = Convert.ToDecimal(collection["txtPEFNumber"].ToString()),
                    TimeOfDay = DateTime.Now,
                    // BeginningEnd is passed through radio buttons on view
                    PatientId = Guid.Parse("9563aae1-85d2-4724-a65f-8d7efefdb0b8")
                };
                string serializedObject = JsonConvert.SerializeObject(pef);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("PEF/", content).Result;

                return RedirectToAction(nameof(Index), pef);
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
