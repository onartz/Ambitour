using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Ambitour
{
    public static class BD
    {
        #region Constantes
        private const int TBI540_ID = 11;
        #endregion

        //public static class CONNEXION
        //{
        //    public static void EnregistrerConnexionStart()
        //    {
        //        try
        //        {
        //            DataSetAmbitourTableAdapters.CONNEXIONTableAdapter ta = new Ambitour.DataSetAmbitourTableAdapters.CONNEXIONTableAdapter();
        //            ta.Insert(SessionInfos.Utilisateur.connexionID, SessionInfos.Utilisateur.Login, SessionInfos.Utilisateur.Nom, SessionInfos.Utilisateur.Prenom, SessionInfos.Utilisateur.Role, System.DateTime.Now,null);
        //            ta.Dispose();
        //        }
        //         catch(Exception ex)
        //        {
        //            Log.Write(ex.Message);
        //        }
        //    }

        //    public static void EnregistrerConnexionStop()
        //    {
        //       try 
        //        {	        
        //             DataSetAmbitourTableAdapters.CONNEXIONTableAdapter ta = new Ambitour.DataSetAmbitourTableAdapters.CONNEXIONTableAdapter();
        //             ta.UpdateConnexionStop(System.DateTime.Now, SessionInfos.Utilisateur.connexionID);
        //             ta.Dispose();
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Write(ex.Message);
        //        }
        //    }
        //}

        //public static class PROGRAMME
        //{
        // public static void Enregistrer()
        //    {
        //       try 
        //        {	        
        //             DataSetAmbitourTableAdapters.PROGRAMMETableAdapter ta=new Ambitour.DataSetAmbitourTableAdapters.PROGRAMMETableAdapter();                  
        //             ta.Insert(SessionInfos.Utilisateur.DossierCourant.InfosDossierOrigine.Name,SessionInfos.Utilisateur.ProgrammeCourant.InfosFichier.Name,SessionInfos.Utilisateur.ProgrammeCourant.NumeroDeProgramme.ToString(),null,null,SessionInfos.Utilisateur.connexionID,0);
        //             ta.Dispose();
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Write(ex.Message);
        //        }
        //    }

        // public static void Enregistrer(Int16 numeroDeProgramme)
        // {
        //     try
        //     {
        //         DataSetAmbitourTableAdapters.PROGRAMMETableAdapter ta = new Ambitour.DataSetAmbitourTableAdapters.PROGRAMMETableAdapter();
        //         ta.Insert(null,null,numeroDeProgramme.ToString(),null,null,SessionInfos.Utilisateur.connexionID,1);
        //         ta.Dispose();
        //     }
        //     catch (Exception ex)
        //     {
        //         Log.Write(ex.Message);
        //     }
        // }
        //}




        /// <summary>
        /// Classe d'accès aux données de la table ResourceEvent
        /// </summary>
        public static class RESOURCEEVENT
        {
            public static void Enregistrer(string description)
            {
                try
                {
                    DataSetAmbitourTableAdapters.ResourceEventTableAdapter ta = new DataSetAmbitourTableAdapters.ResourceEventTableAdapter();
                    ta.Insert(description, System.DateTime.Now, TBI540_ID);      
                    ta.Dispose();
                }
                catch (SqlException ex)
                {
                    throw ex;
                   
                }
            }
           /// <summary>
           /// Récupération du dernier stop broche
           /// </summary>
           /// <returns></returns>
            public static DateTime GetLastStopBroche()
            {
                try
                {
                    DataSetAmbitourTableAdapters.ResourceEventTableAdapter ta = new DataSetAmbitourTableAdapters.ResourceEventTableAdapter();
                    if(ta.getLastStopBroche() != null)
                        return ta.getLastStopBroche().Value;
                    else
                        return new DateTime();
     
                }
                catch (SqlException ex)
                {
                    return new DateTime();
                    
                }
                    
            }
        }
    }

  
}
