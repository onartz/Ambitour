using System;
using System.Collections.Generic;
using System.Text;

namespace Ambitour
{
    /// <summary>
    /// Argument d'Evenements personnalisés
    /// </summary>
    public class CustomEventArgs : EventArgs
    {
        public CustomEventArgs(string s)
        {
            message = s;
        }
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }


    /// <summary>
    /// Retour de fonctions type code_erreur, message_erreur
    /// </summary>
    public class RetourFonction
    {
        private short codeErreur;
        private string messageErreur;

        public short CodeErreur
        { get { return codeErreur; } set { codeErreur = value; } }

        public string MessageErreur
        { get { return messageErreur; } set { messageErreur = value; } }

        /// <summary>
        /// constructeur surchargé
        /// </summary>
        /// <param name="pIntRet"></param>
        /// <param name="pMessage"></param>
        public RetourFonction(short pcodeErreur, string pMessageErreur)
        {
            codeErreur = pcodeErreur;
            messageErreur = pMessageErreur;
        }
    }
    /// <summary>
    /// Argument d'Evenements personnalisés
    /// </summary>
    public class ProcessEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pEtape">Numéro d'étape en cours</param>
        /// <param name="pMessage">MEssage associé</param>
        /// <param name="pPercentage">Pourcentage de réalisation du processus</param>
        public ProcessEventArgs(int pEtape, string pMessage, int pPercentage)
        {
            etape = pEtape;
            message = pMessage;
            percentage=pPercentage;
        }
        private int etape;
        private string message;
        private int percentage;

        public string Message
        {get { return message; }set { message = value; }}
        public int Percentage
        { get { return percentage; } set { percentage = value; } }
        public int Etape
        { get { return etape; } set { etape = value; } }
    }

    /// <summary>
    /// SessionInfos
    /// </summary>
    public static class SessionInfos
    {
        ///// <summary>
        ///// Mode de marche
        ///// </summary>
        //private static string mode;
        //public static string Mode
        //{ get { return mode; } set { mode = value; } }

        /// <summary>
        /// Utilisateur de la session
        /// </summary>
        private static Utilisateur utilisateur;
        public static Utilisateur Utilisateur
        { get { return utilisateur; } set { utilisateur = value; } }
    }

    /// <summary>
    /// Informations sur les processus
    /// </summary>
    public class CurrentState
    {
        public Int16 Etape;
        public string Message;
        public int Percentage;
    }

   

}
