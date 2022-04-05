using System;
using System.Collections.Generic;
using System.Text;

namespace FiguTrading.Models
{
    public class TypeEnchere
    {
        #region Attributs
        private string _nom;

        public static List<TypeEnchere> CollClass = new List<TypeEnchere>();

        #endregion

        #region Constructeurs
        public TypeEnchere(string nom)
        {
            _nom = nom;

            CollClass.Add(this);
        }
        #endregion

        #region Getters/Setters
        public string Nom { get => _nom; set => _nom = value; }

        #endregion

        #region Methodes

        #endregion
    }
}
