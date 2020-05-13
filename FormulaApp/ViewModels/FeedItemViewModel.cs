using System;
using System.Windows.Input;
using CodeHollow.FeedReader;
using CodeHollow.FeedReader.Feeds;
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
#if !WPF
            await Xamarin.Essentials.Browser.OpenAsync(Item.Link);
#else
            System.Diagnostics.Process.Start(Item.Link);
#endif
        });

        public string Image
        {
            get
            {
                if(Item != null)
                {
                    var rssItems = Item.SpecificItem as Rss20FeedItem;

                    if(rssItems != null && rssItems.Enclosure != null)
                    {
                        return rssItems.Enclosure.Url;
                    }
                }

                return string.Empty;
            }
        }
    }
}
