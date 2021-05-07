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
        bool beginningEnd;
        public VitalTrackerPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
            txtTime.Text = DateTime.Now.ToString();
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://3928f303f9a2.ngrok.io/");
            //client.BaseAddress = new Uri("https://localhost:44364/");
            return client;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = InitializeClient();

                Weight weight = new Models.Weight 
                {
                    Id = Guid.NewGuid(),
                    WeightNumberInPounds = Convert.ToInt32(txtWeight.Text), 
                    TimeOfDay = DateTime.Parse(txtTime.Text),
                    PatientId = Guid.Parse("9563aae1-85d2-4724-a65f-8d7efefdb0b8")
                };

                PEF pef = new Models.PEF 
                {
                    Id = Guid.NewGuid(),
                    PEFNumber = Convert.ToDecimal(txtPEFNumber.Text), 
                    BeginningEnd = beginningEnd, 
                    TimeOfDay = DateTime.Parse(txtTime.Text),
                    PatientId = Guid.Parse("9563aae1-85d2-4724-a65f-8d7efefdb0b8")
                };

                FEV1 fev1 = new Models.FEV1
                {
                    Id = Guid.NewGuid(),
                    FEV1Number = Convert.ToDecimal(txtFEV1Number.Text),
                    BeginningEnd = beginningEnd,
                    TimeOfDay = DateTime.Parse(txtTime.Text),
                    PatientId = Guid.Parse("9563aae1-85d2-4724-a65f-8d7efefdb0b8"),
                    Alert = null
                };

                BloodPressure bloodPressure = new Models.BloodPressure
                {
                    Id = Guid.NewGuid(),
                    BPsystolic = Convert.ToInt32(txtBPSNumber.Text),
                    BPdiastolic = Convert.ToInt32(txtBPDNumber.Text),
                    BeginningEnd = beginningEnd,
                    TimeOfDay = DateTime.Parse(txtTime.Text),
                    PatientId = Guid.Parse("9563aae1-85d2-4724-a65f-8d7efefdb0b8")
                };

                Pulse pulse = new Models.Pulse 
                {
                    Id = Guid.NewGuid(),
                    PulseNumber = Convert.ToInt32(txtPulseNumber.Text), 
                    BeginningEnd = beginningEnd, 
                    TimeOfDay = DateTime.Parse(txtTime.Text),
                    PatientId = Guid.Parse("9563aae1-85d2-4724-a65f-8d7efefdb0b8")
                };

                Temperature temperature = new Models.Temperature { 
                    Id = Guid.NewGuid(),
                    TempNumber = Convert.ToDecimal(txtTempNumber.Text), 
                    BeginningEnd = beginningEnd, 
                    TimeOfDay = DateTime.Parse(txtTime.Text),
                    PatientId = Guid.Parse("9563aae1-85d2-4724-a65f-8d7efefdb0b8")
                };

                string serializedWeight = JsonConvert.SerializeObject(weight);
                var WeightContent = new StringContent(serializedWeight);
                WeightContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage weigtResponse = client.PostAsync("Weight/", WeightContent).Result;

                string serializedPEF = JsonConvert.SerializeObject(pef);
                var pefContent = new StringContent(serializedPEF);
                pefContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage pefResponse = client.PostAsync("PEF/", pefContent).Result;

                string serializedFEV1 = JsonConvert.SerializeObject(fev1);
                var fev1Content = new StringContent(serializedFEV1);
                fev1Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage fev1Response = client.PostAsync("FEV1/", fev1Content).Result;

                string serializedBloodPressure = JsonConvert.SerializeObject(bloodPressure);
                var bloodPressureContent = new StringContent(serializedBloodPressure);
                bloodPressureContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage bloodPressureResponse = client.PostAsync("BloodPressure/", bloodPressureContent).Result;

                string serializedPulse = JsonConvert.SerializeObject(pulse);
                var pulseContent = new StringContent(serializedPulse);
                pulseContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage pulseResponse = client.PostAsync("Pulse/", pulseContent).Result;

                string serializedTemperature = JsonConvert.SerializeObject(temperature);
                var temperatureContent = new StringContent(serializedTemperature);
                temperatureContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage temperatureResponse = client.PostAsync("Temperature/", temperatureContent).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnBeginning_Clicked(object sender, EventArgs e)
        {
            beginningEnd = true;
            btnBeginning.BackgroundColor = Color.FromHex("#ff0057");
            btnEnd.BackgroundColor = Color.FromHex("#ffbdd4");
        }

        private void txtTempNumber_Clicked(object sender, EventArgs e)
        {

        }

        private void btnEnd_Clicked(object sender, EventArgs e)
        {
            beginningEnd = false;
            btnEnd.BackgroundColor = Color.FromHex("#ff0057");
            btnBeginning.BackgroundColor = Color.FromHex("#ffbdd4");
        }
    }
}