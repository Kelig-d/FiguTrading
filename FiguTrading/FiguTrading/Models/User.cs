using FiguTrading.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace FiguTrading.Models
{
    public class User
    {
        #region Attributs
        private int _id;
        private string _nom;
        private string _prenom;
        private string _pseudo;
        private string _email;
        private string _photo;
        private string _pwd;
        private string _adresse;
        private string _cp;
        private string _ville;
        private string _telephone;
        private double _latitude;
        private double _longitude;


        public static User CurrentUser;

        #endregion

        #region Constructeurs
        public User (int id, string pseudo, string pwd, string email, string photo, string nom = null, string prenom = null, string adresse = null, string cp = null, string ville = null, string telephone = null)
        {
            _id = id;
            _nom = nom;
            _prenom = prenom;
            _pseudo = pseudo;
            _pwd = pwd;
            _adresse = adresse;
            _cp = cp;
            _ville = ville;
            _telephone = telephone;
            _email = email;
            _photo = photo;

        }

        public User()
        {

        }
        #endregion

        #region Getters/Setters
        public string Nom { get => _nom; set => _nom = value; }
        public string Prenom { get => _prenom; set => _prenom = value; }
        public string Pseudo { get => _pseudo; set => _pseudo = value; }
        public string Password { get => _pwd; set => _pwd = value; }
        public string Adresse { get => _adresse; set => _adresse = value; }
        public string Cp { get => _cp; set => _cp = value; }
        public string Ville { get => _ville; set => _ville = value; }
        public string Telephone { get => _telephone; set => _telephone = value; }
        public string Email { get => _email; set => _email = value; }
        public string Photo { get => _photo; set => _photo = value; }
        public int Id { get => _id; set => _id = value; }
        public double Latitude { get => _latitude; set => _latitude = value; }
        public double Longitude { get => _longitude; set => _longitude = value; }

        #endregion

        #region Methodes
        #endregion
    }
}
