using System;
using System.Windows.Input;
using CodeHollow.FeedReader;
using CoreTelephony;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FormulaApp.ViewModels
{
    public class FeedItemViewModel : ViewModelBase
    {
        public FeedItemViewModel(FeedItem item)
        {
            Item = item;
        }

        public FeedItem Item { get; set; }

        public ICommand Open => new Command(async() =>
        {
            await Browser.OpenAsync(Item.Link);
        });
    }
}
