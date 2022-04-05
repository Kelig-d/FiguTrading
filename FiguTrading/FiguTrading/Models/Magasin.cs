using System;
using System.Collections.Generic;
using System.Text;

namespace FiguTrading.Models
{
    public class Magasin
    {
        #region attributs

        private string _nom;
        private string _adresse;
        private string _ville;
        private string _cp;
        private double _latitude;
        private double _longitude;
        private string _telephone;

        public static List<Magasin> CollClass = new List<Magasin>();
        #endregion

        #region constructeur
        public Magasin(string nom, double latitude, double longitude, string telephone, string adresse, string ville, string cp)
        {
            _nom = nom;
            _latitude = latitude;
            _longitude = longitude;
            _telephone = telephone;
            _adresse = adresse;
            _ville = ville;
            _cp = cp;

            CollClass.Add(this);
        }

        #endregion

        #region getters/setters
        public string Nom { get => _nom; set => _nom = value; }
        public double Latitude { get => _latitude; set => _latitude = value; }
        public double Longitude { get => _longitude; set => _longitude = value; }
        public string Telephone { get => _telephone; set => _telephone = value; }
        public string Adresse { get => _adresse; set => _adresse = value; }
        public string Ville { get => _ville; set => _ville = value; }
        public string Cp { get => _cp; set => _cp = value; }
        #endregion

        #region methodes

        #endregion
    }
}
