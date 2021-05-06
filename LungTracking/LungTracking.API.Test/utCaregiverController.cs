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

namespace AJP.LungTracking.API.Test
{
    [TestClass]
    public class utCaregiverController
    {
        Caregiver caregiver = new Caregiver();
        Guid caregiverId = Guid.Parse("f8cd0f12-04a4-4bb7-afed-425e95018ecc");
        Guid userId = Guid.Parse("fce022bb-584d-42d6-97c0-b83ac1985807");

        [TestMethod]
        public void GetTest()
        {
            Task.Run(async () =>
            {
                var controller = new CaregiverController();
                var task = controller.Get();
                IEnumerable<Caregiver> appointments = (IEnumerable<Caregiver>)task;
                Assert.AreEqual(6, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void GetByUserIdTest()
        {
            Task.Run(async () =>
            {
                var controller = new CaregiverController();
                var task = controller.Get(userId);
                IEnumerable<Caregiver> appointments = (IEnumerable<Caregiver>)task;
                Assert.AreEqual(1, appointments.ToList().Count);
            });
        }

        [TestMethod]
        public void PostTest()
        {
            Task.Run(async () =>
            {
                caregiver.FirstName = "newfirstname";
                caregiver.LastName = "newlastname";
                caregiver.City = "Testopolis";
                caregiver.State = "WI";
                caregiver.PhoneNumber = "(555)555-2345";
                caregiver.UserId = Guid.Parse("798c8bdc-86ef-4935-ad2f-b8df92eeafc4");

                var controller = new CaregiverController();
                var task = controller.Post(caregiver);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Caregiver>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void PutTest()
        {
            caregiver.LastName = "updatedlastname";

            Task.Run(async () =>
            {
                var controller = new CaregiverController();
                var task = controller.Put(caregiver);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Caregiver>;
                Assert.IsNotNull(createdResult);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var controller = new CaregiverController();
                var task = controller.Delete(caregiver.Id);
                IHttpActionResult actionResult = (IHttpActionResult)task;
                var createdResult = actionResult as OkNegotiatedContentResult<Caregiver>;
                Assert.IsNotNull(createdResult);
            });
        }
    }
}