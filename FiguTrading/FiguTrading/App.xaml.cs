using FiguTrading.Models;
using FiguTrading.Services;
using FiguTrading.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FiguTrading
{
    public partial class App : Application
    {
        private readonly Api _apiServices = new Api();
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

        }

        protected async override void OnStart()
        {
                try
                {
                   string _email = await SecureStorage.GetAsync("email");
                   string _password = await SecureStorage.GetAsync("pass");
                    Dictionary<string, object> param = new Dictionary<string, object>() { { "Email", _email }, { "Password", _password } };
                    var connexion = await _apiServices.GetOneAsync<User>("getUserByMailAndPass", param);

                    if (connexion == default(User))
                    {
                        
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
                    }

            }
                catch (Exception)
                {

                }

                

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
