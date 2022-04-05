using FiguTrading.Models;
using FiguTrading.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace FiguTrading.ViewModels
{
    class ProfilViewModel : BaseViewModel
    {
        private readonly Api _apiServices = new Api();
        private User _user;
        public ProfilViewModel()
        {
            GetUser();
        }

        public User LeUser { get => _user; set => SetProperty(ref _user, value); }
       

        public async void GetUser()
        {
            Dictionary<string, object> param = new Dictionary<string, object>() { { "Email", SecureStorage.GetAsync("email") }, { "Password", SecureStorage.GetAsync("pass") } };
            LeUser = await _apiServices.GetOneAsync<User>("getUserByMailAndPass",param);
        }
    }
}
