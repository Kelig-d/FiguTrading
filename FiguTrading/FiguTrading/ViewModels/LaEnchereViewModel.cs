using FiguTrading.Models;
using FiguTrading.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FiguTrading.ViewModels
{
    public class LaEnchereViewModel : BaseViewModel
    {
        private Enchere _enchere;
        private string _texteDate;
        private bool _canEncherir;
        private double _prixActuel;
        private readonly Api _apiServices = new Api();
        private double _progress;
        private bool _connected;
        private Encherir _encherir;
        private bool _enabled = true;
        private bool _isClassic;
        private bool _isInverse;
        private bool _isFlash;

        public LaEnchereViewModel(Enchere enchere)
        {
            _enchere = enchere;
            IsWon();
            ShowType();
            UpdateDate();
            GetPrixActuel();
            Encherir = new Command(MakeEnchere);
            _connected = Constantes.Connected;
        }

        public Command Encherir { get; }

        public Enchere Enchere
        {
            get => _enchere;
            set => SetProperty(ref _enchere, value);
        }

        public string TexteDate
        {
            get => _texteDate;
            set => SetProperty(ref _texteDate, value);
        }

        public bool CanEncherir
        {
            get => _canEncherir;
            set => SetProperty(ref _canEncherir, value);
        }

        public double PrixActuel
        {
            get => _prixActuel;
            set => SetProperty(ref _prixActuel, value);
        }

        public double LeProgress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

        public bool Connected
        {
            get => _connected;
            set => _connected = value;
        }

        public bool IsClassic
        {
            get => _isClassic;
            set => SetProperty(ref _isClassic, value);
        }

        public bool IsInverse
        {
            get => _isInverse;
            set => SetProperty(ref _isInverse, value);
        }

        public bool IsFlash
        {
            get => _isFlash;
            set => SetProperty(ref _isFlash, value);
        }

        public bool IsNonFlash
        {
            get => !_isFlash;
            set => SetProperty(ref _isFlash, !value);
        }
        public bool Enabled
        {
            get => _enabled;
            set => SetProperty(ref _enabled, value);
        }

        public void ShowType()
        {
            IsClassic = _enchere.LeType.Nom == "classique";
            IsFlash = _enchere.LeType.Nom == "flash";
            IsInverse = _enchere.LeType.Nom == "inversevrai";
        }

        public async void IsWon()
        {
            var param = new Dictionary<string, object>(){{"Id", _enchere.Id}};
            User gagnant = null;
            await Task.Run(async () =>
            {
                
                while (gagnant == null)
                {
                    gagnant = await _apiServices.GetOneAsync<User>("getGagnant", param);
                    Thread.Sleep(1000);
                }
                
            });
            await Application.Current.MainPage.DisplayAlert("Gagnant", "Le gagnant est "+gagnant.Pseudo, "Ok");
            
        }
        
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
                            TexteDate = "Il reste " + time.Days + " jours, " + time.Hours + " heures, " + time.Minutes +
                                        " minutes et " + time.Seconds + " secondes";
                            LeProgress = (double) time.Ticks / (double) realTime.Ticks;
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
                        {"Id", id}
                    };
                    _encherir = await _apiServices.GetOneAsync<Encherir>("getActualPrice", param);
                    int _idUser = Convert.ToInt32(await SecureStorage.GetAsync("id"));
                    Enabled = (IsClassic && _encherir.IdUser != _idUser) || (IsInverse && _encherir.IdUser == 0);
                    

                    PrixActuel = _encherir.PrixEnchere;
                    Thread.Sleep(1000);
                }
            });
        }

        public async void MakeEnchere()
        {
            if (User.CurrentUser != null)
            {


                    switch (Enchere.LeType.Nom)
                {
                    case "classique":
                        string pricestr = await Application.Current.MainPage.DisplayPromptAsync("Enchérir",
                            "Quel est le prix que vous voulez mettre", initialValue: PrixActuel.ToString(),
                            keyboard: Keyboard.Numeric);
                        double price = Convert.ToDouble(pricestr);
                        if (price > PrixActuel)
                        {
                            var encherir = new Encherir(Enchere, User.CurrentUser, DateTime.Now, price);
                            await _apiServices.PostAsyncEncherir("postEncherir", encherir);

                            break;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Erreur",
                                "L'enchère doit être supérieure au prix", "OK");
                            break;
                        }
                    case "inversevrai":
                        var reservation = new Encherir(Enchere, User.CurrentUser, DateTime.Now, _encherir.PrixEnchere);
                        await _apiServices.PostAsyncEncherir("postEncherir", reservation);
                        
                        break;
                }
            }
        }
    }
}