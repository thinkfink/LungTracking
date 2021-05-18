using LungTracking.BL.Models;
using LungTracking.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
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
    public class WeightController : Controller
    {
        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://3928f303f9a2.ngrok.io/");
            client.BaseAddress = new Uri("https://localhost:44323/");
            return client;
        }

        // GET: WeightController
        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response;
                string result;
                dynamic items;

                response = client.GetAsync("Weight").Result;
                result = response.Content.ReadAsStringAsync().Result;
                items = (JArray)JsonConvert.DeserializeObject(result);
                List<Weight> weights = items.ToObject<List<Weight>>();

                return View(weights);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        // GET: WeightController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WeightController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WeightController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                if (Authenticate.IsAuthenticated(HttpContext))
                {
                    HttpClient client = InitializeClient();
                    Weight weight = new Weight
                    {
                        Id = Guid.NewGuid(),
                        WeightNumberInPounds = Convert.ToInt32(collection["txtWeight"].ToString()),
                        TimeOfDay = DateTime.Now,
                        PatientId = Guid.Parse("9563aae1-85d2-4724-a65f-8d7efefdb0b8")
                    };
                    string serializedObject = JsonConvert.SerializeObject(weight);
                    var content = new StringContent(serializedObject);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = client.PostAsync("Weight/", content).Result;

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

        // GET: WeightController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WeightController/Edit/5
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

        // GET: WeightController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WeightController/Delete/5
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
