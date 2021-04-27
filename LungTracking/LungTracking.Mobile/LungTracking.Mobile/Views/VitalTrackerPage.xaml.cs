﻿using LungTracking.Mobile.Models;
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
        enum BeginningEnd { Beginning = 0, End = 1 };
        public VitalTrackerPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
            txtTime.Text = DateTime.Now.ToString();
        }

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44364/");
            return client;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            HttpClient client = InitializeClient();

            PEF pef = new Models.PEF { PEFNumber = Convert.ToInt32(txtPEFNumber.Text), BeginningEnd = (Enum)Enum.Parse(typeof(BeginningEnd), txtBeginningEndNumber.Text), TimeOfDay = DateTime.Now };
            FEV1 fev1 = new Models.FEV1 { FEV1Number = Convert.ToInt32(txtFEV1Number.Text), BeginningEnd = (Enum)Enum.Parse(typeof(BeginningEnd), txtBeginningEndNumber.Text), TimeOfDay = DateTime.Now };
            BloodPressure bloodPressure = new Models.BloodPressure { BPsystolic = Convert.ToInt32(txtBPSNumber.Text), BPdiastolic = Convert.ToInt32(txtBPDNumber.Text), 
                                                                     BeginningEnd = (Enum)Enum.Parse(typeof(BeginningEnd), txtBeginningEndNumber.Text), TimeOfDay = DateTime.Now };
            Pulse pulse = new Models.Pulse { PulseNumber = Convert.ToInt32(txtPulseNumber.Text), BeginningEnd = (Enum)Enum.Parse(typeof(BeginningEnd), txtBeginningEndNumber.Text), TimeOfDay = DateTime.Now };
            Temperature temperature = new Models.Temperature { TempNumber = Convert.ToInt32(txtTempNumber.Text), BeginningEnd = (Enum)Enum.Parse(typeof(BeginningEnd), txtBeginningEndNumber.Text), TimeOfDay = DateTime.Now };

            /*Vitals vitals = new Models.Vitals { PEFNumber = Convert.ToInt32(txtPEFNumber.Text), FEV1Number = Convert.ToInt32(txtFEV1Number.Text), BPsystolic = Convert.ToInt32(txtBPSNumber.Text), 
                                                BPdiastolic = Convert.ToInt32(txtBPDNumber.Text), PulseNumber = Convert.ToInt32(txtPulseNumber.Text), TempNumber = Convert.ToInt32(txtTempNumber.Text),
                                                , BeginningEnd = (Enum)Enum.Parse(typeof(BeginningEnd), txtBeginningEndNumber.Text), TimeOfDay = DateTime.Now };*/

            string serializedPEF = JsonConvert.SerializeObject(pef);
            var pefContent = new StringContent(serializedPEF);
            pefContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage pefResponse = client.PutAsync("PEF/" + pef.Id, pefContent).Result;

            string serializedFEV1 = JsonConvert.SerializeObject(fev1);
            var fev1Content = new StringContent(serializedFEV1);
            pefContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage fev1Response = client.PutAsync("FEV1/" + fev1.Id, fev1Content).Result;

            string serializedBloodPressure = JsonConvert.SerializeObject(bloodPressure);
            var bloodPressureContent = new StringContent(serializedBloodPressure);
            bloodPressureContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage bloodPressureResponse = client.PutAsync("BloodPressure/" + bloodPressure.Id, bloodPressureContent).Result;

            string serializedPulse = JsonConvert.SerializeObject(pulse);
            var pulseContent = new StringContent(serializedPulse);
            pulseContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage pulseResponse = client.PutAsync("Pulse/" + pulse.Id, pulseContent).Result;

            string serializedTemperature = JsonConvert.SerializeObject(temperature);
            var temperatureContent = new StringContent(serializedTemperature);
            temperatureContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpResponseMessage temperatureResponse = client.PutAsync("Temperature/" + temperature.Id, temperatureContent).Result;
        }
    }
}