using FiguTrading.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiguTrading.Models
{
    public class Encherir
    {
        #region Attributs
        private Enchere _laEnchere;
        private User _leUser;
        private DateTime _laDate;
        private double _prixEnchere;
        private int _idUser;


        public static List<Encherir> CollClass = new List<Encherir>();

        #endregion

        #region Constructeur
        public Encherir(Enchere laEnchere, User leUser, DateTime dateenchere, double prixEnchere, int id = 0)
        {
            _laEnchere = laEnchere;
            _leUser = leUser;
            _laDate = dateenchere;
            _prixEnchere = prixEnchere;
            _idUser = id;
        }

        #endregion

        #region Getters/Setters
        public Enchere LaEnchere { get => _laEnchere; set => _laEnchere = value; }
        public User LeUser { get => _leUser; set => _leUser = value; }
        public DateTime LaDate { get => _laDate; set => _laDate = value; }
        public double PrixEnchere { get => _prixEnchere; set => _prixEnchere = value; }

        public int IdUser
        {
            get => _idUser;
            set => _idUser = value;
        }
    


        #endregion

        #region Methodes
        #endregion
    }
}
