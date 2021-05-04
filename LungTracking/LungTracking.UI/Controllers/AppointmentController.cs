using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LungTracking.BL.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace LungTracking.UI.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: AppointmentController
        public ActionResult Index()
        {
            HttpResponseMessage response;
            string result;
            dynamic items;
            
            HttpClient client = InitializeClient();

            response = client.GetAsync("Appointment").Result;
            result = response.Content.ReadAsStringAsync().Result;
            items = (JArray)JsonConvert.DeserializeObject(result);
            List<Appointment> appointments = items.ToObject<List<Appointment>>();

            return View(appointments);
        }

        // GET: AppointmentController/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: AppointmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Appointment appointment)
        {
            try
            {
                HttpResponseMessage response;
                string result;
                dynamic items;

                HttpClient client = InitializeClient();

                response = client.GetAsync("Appointment/" + appointment.PatientId).Result;
                result = response.Content.ReadAsStringAsync().Result;
                items = (JArray)JsonConvert.DeserializeObject(result);
                List<Appointment> appointments = items.ToObject<List<Appointment>>();

                return View(nameof(Index), appointments);
            }
            catch
            {
                return View();
            }
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44323/api/");
            return client;
        }

        // GET: AppointmentController/Edit/5
        public ActionResult Edit(Guid id)
        {
            HttpResponseMessage response;
            string result;
            dynamic item;

            HttpClient client = InitializeClient();

            response = client.GetAsync("Appointment/" + id).Result;
            result = response.Content.ReadAsStringAsync().Result;
            item = JsonConvert.DeserializeObject(result);
            Appointment appointment = item.ToObject<Appointment>();

            return View(appointment);
        }

        // POST: AppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Appointment appointment)
        {
            try
            {
                HttpClient client = InitializeClient();
                string serializedObject = JsonConvert.SerializeObject(appointment);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PutAsync("Appointment/" + appointment.Id, content).Result;

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(appointment);
            }
        }

        // GET: AppointmentController/Delete/5
        public ActionResult Delete(Guid id)
        {
            HttpResponseMessage response;
            string result;
            dynamic item;

            HttpClient client = InitializeClient();

            response = client.GetAsync("Appointment/" + id).Result;
            result = response.Content.ReadAsStringAsync().Result;
            item = JsonConvert.DeserializeObject(result);
            Appointment appointment = item.ToObject<Appointment>();

            return View(appointment);
        }

        // POST: AppointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, Appointment appointment)
        {
            try
            {
                HttpClient client = InitializeClient();
                string serializedObject = JsonConvert.SerializeObject(appointment);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.DeleteAsync("Appointment/" + appointment.Id).Result;

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(appointment);
            }
        }
    }
}
