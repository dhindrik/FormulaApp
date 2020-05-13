using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CodeHollow.FeedReader;
using FormulaApp.Services;
using Xamarin.Forms;
#if !WPF
using Xamarin.Essentials;
#endif

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

#if !WPF
            MainThread.BeginInvokeOnMainThread(() => {
#else
                System.Windows.Application.Current.Dispatcher.Invoke(() => { 
#endif             
                    PrimaryItems = new ObservableCollection<FeedItemViewModel>(f1Result.Take(10));
                    SecondaryItems = new ObservableCollection<FeedItemViewModel>(feResult.Take(10));

                PrimaryItem = PrimaryItems.First();
                SecondaryItem = SecondaryItems.First();

                    IsBusy = false;
                });
            });

        }

        private FeedItemViewModel primaryItem;
        public FeedItemViewModel PrimaryItem
        {
            get => primaryItem;
            set => Set(ref primaryItem, value);
        }

        private ObservableCollection<FeedItemViewModel> primaryItems;
        public ObservableCollection<FeedItemViewModel> PrimaryItems
        {
            get => primaryItems;
            set => Set(ref primaryItems, value);
        }

        private FeedItemViewModel secondaryItem;
        public FeedItemViewModel SecondaryItem
        {
            get => secondaryItem;
            set => Set(ref secondaryItem, value);
        }

        private ObservableCollection<FeedItemViewModel> secondaryItems;
        public ObservableCollection<FeedItemViewModel> SecondaryItems
        {
            get => secondaryItems;
            set => Set(ref secondaryItems, value);
        }

        public ICommand GoToWeb => new Command(async() =>
        {
#if !WPF
            await Browser.OpenAsync("https://motorsport.com");
#else
            System.Diagnostics.Process.Start("https://motorsport.com");
#endif
        });

        public ICommand Prev => new Command(() =>
        {
            var index = PrimaryItems.IndexOf(PrimaryItem);

            if(index > 0)
            {
                PrimaryItem = PrimaryItems[index - 1];
            }
        });

        public ICommand Next => new Command(() =>
        {
            var index = PrimaryItems.IndexOf(PrimaryItem);

            if (index + 1 < PrimaryItems.Count)
            {
                PrimaryItem = PrimaryItems[index + 1];
            }
        });
    }
}
