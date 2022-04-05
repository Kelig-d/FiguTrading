using FiguTrading.Models;
using FiguTrading.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FiguTrading.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LaEncherePage : ContentPage
    {
        public LaEncherePage(Enchere enchere)
        {
            InitializeComponent();
            BindingContext = new LaEnchereViewModel(enchere);
        }
    }
}