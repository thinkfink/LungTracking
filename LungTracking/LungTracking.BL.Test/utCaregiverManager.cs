using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LungTracking.BL.Test
{
    [TestClass]
    public class utCaregiverManager
    {
        Guid caregiverId = Guid.Parse("f8cd0f12-04a4-4bb7-afed-425e95018ecc");
        Guid userId = Guid.Parse("fce022bb-584d-42d6-97c0-b83ac1985807");

        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                var task = await CaregiverManager.Load();
                IEnumerable<Models.Caregiver> caregivers = task;
                Assert.AreEqual(300, caregivers.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByCaregiverIdTest()
        {
            Task.Run(async () =>
            {
                var results = await CaregiverManager.LoadByCaregiverId(caregiverId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void LoadByUserIdTest()
        {
            Task.Run(async () =>
            {
                var results = await CaregiverManager.LoadByUserId(userId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                Caregiver caregiver = new Caregiver();
                caregiver.FirstName = "newfirstname";
                caregiver.LastName = "newlastname";
                caregiver.City = "Testopolis";
                caregiver.State = "WI";
                caregiver.PhoneNumber = "(555)555-2345";
                caregiver.UserId = Guid.Parse("798c8bdc-86ef-4935-ad2f-b8df92eeafc4");

                int results = await CaregiverManager.Insert(caregiver);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = CaregiverManager.Load();
                IEnumerable<Models.Caregiver> caregivers = task.Result;
                Models.Caregiver caregiver = caregivers.FirstOrDefault(p => p.FirstName == "newfirstname");
                caregiver.LastName = "updatedlastname";
                var results = CaregiverManager.Update(caregiver);
                Assert.IsTrue(results.Result > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = CaregiverManager.Load();
                IEnumerable<Models.Caregiver> caregivers = task.Result;
                task.Wait();
                Models.Caregiver caregiver = caregivers.FirstOrDefault(p => p.FirstName == "newfirstname");
                var results = CaregiverManager.Delete(caregiver.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}