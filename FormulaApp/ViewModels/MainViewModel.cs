using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CodeHollow.FeedReader;
using FormulaApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FormulaApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            IsBusy = true;

            Task.Run(async () =>
            {
                await NewsService.FetchData();

                var f1Result = NewsService.GetF1().Select(x => new FeedItemViewModel(x));
                var feResult = NewsService.GetFE().Select(x => new FeedItemViewModel(x));

                MainThread.BeginInvokeOnMainThread(() => {
                    PrimaryItems = new ObservableCollection<FeedItemViewModel>(f1Result.Take(10));
                    SecondaryItems = new ObservableCollection<FeedItemViewModel>(feResult.Take(10));

                    IsBusy = false;
                });
            });

        }

        private ObservableCollection<FeedItemViewModel> primaryItems;
        public ObservableCollection<FeedItemViewModel> PrimaryItems
        {
            get => primaryItems;
            set => Set(ref primaryItems, value);
        }

        private ObservableCollection<FeedItemViewModel> secondaryItems;
        public ObservableCollection<FeedItemViewModel> SecondaryItems
        {
            get => secondaryItems;
            set => Set(ref secondaryItems, value);
        }

        public ICommand GoToWeb => new Command(async() =>
        {
            await Browser.OpenAsync("https://motorsport.com");
        });
    }
}
