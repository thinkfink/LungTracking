using LungTracking.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LungTracking.Mobile.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string text;
        private string text1;
        private string text2;
        private string text3;
        private string text4;
        private string text5;
        private string text6;
        private string time;
        

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(time);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Text1
        {
            get => text1;
            set => SetProperty(ref text1, value);
        }

        public string Text2
        {
            get => text2;
            set => SetProperty(ref text2, value);
        }

        public string Text3
        {
            get => text3;
            set => SetProperty(ref text3, value);
        }

        public string Text4
        {
            get => text4;
            set => SetProperty(ref text4, value);
        }

        public string Text5
        {
            get => text5;
            set => SetProperty(ref text5, value);
        }

        public string Text6
        {
            get => text6;
            set => SetProperty(ref text6, value);
        }

        public string Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Number = Text,
                Time = Time
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
