using FiguTrading.Models;
using FiguTrading.Services;
using FiguTrading.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FiguTrading.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        private string _email;
        private string _password;
        private readonly Api _apiServices = new Api();
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            Register = new Command(RegisterRedirect);
            GetSavedLogins();
        }

        public Command LoginCommand { get; }
        public Command Register { get; }
        public string Email { get => _email; set => SetProperty(ref _email, value); }
        public string Password { get => _password; set => SetProperty(ref _password, value); }
        
        internal void OnLoginClicked(object obj)
        {

            Dictionary<string, object> param = new Dictionary<string, object>() { { "Email", _email }, { "Password", _password } };
            Connect(param);

        }

        private async void RegisterRedirect()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        private async void GetSavedLogins()
        {
            try
            {
                _email = await SecureStorage.GetAsync("email");
                _password = await SecureStorage.GetAsync("pass");
                Dictionary<string, object> param = new Dictionary<string, object>() { { "Email", _email }, { "Password", _password } };
                Connect(param);

            }
            catch (Exception)
            {

            }
        }

        private async void Connect(Dictionary<string,object> param = null)
        {
            var connexion = await _apiServices.GetOneAsync<User>("getUserByMailAndPass", param);
            
            if(connexion == default(User))
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "L'email ou le mot de passe est incorrect", "Ok");
                SecureStorage.RemoveAll();
            }
            else
            {
                User.CurrentUser = connexion;
                try
                {
                    await SecureStorage.SetAsync("id", Convert.ToString(connexion.Id));
                    await SecureStorage.SetAsync("email", _email);
                    await SecureStorage.SetAsync("pass", _password);
                }
                catch (Exception)
                {

                }
                Constantes.Connected = true;
                await Shell.Current.GoToAsync($"//{nameof(AccueilPage)}");
                
            }
            
        }
    }
}
