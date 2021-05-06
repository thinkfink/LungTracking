using LungTracking.API.Controllers;
using LungTracking.BL.Models;
using LungTracking.UI.Models;
using Microsoft.AspNetCore.Http;
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
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44323/api/");
            return client;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user, string returnUrl)
        {
            try
            {
                if(StatusCodeResult.Equals(StatusCode(200), user))
                {
                    HttpContext.Session.SetObject("user", user);
                    HttpContext.Session.SetObject("username", "");
                    //HttpResponseMessage response;
                    //string result;
                    //dynamic items;

                    //HttpClient client = InitializeClient();

                    //response = client.GetAsync("User/" + user.Id).Result;
                    //result = response.Content.ReadAsStringAsync().Result;
                    //items = (JArray)JsonConvert.DeserializeObject(result);
                    //List<Patient> patients = items.ToObject<List<Patient>>();

                    if (user != null)
                        HttpContext.Session.SetObject("username", "Welcome " + user.Username);

                    if(TempData["returnurl"] != null)
                    {
                        return Redirect(TempData["returnurl"].ToString());
                    }
                    else
                    {
                        ViewBag.Message = "You are logged in.";
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }


        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
