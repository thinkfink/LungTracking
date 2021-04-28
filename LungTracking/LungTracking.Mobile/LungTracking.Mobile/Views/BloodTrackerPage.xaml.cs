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
    public partial class BloodTrackerPage : ContentPage
    {
        public Item Item { get; set; }

        public BloodTrackerPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
            txtTime.Text = DateTime.Now.ToString();
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://19bc72669525.ngrok.io/");
            //client.BaseAddress = new Uri("https://localhost:44364/");
            return client;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            HttpClient client = InitializeClient();
            BloodSugar bloodSugar = new Models.BloodSugar { BloodSugarNumber = Convert.ToInt32(txtBloodSugarNumber.Text), TimeOfDay = DateTime.Now, UnitsOfInsulinGiven = Convert.ToInt32(txtUnitsOfInsulinGiven.Text), PatientId = Guid.NewGuid() };
            string serializedObject = JsonConvert.SerializeObject(bloodSugar);
            var content = new StringContent(serializedObject);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PutAsync("BloodSugar/" + bloodSugar.Id, content).Result;
        }
    }
}