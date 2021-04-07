using LungTracking.Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace LungTracking.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}