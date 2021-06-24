using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App4.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace App4.ViewModels
{
    public class CardsEquipmentViewModel : ViewModelBase
    {

        public ObservableRangeCollection<Cards> Cards { get; set; }
        public ObservableRangeCollection<Grouping<string, Cards>> CardsGroups { get; set; }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Cards> FavouriteCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }
        public Command LoadMoreCommand { get; }
        public Command DelayLoadMoreCommand { get; }
        public Command ClearCommand { get; }


        public CardsEquipmentViewModel()
        {
            Title = "Cards Ready to go";

            Cards = new ObservableRangeCollection<Cards>();
            CardsGroups = new ObservableRangeCollection<Grouping<string, Cards>>();

            LoadMore();

            
            RefreshCommand = new AsyncCommand(Refresh);
            FavouriteCommand = new AsyncCommand<Cards>(Favourite);
            SelectedCommand = new AsyncCommand<object>(Selected);
            LoadMoreCommand = new Command(LoadMore);
            ClearCommand = new Command(Clear);
            DelayLoadMoreCommand = new Command(DelayLoadMore);
        }


        async Task Favourite(Cards cards)
        {
            if (cards == null)
                return;
            await Application.Current.MainPage.DisplayAlert("Favorite", cards.Name, "Ok");
        }


        Cards previouslySelected;
        Cards selectedCards;
        public Cards SelectedCards
        {
            get => selectedCards;
            set => SetProperty(ref selectedCards, value);
           
            
        }

        async Task Selected(object args)
        {
            var cards = args as Cards;
                if (cards == null)
                return;
            SelectedCards = null;

            await Application.Current.MainPage.DisplayAlert("Selected", cards.Name, "Ok");
        }

        void LoadMore()
        {


            var image = "https://i.pinimg.com/originals/87/b7/34/87b734e6fbbe58302ee9ea7305459d18.png";
            var image2 = "https://i.pinimg.com/originals/94/d9/c0/94d9c05fd0e310abc1bec3d13c9691ca.png";
            var image3 = "https://i.pinimg.com/originals/a1/e2/d5/a1e2d50ae5bc1f8ac3162127ce10bf4e.png";
            Cards.Add(new Cards { Group = "Trade", Name = "Yoongi", Image = "yoongi.png" });
            Cards.Add(new Cards { Group = "Trade", Name = "Wendy", Image = "wendy.png" });
            Cards.Add(new Cards { Group = "Sell", Name = "Yeji", Image = "yeji.png" });
            Cards.Add(new Cards { Group = "Sell", Name = "Jennie", Image = image2 });
            Cards.Add(new Cards { Group = "Trade", Name = "Yoongi", Image = image });
            Cards.Add(new Cards { Group = "Trade", Name = "Jihyo", Image = image3 });

            CardsGroups.Clear();

            CardsGroups.Add(new Grouping<string, Cards>("Sell", Cards.Where(c => c.Group == "Sell")));
            CardsGroups.Add(new Grouping<string, Cards>("Trade", Cards.Where(c => c.Group == "Trade")));

            

        }



       
        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            Cards.Clear();
            LoadMore();
            IsBusy = false;
        }
        void DelayLoadMore()
        {
            if (Cards.Count <= 10)
                return;

            LoadMore();
        }
        void Clear()
        {
            Cards.Clear();
            CardsGroups.Clear();
        }
    }
}
