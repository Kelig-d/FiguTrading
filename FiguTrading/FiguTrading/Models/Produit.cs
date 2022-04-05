using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FiguTrading.Models
{
    public class Produit
    {
        #region Attributs
        private string _nom;
        private string _photo;
        private double _prix;
        private Magasin _leMagasin;

        public static List<Produit> CollClass = new List<Produit>();

        #endregion

        #region Constructeurs
        public Produit(string nom, string photo, double prix, Magasin leMagasin)
        {
            _nom = nom;
            _photo = photo;
            _prix = prix;
            _leMagasin = leMagasin;

            CollClass.Add(this);
        }
        #endregion

        #region Getters/Setters
        public string Nom { get => _nom; set => _nom = value; }
        public string Photo { get => _photo; set => _photo = value; }
        public double Prix { get => _prix; set => _prix = value; }
        public Magasin LeMagasin { get => _leMagasin; set => _leMagasin = value; }
        #endregion

        #region Methodes

        #endregion
    }
}
