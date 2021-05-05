using LungTracking.Mobile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LungTracking.Mobile.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;
        List<BloodPressure> bloodPressures;
        List<BloodSugar> bloodSugars;
        List<FEV1> fev1s;
        List<PEF> pefs;
        List<Pulse> pulses;
        List<Temperature> temperatures;
        List<Weight> weights;

        public MockDataStore()
        {
            /*items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };*/

            Reload();
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://ee52cf2d1e58.ngrok.io/");
            //client.BaseAddress = new Uri("https://localhost:44364/");
            return client;
        }

        private async void Reload()
        {
            HttpClient client = InitializeClient();
            HttpResponseMessage response;
            string result;
            dynamic items;

            // Call the API Color controller/Get
            response = client.GetAsync("Weight").Result;
            // Get the JSON response
            result = response.Content.ReadAsStringAsync().Result;
            // Split the JSON response into a JArray
            items = (JArray)JsonConvert.DeserializeObject(result);
            // Convert the JArray to List<Color>
            weights = items.ToObject<List<Weight>>();

            items = new List<Item>();
            foreach (var w in weights)
            {
                items.Add(new Item { Id = w.Id.ToString(), Number = w.WeightNumberInPounds.ToString(), Time = w.TimeOfDay.ToString() });
            }

        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}