using System;
using System.Collections.Generic;
using FormulaApp.ViewModels;
using Xamarin.Forms;

namespace FormulaApp.Views
{
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();

            BindingContext = new MainViewModel();
        }
    }
}
