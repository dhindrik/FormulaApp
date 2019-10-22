using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeHollow.FeedReader;

namespace FormulaApp.Services
{
    public class NewsService
    {
        private static List<FeedItem> Formula1 { get; set; }
        private static List<FeedItem> FormulaE { get; set; }


        public static async Task FetchData()
        {
            var f1Task = Fetch("https://www.motorsport.com/rss/f1/news/");
            var fETask = Fetch("https://www.motorsport.com/rss/formula-e/news/");
   

            await Task.WhenAll(f1Task, fETask);

            Formula1 = f1Task.Result;
            FormulaE = fETask.Result;
        }

        private static async Task<List<FeedItem>> Fetch(string url)
        {
            var feed = await FeedReader.ReadAsync(url);

            return feed.Items.ToList();
        }

        public static List<FeedItem> GetF1()
        {
            var items = new List<FeedItem>();

            if (Formula1 != null)
            {
                items.AddRange(Formula1);
            }

            return items.OrderByDescending(x => x.PublishingDate).ToList();
        }

        public static List<FeedItem> GetFE()
        {
            var items = new List<FeedItem>();

            if (FormulaE != null)
            {
                items.AddRange(FormulaE);
            }

            return items.OrderByDescending(x => x.PublishingDate).ToList();
        }

        public static List<FeedItem> GetAll()
        {
            var items = new List<FeedItem>();

            if(Formula1 != null)
            {
                items.AddRange(Formula1);
            }

            if (FormulaE != null)
            {
                items.AddRange(FormulaE);
            }


            return items.OrderByDescending(x => x.PublishingDate).ToList();
        }
    }
}
