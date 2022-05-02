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
        private Enchere _enchereFlash;
        private ObservableCollection<Enchere> _lesEncheresEnCours;
        private readonly Api _apiServices = new Api();
        private ObservableCollection<Enchere> _lesEncheresFlashs;
        public AccueilVueModele()
        {
            _lesEncheresEnCours = new ObservableCollection<Enchere>();
            GetLesEncheresFlashs();
            GetEncheresEnCours();
            GetProchaineEnchere();
            

            EnchereDetails = new Command(LesDetails);
            
        }

        public Enchere ProchaineEnchere { get => _prochaineEnchere; set => SetProperty(ref _prochaineEnchere, value); }
        public ObservableCollection<Enchere> LesEncheresEnCours { get => _lesEncheresEnCours; set => SetProperty(ref _lesEncheresEnCours, value); }

        public ObservableCollection<Enchere> LesEncheresFlashs
        {
            get => _lesEncheresFlashs;
            set => SetProperty(ref _lesEncheresFlashs, value);
        }
        public Command EnchereDetails { get; }

        public Enchere LaEnchere { get => _enchere; set => SetProperty(ref _enchere, value); }
        
        public Enchere LaEnchereFlash
        {
            get => _enchereFlash;
            set => SetProperty(ref _enchereFlash, value);
        }
        public async void GetEncheresEnCours()
        {
            var tempList = _lesEncheresEnCours.ToList();
            for(int i=1; i<3; i++)
            {
                var param = new Dictionary<string, object>() {{"IdTypeEnchere", i}};
                var encheresByType = await _apiServices.GetAsync<Enchere>("getEncheresEnCours", param);
                var listEncheresByType = encheresByType.ToList();
                tempList.AddRange(listEncheresByType);
            }
            LesEncheresEnCours = new ObservableCollection<Enchere>(tempList);
            
            

        }
        public async void GetProchaineEnchere()
        {
            ProchaineEnchere = await _apiServices.GetOneAsync<Enchere>("getProchaineEnchere");
        }

        private async void LesDetails()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LaEncherePage(_enchere));
        }

        public async void GetLesEncheresFlashs()
        {
            var param = new Dictionary<string, object>() {{"IdTypeEnchere", 3}};
            LesEncheresFlashs = await _apiServices.GetAsync<Enchere>("getEncheresFutures", param);
        }
    }
}
