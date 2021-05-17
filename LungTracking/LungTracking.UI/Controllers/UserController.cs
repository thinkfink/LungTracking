using LungTracking.API.Controllers;
using LungTracking.BL;
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
            client.BaseAddress = new Uri("https://localhost:44323/");
            return client;
        }

        public IActionResult Seed()
        {
            UserManager.Seed();
            return View();
        }

        // GET: UserController/Create
        public IActionResult Login(string returnUrl)
        {
            TempData["returnurl"] = returnUrl;
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user, string returnUrl, IFormCollection collection)
        {
            try
            {
                HttpClient client = InitializeClient();
                user = new User
                {
                    Username = collection["txtUsername"].ToString(),
                    Password = collection["txtPassword"].ToString(),
                    LastLogin = DateTime.Now
                };
                string serializedObject = JsonConvert.SerializeObject(user);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("Login/", content).Result;

                if (response.ReasonPhrase == "OK")
                //if(UserManager.Login(user))
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

        public IActionResult Logout()
        {
            HttpContext.Session.SetObject("user", null);
            HttpContext.Session.SetObject("username", "");
            return View();
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
        public ActionResult Create(User user)
        {
            try
            {
                UserManager.Insert(user);
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
