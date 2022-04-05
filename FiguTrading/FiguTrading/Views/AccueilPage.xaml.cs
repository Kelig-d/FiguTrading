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
    public partial class AccueilPage : ContentPage
    {
        private readonly AccueilVueModele viewModel;
        private static LaEncherePage laEncherePage;
        public AccueilPage()
        {
            
            InitializeComponent();
            BindingContext = viewModel = new AccueilVueModele();
        }

        private async void EnchereDetails(object sender, EventArgs e)
        {
            int index = -1;
            foreach (var element in Navigation.NavigationStack)
            {
                if (element == laEncherePage) break;
                index++;
            }
            try
            {
                if(index>-1) Navigation.RemovePage(Navigation.NavigationStack[index + 1]);
            }
            catch (Exception) { }
            laEncherePage = new LaEncherePage(viewModel.LaEnchere);
            await Navigation.PushAsync(laEncherePage);
        }
    }
}