using System;
using System.Collections.Generic;

namespace FiguTrading.Models
{
    public class Enchere
    {
        #region Attributs
        private int _id;
        private DateTime _dateDebut;
        private DateTime _dateFin;
        private double _prixReserve;
        private Produit _leProduit;
        private TypeEnchere _leType;
        private Magasin _leMagasin;
        private List<User> _lesParticipants;
        private bool _visibilite;

        public static List<Enchere> CollClass = new List<Enchere>();
        #endregion

        #region Constructeurs
        public Enchere(int id, DateTime dateDebut, DateTime dateFin, double prixReserve, bool visibilite, Produit leproduit, TypeEnchere letypeenchere, Magasin lemagasin)
        {
            _id = id;
            _dateDebut = dateDebut;
            _dateFin = dateFin;
            _prixReserve = prixReserve;
            _leProduit = leproduit;
            _leType = letypeenchere;
            _visibilite = visibilite;
            _leMagasin = lemagasin;

            _lesParticipants = new List<User>();

            CollClass.Add(this);
        }
        #endregion

        #region Getters/Setters
        public DateTime DateDebut { get => _dateDebut; set => _dateDebut = value; }
        public DateTime DateFin { get => _dateFin; set => _dateFin = value; }
        public double PrixReserve { get => _prixReserve; set => _prixReserve = value; }
        public Produit LeProduit { get => _leProduit; set => _leProduit = value; }
        public TypeEnchere LeType { get => _leType; set => _leType = value; }
        public List<User> LesParticipants { get => _lesParticipants; set => _lesParticipants = value; }
        public bool Visibilite { get => _visibilite; set => _visibilite = value; }
        public Magasin LeMagasin { get => _leMagasin; set => _leMagasin = value; }
        public int Id { get => _id; set => _id = value; }
        #endregion

        #region Methodes
        public bool addUser(User user)
        {
            try
            {
                //Intégrer plus tard l'insertion dans la bdd
                LesParticipants.Add(user);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
