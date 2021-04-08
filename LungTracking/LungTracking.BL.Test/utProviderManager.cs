using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LungTracking.BL;
using LungTracking.BL.Models;
using System.Collections.Generic;
using System.Linq;

namespace LungTracking.BL.Test
{
    [TestClass]
    public class utProviderManager
    {
        Guid providerId = Guid.Parse("bf27f470-06f9-418d-bde9-18be4c4c11cd");
        Guid userId = Guid.Parse("a528aa8d-0038-4079-805c-4dcf18e3cb83");

        [TestMethod]
        public void LoadTest()
        {
            List<Provider> providers = new List<Provider>();
            providers = ProviderManager.Load();
            int expected = 50;
            Assert.AreEqual(expected, providers.Count);
        }

        [TestMethod]
        public void LoadByProviderIdTest()
        {
            Provider provider = new Provider();
            provider = ProviderManager.LoadByProviderId(providerId);
            Assert.IsNotNull(provider);
        }

        [TestMethod]
        public void LoadByUserIdTest()
        {
            Provider provider = new Provider();
            provider = ProviderManager.LoadByUserId(userId);
            Assert.IsNotNull(provider);
        }

        [TestMethod]
        public void InsertTest()
        {
            Provider provider = new Provider();
            provider.FirstName = "newfirstname";
            provider.LastName = "newlastname";
            provider.City = "Testopolis";
            provider.State = "WI";
            provider.PhoneNumber = "(555)555-2345";
            provider.JobDescription = "Doctor of Testing";
            provider.ImagePath = "doctor.jpg";
            provider.UserId = Guid.Parse("798c8bdc-86ef-4935-ad2f-b8df92eeafc4");

            int result = ProviderManager.Insert(provider);
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            List<Provider> providers = ProviderManager.Load();

            Provider provider = providers.Where(p => p.FirstName == "newfirstname").FirstOrDefault();

            provider.LastName = "updatedlastname";

            ProviderManager.Update(provider);

            Provider updatedprovider = providers.FirstOrDefault(p => p.FirstName == provider.FirstName);

            Assert.AreEqual(provider.LastName, updatedprovider.LastName);
        }

        [TestMethod]
        public void DeleteTest()
        {
            List<Provider> providers = ProviderManager.Load();
            Provider provider = providers.Where(p => p.FirstName == "newfirstname").FirstOrDefault();

            int results = ProviderManager.Delete(provider.Id);
            Assert.IsTrue(results > 0);
        }
    }
}
