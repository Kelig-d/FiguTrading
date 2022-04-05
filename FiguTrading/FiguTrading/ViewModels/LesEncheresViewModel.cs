using FiguTrading.Models;
using FiguTrading.Services;
using FiguTrading.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FiguTrading.ViewModels
{
    public class LesEncheresViewModel :BaseViewModel
    {
        #region Attributs
        private ObservableCollection<Enchere> _maListeEnchere;
        private Enchere _enchere;
        private readonly Api _apiServices = new Api();
        private INavigation _navigation;
        public Command EnchereDetails { get; }
        #endregion

        #region Constructeur
        public LesEncheresViewModel(INavigation navigation)
        {
            GetListeEncheres();
            EnchereDetails = new Command(LesDetails);
            _navigation = navigation;


        }
        #endregion

        #region Getters/Setters
        public ObservableCollection<Enchere> MaListeEnchere { 
            get => _maListeEnchere; 
            set =>SetProperty(ref _maListeEnchere, value);
        }

        public Enchere LaEnchere { get => _enchere; set => _enchere = value; }

        public INavigation Navigation { get => _navigation; set => _navigation = value; }
        #endregion

        #region Methodes
        public async void GetListeEncheres()
        {
            MaListeEnchere = await _apiServices.GetAsync<Enchere>("getEncheresFutures");
            
        }

        private async void LesDetails()
        {
            await Navigation.PushAsync(new LaEncherePage(_enchere));
        }


        #endregion
    }
}