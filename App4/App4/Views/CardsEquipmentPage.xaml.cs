using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App4.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardsEquipmentPage : ContentPage
    {
        public CardsEquipmentPage()
        {
            InitializeComponent();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var cards = ((ListView)sender).SelectedItem as Cards;
            if (cards == null)
                return;
            await DisplayAlert("Card selected", cards.Name, "Ok");
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            var cards = ((MenuItem)sender).BindingContext as Cards;
            if (cards == null)
                return;
            await DisplayAlert("Favorite card selected", cards.Name, "Ok");
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var random = new Random();
            var random1 = (int)random.Next(0, 255);
            var random2 = (int)random.Next(0, 255);
            var random3 = (int)random.Next(0, 255);
            App.Current.Resources["WindowBackgroundColor"] = Color.FromRgb(random1, random2, random3);
        }

    }
}