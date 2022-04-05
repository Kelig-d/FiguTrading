using FiguTrading.Models;
using FiguTrading.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FiguTrading.ViewModels
{
    public class LaEnchereViewModel : BaseViewModel
    {
        private Enchere _enchere;
        private string _texteDate;
        private bool _canEncherir = false;
        private double _prixActuel;
        private readonly Api _apiServices = new Api();
        private double _progress;
        private bool _connected;
        public LaEnchereViewModel(Enchere enchere)
        {
            _enchere = enchere;
            UpdateDate();
            GetPrixActuel();
            Encherir = new Command(MakeEnchere);
            _connected = Constantes.Connected;
        }

        public Command Encherir { get; }
        public Enchere Enchere { get => _enchere; set => SetProperty(ref _enchere , value); }
        
        public string TexteDate { get => _texteDate; set => SetProperty(ref _texteDate, value); }
        public bool CanEncherir { get => _canEncherir; set => SetProperty(ref _canEncherir, value); }
        public double PrixActuel { get => _prixActuel; set => SetProperty(ref _prixActuel, value); }
        public double LeProgress { get => _progress; set => SetProperty(ref _progress, value); }
        public bool Connected { get => _connected; set => _connected = value; }

        public void UpdateDate()
        {
            if (_enchere.DateFin > DateTime.Now)
            {

                    Task.Run(() =>
                    {
                        while (true)
                        {
                            DateTime dateDebut = _enchere.DateDebut;
                            DateTime dateFin = _enchere.DateFin;
                            DateTime current = DateTime.Now;

                            if (dateDebut > current)
                            {
                                TexteDate = "L'enchère démarrera le " + dateDebut.Day + " à " + dateDebut.Hour;
                                CanEncherir = false;

                            }
                            else
                            {
                                var time = dateFin - current;
                                var realTime = dateFin - dateDebut;
                                TexteDate = "Il reste "+time.Days+" jours, " + time.Hours + " heures, " + time.Minutes + " minutes et " + time.Seconds + " secondes"; ;
                                LeProgress = 1 - (double)time.Ticks/(double)realTime.Ticks;
                                CanEncherir = true;
  
                            }
                            Thread.Sleep(1000);
                        }
                    });
                
            }
        }
        public void GetPrixActuel()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    int id = _enchere.Id;
                    Dictionary<string, object> param = new Dictionary<string, object>
                    {
                        { "Id", id }
                    };
                    var encherir = await _apiServices.GetOneAsync<Encherir>("getActualPrice", param);
                    PrixActuel = encherir.PrixEnchere;
                    Thread.Sleep(1000);
                }
            });
        }

        public async void MakeEnchere()
        {
            if (User.CurrentUser != null)
            {
                string pricestr = await Application.Current.MainPage.DisplayPromptAsync("Enchérir", "Quel est le prix que vous voulez mettre", initialValue: PrixActuel.ToString(), keyboard: Keyboard.Numeric);
                double price = Convert.ToDouble(pricestr);
                switch(Enchere.LeType.Nom)
                {
                    case "classique":
                        if (price > PrixActuel)
                        {
                            var encherir = new Encherir(Enchere, User.CurrentUser, DateTime.Now, price);
                            await _apiServices.PostAsyncEncherir("postEncherir", encherir);
                            break;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Erreur", "Impossible d'enchérir en dessous du prix", "OK");
                            break;
                        }
                    case "inversevrai":
                        break;
                }

            }
        }

    }
}