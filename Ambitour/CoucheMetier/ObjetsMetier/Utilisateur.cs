using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.Collections;
using System.Data;
using System.Security.AccessControl;
using System.Security.Principal;
using System.IO;

namespace Ambitour
{
  
    /// <summary>
    /// Représente un utilisateur du système
    /// </summary>
    public class Utilisateur
    {
        #region Variables locales
        public Guid connexionID;
        private string nom;
        private string prenom;
        private string login;
        private string role;
       
        private DossierDeFabrication dossierCourant;
        private ProgrammePiece programmeCourant;

        #endregion

        #region Attributs
        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        public string Nom
        {get{return nom;}set{nom = value;}}
        /// <summary>
        /// Prénom de l'utilisateur
        /// </summary>
        public string Prenom
        {get{return prenom;}set{prenom = value;}}
        /// <summary>
        /// Login de l'utilisateur
        /// </summary>
        public string Login
        {get{return login;}set{login = value;}}
     
        /// <summary>
        /// Role de l'utilisateur dans l'application
        /// </summary>
        public string Role
        {get {return role;}set{role = value;}}

  
        /// <summary>
        /// Dossier courant à traiter ou en  cours de traitement
        /// </summary>
        public DossierDeFabrication DossierCourant
        { get { return dossierCourant; }}

        /// <summary>
        /// Programme courant à traiter ou en cours de traitement
        /// </summary>
        public ProgrammePiece ProgrammeCourant
        { get { return programmeCourant; }}

        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Utilisateur()
        {
        }
        /// <summary>
        /// Constructeur surchargé
        /// </summary>
        /// <param name="pCarte">Carte lue</param>
        public Utilisateur(Carte pCarte)
        {
            connexionID = System.Guid.NewGuid();
            nom = pCarte.Nom;
            prenom = pCarte.Prenom;
            //login = pCarte.Login;
            role = pCarte.Role;
        }

       /// <summary>
        /// Constructeur surchargé avec tous les paramètres
       /// </summary>
       /// <param name="nom"></param>
       /// <param name="prenom"></param>
       /// <param name="login"></param>
       /// <param name="role"></param>
        public Utilisateur(string nom, string prenom, string login, string role)
        {
            this.connexionID = System.Guid.NewGuid();
            this.nom = nom;
            this.prenom = prenom;
            this.login = login;
            this.role = role;
        }

        #endregion

        #region Méthodes publiques

     
        /// <summary>
        /// Défini le programme courant
        /// </summary>
        /// <param name="pFullPath"></param>
        public void SetProgrammeCourant(string pFullPath)
        {
            programmeCourant = new ProgrammePiece(pFullPath);
        }

        /// <summary>
        /// Défini le programme courant
        /// </summary>
        /// <param name="pProgrammePiece"></param>
        public void SetProgrammeCourant(ProgrammePiece pProgrammePiece)
        {
            programmeCourant = pProgrammePiece;
        }
        /// <summary>
        /// Définit le dossier courant
        /// </summary>
        /// <param name="pFullPath">Chemin du répertoire</param>
        public void SetDossierCourant(string pFullPath)
        {
            dossierCourant = new DossierDeFabrication(pFullPath);
        }
        /// <summary>
        /// Définit le dossier courant
        /// </summary>
        /// <param name="pDirectoryInfo">DirectoryInfo</param>
        public void SetDossierCourant(DirectoryInfo pDirectoryInfo)
        {
            dossierCourant = new DossierDeFabrication(pDirectoryInfo);
        }       
        /// <summary>
        /// Définit le dossier courant
        /// </summary>
        /// <param name="pDossier"></param>
        public void SetDossierCourant(DossierDeFabrication pDossier)
        {
            dossierCourant = pDossier;
        }   
   
        #endregion
    }
}

