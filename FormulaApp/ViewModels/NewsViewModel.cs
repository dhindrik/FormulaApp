using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CodeHollow.FeedReader;
using FormulaApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FormulaApp.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        private readonly int type = -1;

        public NewsViewModel(int type)
        {
            this.type = type;

            ShowData();
        }

        public NewsViewModel()
        {
            ShowData();
        }

        private void ShowData()
        {
            IsBusy = true;

            List<FeedItem> result = null;

            if (type > -1)
            {              

                switch (type)
                {
                    case 0:
                        result = NewsService.GetF1();
                        break;
                    case 1:
                        result = NewsService.GetFE();
                        break;
                } 
            }
            else
            {
                result = NewsService.GetAll();
            }

            Items = new ObservableCollection<FeedItem>(result);

            IsBusy = false;
        }

        private ObservableCollection<FeedItem> items;
        public ObservableCollection<FeedItem> Items
        {
            get => items;
            set => Set(ref items, value);
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => Set(ref isRefreshing, value);
        }


        public ICommand Refresh => new Command(async() =>
        {
            IsRefreshing = true;

            await NewsService.FetchData();

            ShowData();

            IsRefreshing = false;
        });

        public ICommand ItemSelected => new Command<FeedItem>(async (item) =>
        {
            await Browser.OpenAsync(item.Link);
        });
    }
}
