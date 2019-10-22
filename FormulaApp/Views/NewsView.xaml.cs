using System;
using System.Collections.Generic;
using FormulaApp.ViewModels;
using Xamarin.Forms;

namespace FormulaApp.Views
{
    public partial class NewsView : ContentPage
    {
        public NewsView(int type)
        {
            InitializeComponent();

            BindingContext = new NewsViewModel(type);
        }
    }
}
