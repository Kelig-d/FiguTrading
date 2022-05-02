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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            OnAppearing();
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
            
        }

        protected async override void OnAppearing()
        {
            if (Constantes.Connected) await Shell.Current.GoToAsync($"//{nameof(ProfilPage)}");
        }
    }
}