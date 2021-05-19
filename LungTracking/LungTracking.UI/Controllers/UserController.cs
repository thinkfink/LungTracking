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
                
                // running the login method
                string serializedObject = JsonConvert.SerializeObject(user);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("Login/", content).Result;

                if (response.ReasonPhrase == "OK")
                {
                    HttpContext.Session.SetObject("user", user);
                    HttpContext.Session.SetObject("username", "");

                    // grab the userId upon login
                    HttpClient userClient = InitializeClient();
                    HttpResponseMessage userResponse;
                    string result;
                    dynamic item;

                    userResponse = userClient.GetAsync("UserId/" + user.Username).Result;
                    result = userResponse.Content.ReadAsStringAsync().Result;
                    item = JsonConvert.DeserializeObject(result);
                    User loadedUser = item.ToObject<User>();

                    HttpContext.Session.SetObject("userId", loadedUser);

                    int role = loadedUser.Role;
                    
                    // check to see if the userId has a patientId, caregiverId, or providerId
                    HttpClient patientClient = InitializeClient();
                    HttpResponseMessage patientResponse;
                    string patientResult;
                    dynamic patientItem;
                    patientResponse = patientClient.GetAsync("Patient/" + loadedUser.Id).Result;

                    HttpClient caregiverClient = InitializeClient();
                    HttpResponseMessage caregiverResponse;
                    string caregiverResult;
                    dynamic caregiverItem;
                    caregiverResponse = caregiverClient.GetAsync("Caregiver/" + loadedUser.Id).Result;

                    HttpClient providerClient = InitializeClient();
                    HttpResponseMessage providerResponse;
                    string providerResult;
                    dynamic providerItem;
                    providerResponse = providerClient.GetAsync("Provider/" + loadedUser.Id).Result;
                    

                    if (role == 0)
                    {
                        patientResult = patientResponse.Content.ReadAsStringAsync().Result;
                        patientItem = JsonConvert.DeserializeObject(patientResult);
                        Patient loadedPatient = patientItem[0].ToObject<Patient>();

                        HttpContext.Session.SetObject("patientId", loadedPatient.Id);
                    }
                    else if(role == 1)
                    {
                        providerResult = providerResponse.Content.ReadAsStringAsync().Result;
                        providerItem = JsonConvert.DeserializeObject(providerResult);
                        Provider loadedProvider = providerItem[0].ToObject<Provider>();

                        HttpContext.Session.SetObject("providerId", loadedProvider.Id);
                    }
                    else if(role == 2)
                    {
                        caregiverResult = caregiverResponse.Content.ReadAsStringAsync().Result;
                        caregiverItem = JsonConvert.DeserializeObject(caregiverResult);
                        Caregiver loadedCaregiver = caregiverItem[0].ToObject<Caregiver>();

                        HttpContext.Session.SetObject("caregiverId", loadedCaregiver.Id);
                    }

                    /*
                    if (patientResponse.ReasonPhrase == "OK")
                    {
                        patientResult = patientResponse.Content.ReadAsStringAsync().Result;
                        patientItem = JsonConvert.DeserializeObject(patientResult);
                        Patient loadedPatient = patientItem.ToObject<Patient>();

                        HttpContext.Session.SetObject("patientId", loadedPatient.Id);
                    }
                    else
                    {
                        HttpClient caregiverClient = InitializeClient();
                        HttpResponseMessage caregiverResponse;
                        string caregiverResult;
                        dynamic caregiverItem;

                        caregiverResponse = caregiverClient.GetAsync("Caregiver/" + loadedUser.Id).Result;

                        if (caregiverResponse.ReasonPhrase == "OK")
                        {
                            caregiverResult = caregiverResponse.Content.ReadAsStringAsync().Result;
                            caregiverItem = JsonConvert.DeserializeObject(caregiverResult);
                            Caregiver loadedCaregiver = caregiverItem.ToObject<Caregiver>();

                            HttpContext.Session.SetObject("caregiverId", loadedCaregiver.Id);
                        }
                        else
                        {
                            HttpClient providerClient = InitializeClient();
                            HttpResponseMessage providerResponse;
                            string providerResult;
                            dynamic providerItem;

                            providerResponse = providerClient.GetAsync("Provider/" + loadedUser.Id).Result;

                            if (providerResponse.ReasonPhrase == "OK")
                            {
                                providerResult = providerResponse.Content.ReadAsStringAsync().Result;
                                providerItem = JsonConvert.DeserializeObject(providerResult);
                                Provider loadedProvider = providerItem.ToObject<Provider>();

                                HttpContext.Session.SetObject("caregiverId", loadedProvider.Id);
                            }
                        }
                    }
                    */


                    if (user != null)
                        HttpContext.Session.SetObject("username", "Welcome " + user.Username);

                    if(TempData["returnurl"] != null)
                    {
                        return Redirect(TempData["returnurl"].ToString());
                    }
                    else
                    {
                        ViewBag.Message = "You are logged in, " + user.Username + "!";
                        _logger.LogInformation(user.Username + " logged in");
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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                HttpClient client = InitializeClient();
                User user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = collection["txtUsername"].ToString(),
                    Password = collection["txtPassword"].ToString(),
                    Role = Convert.ToInt32(collection["txtRole"].ToString()),
                    Email = collection["txtEmail"].ToString(),
                    Created = DateTime.Now,
                    LastLogin = DateTime.Now
                };
                string serializedObject = JsonConvert.SerializeObject(user);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("User/", content).Result;
                _logger.LogInformation("Created account: " + user.Username);

                return RedirectToAction("Index", "Home");
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
