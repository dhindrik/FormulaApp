using System;
using System.Collections.Generic;
using FormulaApp.ViewModels;
using Xamarin.Forms;

namespace FormulaApp.Views
{
    public partial class AllNews : ContentPage
    {
        public AllNews()
        {
            InitializeComponent();

            BindingContext = new NewsViewModel();
        }
    }
}
