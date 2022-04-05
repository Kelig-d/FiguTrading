using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using FiguTrading.Interfaces;
using System.IO;
using System.ComponentModel;
using FiguTrading.Models;
using FiguTrading.Services;
using System.Data;

namespace FiguTrading.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private ImageSource _photo;
        private User _user;
        private string _confirmPassword;
        private readonly Api _apiServices = new Api();
        string Photo64 = Constantes.BaseImageBase64;
        public RegisterViewModel()
        {
            PhotoPicker = new Command(PickPhoto);
            _user = new User();
           _photo = ImageSource.FromFile("app_icon.png");
            Register = new Command(Registration);
            

        }

        public Command PhotoPicker { get; }

        public Command Register { get; }
        public ImageSource Photo { 
            get => _photo; 
            set => SetProperty(ref _photo, value);
        }

        public string ConfirmPassword { get => _confirmPassword; set => SetProperty(ref _confirmPassword, value); }

        public User LeUser
        {
            get => _user;
            set => SetProperty(ref _user ,value);
        }

        private async void PickPhoto(object sender)
        {
            
            //(sender as ImageButton).IsEnabled = false;

            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    stream.CopyTo(memory);
                    byte[] data = memory.ToArray();
                    Photo = ImageSource.FromStream(() => new MemoryStream(data));
                    this.Photo64 = Convert.ToBase64String(data);
                    
                }




            }

    //(sender as ImageButton).IsEnabled = true;

        }

        private async void Registration(object sender)
        {
            if(_user.Password == _confirmPassword)
            {
                try
                {
                    _user.Photo = Photo64;
                    var response = await _apiServices.PostAsync<User>("postUser", _user);
                    switch (response)
                    {
                        case -1:
                            throw new DuplicateNameException();
                        default:
                            break;
                    };
                    User.CurrentUser = LeUser;
                    await Application.Current.MainPage.DisplayAlert("Information", "Le compte a été créé avec succès", "Ok");
                    await SecureStorage.SetAsync("id", Convert.ToString(_user.Id));
                    await SecureStorage.SetAsync("email",_user.Email);
                    await SecureStorage.SetAsync("pass", _user.Password);
                    await Application.Current.MainPage.Navigation.PopAsync();

                }
                catch (DuplicateNameException)
                {
                    await Application.Current.MainPage.DisplayAlert("Information", "Un compte avec cette adresse mail existe déjà", "Ok");
                }
                catch(Exception)
                {
                    await Application.Current.MainPage.DisplayAlert("Erreur", "Une erreur est survenue, veuillez réessayer plus tard", "Ok");
                }
            }
        }
    }
}
