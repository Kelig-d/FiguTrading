using FiguTrading.Models;
using FiguTrading.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FiguTrading.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LesEncheresPage : ContentPage
    {
        public LesEncheresPage()
        {
            InitializeComponent();

            BindingContext = new LesEncheresViewModel(Navigation);
        }


    }
}