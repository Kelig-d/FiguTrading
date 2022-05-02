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

        private void EnchereDetailsProchaine(object sender, EventArgs e)
        {
            viewModel.LaEnchere = viewModel.ProchaineEnchere;
            EnchereDetails(this, EventArgs.Empty);
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

            var enchereAEnvoyer = sender.GetType() == typeof(AccueilPage) ? viewModel.ProchaineEnchere : viewModel.LaEnchere;
            laEncherePage = new LaEncherePage(enchereAEnvoyer);
            await Navigation.PushAsync(laEncherePage);
        }
    }
}