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
    public class utProviderManager
    {
        Guid providerId = Guid.Parse("bf27f470-06f9-418d-bde9-18be4c4c11cd");
        Guid userId = Guid.Parse("a528aa8d-0038-4079-805c-4dcf18e3cb83");

        [TestMethod]
        public void LoadTest()
        {
            Task.Run(async () =>
            {
                var task = await ProviderManager.Load();
                IEnumerable<Models.Provider> providers = task;
                Assert.AreEqual(300, providers.ToList().Count);
            });
        }

        [TestMethod]
        public void LoadByProviderIdTest()
        {
            Task.Run(async () =>
            {
                var results = await ProviderManager.LoadByProviderId(providerId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void LoadByUserIdTest()
        {
            Task.Run(async () =>
            {
                var results = await ProviderManager.LoadByUserId(userId);
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
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

                int results = await ProviderManager.Insert(provider);
                Assert.IsTrue(results > 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {
            Task.Run(async () =>
            {
                var task = ProviderManager.Load();
                IEnumerable<Models.Provider> providers = task.Result;
                Models.Provider provider = providers.FirstOrDefault(p => p.FirstName == "newfirstname");
                provider.LastName = "updatedlastname";
                var results = ProviderManager.Update(provider);
                Assert.IsTrue(results.Result > 0);
            });
        }

        [TestMethod]
        public void DeleteTest()
        {
            Task.Run(async () =>
            {
                var task = ProviderManager.Load();
                IEnumerable<Models.Provider> providers = task.Result;
                task.Wait();
                Models.Provider provider = providers.FirstOrDefault(p => p.FirstName == "newfirstname");
                var results = ProviderManager.Delete(provider.Id, true);
                Assert.IsTrue(results.Result > 0);
            });
        }
    }
}