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

                var f1Result = NewsService.GetF1();
                var feResult = NewsService.GetFE();

                MainThread.BeginInvokeOnMainThread(() => {
                    PrimaryItems = new ObservableCollection<FeedItem>(f1Result.Take(10));
                    SecondaryItems = new ObservableCollection<FeedItem>(feResult.Take(10));

                    IsBusy = false;
                });
            });

        }

        private ObservableCollection<FeedItem> primaryItems;
        public ObservableCollection<FeedItem> PrimaryItems
        {
            get => primaryItems;
            set => Set(ref primaryItems, value);
        }

        private ObservableCollection<FeedItem> secondaryItems;
        public ObservableCollection<FeedItem> SecondaryItems
        {
            get => secondaryItems;
            set => Set(ref secondaryItems, value);
        }

        
    }
}
