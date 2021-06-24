using System;
using MvvmHelpers;
using MvvmHelpers.Commands;
using App4.Services;
using App4.Views;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;
using App4.Models;

namespace App4.ViewModels
{
    public class MyCardsViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Cards> Cards { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Cards> RemoveCommand { get; }
        public AsyncCommand<Cards> SelectedCommand { get; }

      public MyCardsViewModel()
        {
            Title = "My Cards";
            Cards = new ObservableRangeCollection<Cards>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Cards>(Remove);
            

            
        }

        async Task Add()
        {

            var name = await App.Current.MainPage.DisplayPromptAsync("Name","NameOfCofee");
            var group = await App.Current.MainPage.DisplayPromptAsync("Group","Sell/Trade");

            await CardsService.AddCards(name, group);
            await Refresh();

        }


        async Task Remove(Cards cards)
        {
            await CardsService.RemoveCards(cards.Id);
            await Refresh();
            
        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            Cards.Clear();

            var cards = await CardsService.GetCards();
            Cards.AddRange(cards);
            IsBusy = false;
        }
    }
}
