using FiguTrading.Models;
using FiguTrading.Services;
using FiguTrading.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FiguTrading.ViewModels
{
    public class AccueilVueModele : BaseViewModel
    {
        private Enchere _prochaineEnchere;
        private Enchere _enchere;
        private ObservableCollection<Enchere> _lesEncheresEnCours;
        private readonly Api _apiServices = new Api();
        public AccueilVueModele()
        {
            GetEncheresEnCours();
            GetProchaineEnchere();
            EnchereDetails = new Command(LesDetails);
        }

        public Enchere ProchaineEnchere { get => _prochaineEnchere; set => SetProperty(ref _prochaineEnchere, value); }
        public ObservableCollection<Enchere> LesEncheresEnCours { get => _lesEncheresEnCours; set => SetProperty(ref _lesEncheresEnCours, value); }

        public Command EnchereDetails { get; }

        public Enchere LaEnchere { get => _enchere; set => SetProperty(ref _enchere, value); }

        public async void GetEncheresEnCours()
        {
            LesEncheresEnCours = await _apiServices.GetAsync<Enchere>("getEncheresEnCours");
            

        }
        public async void GetProchaineEnchere()
        {
            ProchaineEnchere = await _apiServices.GetOneAsync<Enchere>("getProchaineEnchere");
        }

        private async void LesDetails()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LaEncherePage(_enchere));
        }
    }
}
