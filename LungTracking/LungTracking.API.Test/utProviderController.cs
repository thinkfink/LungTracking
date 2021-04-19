using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using LungTracking.API.Controllers;
using LungTracking.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AJP.SurveyMaker.API.Test
{
    [TestClass]
    public class utProviderController
    {
        Provider provider = new Provider();
        Guid providerId = Guid.Parse("bf27f470-06f9-418d-bde9-18be4c4c11cd");
        Guid userId = Guid.Parse("a528aa8d-0038-4079-805c-4dcf18e3cb83");

        [TestMethod]
        public void GetTest()
        {
            Task.Run(async () =>
            {
                var controller = new ProviderController();
                var task = controller.Get();
                IEnumerable<Provider> appointments = (IEnumerable<Provider>)task;
                Assert.AreEqual(6, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByProviderIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new ProviderController();
                var task = controller.GetByProviderId(providerId);
                IEnumerable<Provider> appointments = (IEnumerable<Provider>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByUserIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new ProviderController();
                var task = controller.GetByUserId(userId);
                IEnumerable<Provider> appointments = (IEnumerable<Provider>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void PostTest()
        {
            Task.Run(async () =>
            {
                provider.FirstName = "newfirstname";
                provider.LastName = "newlastname";
                provider.City = "Testopolis";
                provider.State = "WI";
                provider.PhoneNumber = "(555)555-2345";
                provider.JobDescription = "Doctor of Testing";
                provider.ImagePath = "doctor.jpg";
                provider.UserId = Guid.Parse("798c8bdc-86ef-4935-ad2f-b8df92eeafc4");

                var controller = new ProviderController();
                var task = controller.Post(provider);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Provider>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void PutTest()
        {
            provider.LastName = "updatedlastname";

            Task.Run(async () =>
            {
                var controller = new ProviderController();
                var task = controller.Put(provider);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Provider>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var controller = new ProviderController();
                var task = controller.Delete(provider.Id);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Provider>;
                Assert.IsNotNull(createdResult);
            });
        }
    }
}