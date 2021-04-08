using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

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
            List<Caregiver> caregivers = new List<Caregiver>();
            caregivers = CaregiverManager.Load();
            int expected = 46;
            Assert.AreEqual(expected, caregivers.Count);
        }

        [TestMethod]
        public void LoadByCaregiverIdTest()
        {
            Caregiver caregiver = new Caregiver();
            caregiver = CaregiverManager.LoadByCaregiverId(caregiverId);
            Assert.IsNotNull(caregiver);
        }

        [TestMethod]
        public void LoadByUserIdTest()
        {
            Caregiver caregiver = new Caregiver();
            caregiver = CaregiverManager.LoadByUserId(userId);
            Assert.IsNotNull(caregiver);
        }

        [TestMethod]
        public void InsertTest()
        {
            Caregiver caregiver = new Caregiver();
            caregiver.FirstName = "newfirstname";
            caregiver.LastName = "newlastname";
            caregiver.City = "Testopolis";
            caregiver.State = "WI";
            caregiver.PhoneNumber = "(555)555-2345";
            caregiver.UserId = Guid.Parse("798c8bdc-86ef-4935-ad2f-b8df92eeafc4");

            int result = CaregiverManager.Insert(caregiver);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            List<Caregiver> caregivers = CaregiverManager.Load();

            Caregiver caregiver = caregivers.Where(p => p.FirstName == "newfirstname").FirstOrDefault();

            caregiver.LastName = "updatedlastname";

            CaregiverManager.Update(caregiver);

            Caregiver updatedcaregiver = caregivers.FirstOrDefault(p => p.FirstName == caregiver.FirstName);

            Assert.AreEqual(caregiver.LastName, updatedcaregiver.LastName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<Caregiver> caregivers = CaregiverManager.Load();
            Caregiver caregiver = caregivers.Where(p => p.FirstName == "newfirstname").FirstOrDefault();

            int results = CaregiverManager.Delete(caregiver.Id);
            Assert.IsTrue(results > 0);
        }
    }
}
