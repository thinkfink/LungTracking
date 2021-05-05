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
            client.BaseAddress = new Uri("https://ee52cf2d1e58.ngrok.io/");
            //client.BaseAddress = new Uri("https://localhost:44364/");
            return client;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = InitializeClient();
                BloodSugar bloodSugar = new Models.BloodSugar
                {
                    Id = Guid.NewGuid(),
                    BloodSugarNumber = Convert.ToInt32(txtBloodSugarNumber.Text),
                    TimeOfDay = DateTime.Now,
                    UnitsOfInsulinGiven = Convert.ToInt32(txtUnitsOfInsulinGiven.Text),
                    TypeOfInsulinGiven = txtTypeOfInsulinGiven.Text,
                    Notes = txtNotes.Text,
                    PatientId = Guid.Parse("9563aae1-85d2-4724-a65f-8d7efefdb0b8")
                };
                string serializedObject = JsonConvert.SerializeObject(bloodSugar);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync("BloodSugar/", content).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}