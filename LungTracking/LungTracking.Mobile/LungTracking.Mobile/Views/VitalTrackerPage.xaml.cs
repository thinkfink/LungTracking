using LungTracking.Mobile.Models;
using LungTracking.Mobile.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LungTracking.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VitalTrackerPage : ContentPage
    {
        public Item Item { get; set; }
        public VitalTrackerPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44364/");
            return client;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            /*
            //PEF, FEV1, Pulse, Temp
            HttpClient client = InitializeClient();
            Weight weight = new Models.Weight { WeightNumberInPounds = Convert.ToInt32(txtNumber.Text), TimeOfDay = DateTime.Now };
            string serializedObject = JsonConvert.SerializeObject(weight);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PutAsync("Weight/" + weight.Id, content).Result;*/
        }
    }
}