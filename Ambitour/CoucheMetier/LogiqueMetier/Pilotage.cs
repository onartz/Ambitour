using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using System.Data.SqlClient;
using Ambitour.CoucheMetier.LogiqueMetier;
using Ambitour.CoucheMetier;

namespace Ambitour
{
    /// <summary>
    /// Classe BLL (Business Logic Layer)
    /// </summary>
    public class Pilotage
    {
        [STAThread]
        static void Main()
        {
            Trace.TraceInformation(DateTime.Now + " : Ambiflux started.");
            try
            {
                if (!(Directory.Exists(GlobalSettings.Default.tempRequestDirectory)))
                    Directory.CreateDirectory(GlobalSettings.Default.tempRequestDirectory);
                if (!(Directory.Exists(GlobalSettings.Default.incomingRequestDirectory)))
                    Directory.CreateDirectory(GlobalSettings.Default.incomingRequestDirectory);
                if (!(Directory.Exists(GlobalSettings.Default.outgoingRequestDirectory)))
                    Directory.CreateDirectory(GlobalSettings.Default.outgoingRequestDirectory);
            }
            catch (IOException ex)
            {
                Trace.TraceInformation(DateTime.Now + " : " + ex.Message);
                throw ex;
            }
            INSTANCE.Start();
        }

        #region Variables locales
        //Mode de marche
        private string mode;
        /// <summary>
        /// backgroundWorker utilisé pour exécuter les tâches asynchrones
        /// </summary>
        private BackgroundWorker backgroundWorker1;
        
 
        
        private static Pilotage instance;
        private LecteurBadge lecteurBadge = new LecteurBadge();

        private EmplacementDossier fileAttenteCFAO = new EmplacementDossier(CoucheMetier.GlobalSettings.Default.strQueuePath);
        private EmplacementDossier emplacementDEMOS = new EmplacementDossier(CoucheMetier.GlobalSettings.Default.strRepertoireDossiersDemo);

        #endregion

        #region Attributs publics
        /// <summary>
        /// Singleton
        /// </summary>        
        public static Pilotage INSTANCE
        {
            get
            {
                if (instance == null)
                    instance = new Pilotage();
                return instance;
            }
        }

        public string Mode
        {get { return mode; }set { mode = value; }}

        public EmplacementDossier FileAttenteCFAO
        { get { return fileAttenteCFAO; } set { fileAttenteCFAO = value; } }

        public EmplacementDossier EmplacementDEMOS
        { get { return emplacementDEMOS; } set { emplacementDEMOS = value; } }

        #endregion


        #region BackgroundWorker
       

        // Initialisation du backgroundWorker
        private void InitializeBackgoundWorker()
        {
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);           
        }

        /// <summary>
        /// Sur progression du backgroundWorker, on déclenche des évènements pour l'extérieur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentState state = (CurrentState)e.UserState;
            StatusPreparationChanged(null, new ProcessEventArgs(state.Etape, state.Message, state.Percentage));
        }

        /// <summary>
        /// Lorsque le backgroundWorker a fini
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CurrentState state = (CurrentState)e.UserState;
            StatusPreparationChanged(null, new ProcessEventArgs(6, "Fin", 100));

            //On enregistre l'évènement dans la base de données
            //BD.PROGRAMME.Enregistrer();          
            Num1050.FocusToIHMNUM();

            //Si l'on est en mode DEMO, on affiche le 3D directement
            if (mode == "DEMO")
            {
                Viewer3D v = new Viewer3D(SessionInfos.Utilisateur.DossierCourant.GetFichiers("*.3dxml")[0].FullName);
                Thread t = new Thread(new ThreadStart(v.AfficheViewer));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }  
        }

        /// <summary>
        /// Tache du backgroundWorker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {          
            BackgroundWorker worker = sender as BackgroundWorker;
            DossierDeFabrication d = SessionInfos.Utilisateur.DossierCourant;
            ProgrammePiece p = SessionInfos.Utilisateur.ProgrammeCourant;
            Preparer(ref d, ref p, worker, e);
        }

  
        #endregion

        #region Constructeurs
        
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Pilotage()
        {
        InitializeBackgoundWorker();
        }

        #endregion

        #region Evènements
        /// <summary>
        /// Evénements en provenance du lecteur et de la CN
        /// </summary>
        public event EventHandler<CustomEventArgs> StatusLecteurChanged;
        public event EventHandler<Num1050.CNEventArgs> StatusCNChanged;
        public event EventHandler<Num1050.CNEventArgs> CNCommunicationFailed;
        public event EventHandler<Num1050.CNEventArgs> CNLogChanged;
       

        //Evénement levé aux différentes étapes de préparation
        public event EventHandler<ProcessEventArgs> StatusPreparationChanged;

        //public delegate void CustomEventHandler(object sender, CustomEventArgs e);

        protected virtual void OnStatusLecteurChanged(CustomEventArgs e)
        {
            EventHandler<CustomEventArgs> handler = StatusLecteurChanged;
            if (handler != null)             
                handler(this, e);
        }
        protected virtual void OnStatusCNChanged(Num1050.CNEventArgs e)
        {
            EventHandler<Num1050.CNEventArgs> handler = StatusCNChanged;
            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnStatusPreparationChanged(ProcessEventArgs e)
        {
            EventHandler<ProcessEventArgs> handler = StatusPreparationChanged;
            if (handler != null)
                handler(this, e);
        }


        protected virtual void OnCNCommunicationFailed(Num1050.CNEventArgs e)
        {
            EventHandler<Num1050.CNEventArgs> handler = CNCommunicationFailed;
            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnCNLogChanged(Num1050.CNEventArgs e)
        {
            EventHandler<Num1050.CNEventArgs> handler = CNLogChanged;
            if (handler != null)
                handler(this, e);

        }

       
        #endregion

        #region Méthodes publiques
        /// <summary>
        /// Démarrage de l'application de pilotage
        /// </summary>
        public void Start()
        {
            //Log.Write(System.DateTime.Now + " : Démarrage Ambitour");
            //Trace.TraceInformation(System.DateTime.Now + " : Démarrage Ambitour");
           
            //Test de la connexion à la BD
            

            try
            {
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(Ambitour.Properties.Settings.Default.AIPLConnectionString);
                
                con.Open();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                Trace.TraceError(System.DateTime.Now  + "Erreur lors de la connexion à la BD");
                Trace.TraceError("Server : " + e.Server);
                Trace.TraceError("Message : " + e.Message);
                //MessageBox.Show(System.DateTime.Now + "Erreur lors de la connexion à la BD.");
                DialogResult result1 = MessageBox.Show(System.DateTime.Now + "Erreur lors de la connexion à la BD.",
                     "Database error",
                     MessageBoxButtons.OK);    
            }
         
            //Test du lecteur de carte
            bool res = lecteurBadge.Open();
            if (!lecteurBadge.Open())
            {
                Trace.TraceError(DateTime.Now + "Erreur lecteur badge.");
                DialogResult result1 = MessageBox.Show(System.DateTime.Now + "Le lecteur de badge ne fonctionne pas. Vérifier et relancer l'application",
 "CardReader Error",
 MessageBoxButtons.OK);
                return;
            };

            Trace.TraceInformation(DateTime.Now + " : Lecteur badge ok.");
            
            //enregistrement du début de session
            try
            {
                BD.RESOURCEEVENT.Enregistrer("Démarrage Ambitour");
            }
            catch (SqlException ex)
            {
                Trace.TraceInformation(DateTime.Now + " : Erreur écriture dans la base.");
                //Pas grave
            }

            Trace.TraceInformation(DateTime.Now + " : Tentative de lecture du dossier distant...");
            //Test de lecture du dossier Ambitour distant
            if (!Directory.Exists(CoucheMetier.GlobalSettings.Default.strQueuePath))
            {
                Trace.TraceError(System.DateTime.Now + "Failed (" + CoucheMetier.GlobalSettings.Default.strQueuePath + ").");
                DialogResult result1 = MessageBox.Show(System.DateTime.Now + "Dossier Ambitour distant non accessible",
                    "FileSystem Error",
                    MessageBoxButtons.OK);
            }

            Trace.TraceInformation(DateTime.Now + " OK.");

    
            //Abonnement aux évènements de la classe LecteurBadge
            lecteurBadge.CarteLue += new EventHandler<CustomEventArgs>(LecteurBadge_CarteLue);
            lecteurBadge.StatusChanged += new EventHandler<CustomEventArgs>(LecteurBadge_statusChanged);

          
            //Abonnement aux évènements de la classe NUM1050
            Num1050.INSTANCE.StatusChanged += new EventHandler<Num1050.CNEventArgs>(INSTANCE_NotifierEtat);
            Num1050.INSTANCE.CommunicationFailed += new EventHandler<Num1050.CNEventArgs>(INSTANCE_CommunicationFailed);
            Num1050.INSTANCE.LogEvent += new EventHandler<Num1050.CNEventArgs>(INSTANCE_LogEvent);

            //Démarrage du cycle de la NUM
            //try
            //{
            //    Num1050.INSTANCE.start();
            //}
            //catch (Exception ex)
            //{
            //    return;
            //}

            //Démarrage du lecteur de cartes
            lecteurBadge.Start();
           
            //Création du Form frmStart
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmStart());         
        }

        //Handler de l'évènement LogEvent de la CN
        void INSTANCE_LogEvent(object sender, Num1050.CNEventArgs e)
        {
            Trace.TraceInformation(DateTime.Now + " : LogEvent : " + e.Etat.Message);
           /* DialogResult result1 = MessageBox.Show(System.DateTime.Now + " : " + e.Etat.Message,
                    "CN Log",
                    MessageBoxButtons.OK);*/
        }

        void INSTANCE_CommunicationFailed(object sender, Num1050.CNEventArgs e)
        {
            Trace.TraceError(DateTime.Now + " : CommunicationFailed : " + e.Etat.Message);
            DialogResult result1 = MessageBox.Show(System.DateTime.Now + " : " + e.Etat.Message,
                     "CN Communication Error",
                     MessageBoxButtons.OK);
            Stop();
            
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Arret de l'application de pilotage
        /// </summary>
        public void Stop()
        {
            //Arret du lecteur de badge
            lecteurBadge.Stop();
            //Désabonnement
            lecteurBadge.CarteLue -= new EventHandler<CustomEventArgs>(LecteurBadge_CarteLue);
            lecteurBadge.StatusChanged -= new EventHandler<CustomEventArgs>(LecteurBadge_statusChanged);
            //Arret de lacom avec la CN
            Num1050.INSTANCE.stop();
            //Désabonnement
            Num1050.INSTANCE.StatusChanged -= new EventHandler<Num1050.CNEventArgs>(INSTANCE_NotifierEtat);
            Num1050.INSTANCE.CommunicationFailed -= new EventHandler<Num1050.CNEventArgs>(this.INSTANCE_CommunicationFailed);
            Num1050.INSTANCE.LogEvent -= new EventHandler<Num1050.CNEventArgs>(this.INSTANCE_LogEvent);
            ////Enregistrement stop en BD
            BD.RESOURCEEVENT.Enregistrer("Arrêt Ambitour");
            Application.Exit();
        }


        /// <summary>
        /// Demande de quitter l'application
        /// </summary>
        public void FermerSession()
        {
            //Annulation du backgroundWorker
            backgroundWorker1.CancelAsync();
           
          
            switch (mode)
            {
                case "DEMO":
                    //BD.CONNEXION.EnregistrerConnexionStop();  
                    break;
                default:
                    //BD.CONNEXION.EnregistrerConnexionStop();  
                    string dossierArchivage = CoucheMetier.GlobalSettings.Default.strQueuePath + "\\" + CoucheMetier.GlobalSettings.Default.strDefaultArchiveQueueName;
                    try
                    {
                        DirectoryInfo diArchivageDossier = new DirectoryInfo(dossierArchivage + "\\" + SessionInfos.Utilisateur.connexionID.ToString());
                        Num1050.FocusToIHMNUM();
                    }
                    catch (IOException ex)
                    {
                        Log.Write("Erreur suppression répertoire");
                    }
                    break;
            }
       
        }


        /// <summary>
        /// Demande de préparation
        /// </summary>
        public void PreparerCN()
        {
            backgroundWorker1.RunWorkerAsync();
        }

    /// <summary>
    /// Demande de préchauffage envoyée à la CN
    /// </summary>
        public void DemandePrechauffage()
        {
            Num1050.INSTANCE.Prechauffer();
            
             //Enregistrement en BD de la demande
             //BD.PROGRAMME.Enregistrer(Num1050.NUMEROPROGRAMMEPRECHAUFFAGE);                 
        }
        #endregion

        #region Méthodes privées

        /// <summary>
        /// Méthode exécutée dans le backgroundworker
        /// </summary>
        /// <param name="dossier"></param>
        /// <param name="programme"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// 
        private void Preparer(ref DossierDeFabrication pDossier, ref ProgrammePiece pProgramme, System.ComponentModel.BackgroundWorker pWorker, System.ComponentModel.DoWorkEventArgs e)
        {
            RetourFonction r;
            CurrentState state = new CurrentState();
            System.Threading.Thread.Sleep(100);

            state.Etape = 1;
            state.Message = "Start";
            state.Percentage = 0;
            pWorker.ReportProgress(state.Percentage, state);
            //Etape 1 : Téléchargement du dossier sur le disque du TBI   
            try
            {
                pDossier.Telecharger(CoucheMetier.GlobalSettings.Default.strRepertoireLocalDossiersDeFabrication);
                //Le programme pièce se trouve maintenant théoriquement sur le disque d'AIP-TBI540
                //on modifie sa référence

                //ModifierRefProgrammePiece(ref pProgramme, CoucheMetier.GlobalSettings.Default.strRepertoireLocalDossiersDeFabrication);
                pProgramme.ModifierRef(pDossier.InfosDossierTransfere.FullName + "\\" + pProgramme.InfosFichier.Name);
                pProgramme.SupprimerNumeroDeProgramme();
            }
            catch (Exception ex)
            {
                state.Message = ex.Message;
                state.Etape = 10;
                pWorker.ReportProgress(0, state);
                return;
            }

           // return;


            //Etape 2 : Téléchargement du pgm pièce dans la CN
            state.Etape = 2;
            state.Message = "Téléchargement du dossier OK";
            state.Percentage = 20;
            pWorker.ReportProgress(state.Percentage, state);
            System.Threading.Thread.Sleep(100);

            //Suppression du programme ayant le même numéro que celui que l'on souhaite charger
            //si une erreur survient en supprimant un pgm qui n'existe pas (5), on continue
            r = Num1050.INSTANCE.SupprimerProg(pProgramme);
            if ((r.CodeErreur != 0) && (r.CodeErreur != 5))
            {
                state.Message = r.MessageErreur;
                state.Etape = 10;
                pWorker.ReportProgress(state.Percentage, state);
                return;
            }
            //Chargement du nouveau programme
            r = Num1050.INSTANCE.ChargerProg(pProgramme);
            if (r.CodeErreur != 0)
            {
                state.Message = r.MessageErreur;
                state.Etape = 10;
                pWorker.ReportProgress(state.Percentage, state);
                return;
            }

            //Etape 3 : Mise en pgm courant
            state.Etape = 3;
            state.Message = "Chargement programme OK";
            state.Percentage = 40;
            pWorker.ReportProgress(state.Percentage, state);
            System.Threading.Thread.Sleep(100);

            r = Num1050.INSTANCE.SetProgCourant(pProgramme.NumeroDeProgramme);
            if (r.CodeErreur != 0)
            {
                state.Message = r.MessageErreur;
                state.Etape = 10;
                pWorker.ReportProgress(state.Percentage, state);
                return;
            }

            //Etape 4 : Mode CONT
            state.Etape = 4;
            state.Message = "Mise en programme courant OK";
            state.Percentage = 60;
            pWorker.ReportProgress(state.Percentage, state);
            System.Threading.Thread.Sleep(100);

            r = Num1050.INSTANCE.SetMode("CONT");
            if (r.CodeErreur != 0)
            {
                state.Message = r.MessageErreur;
                state.Etape = 40;
                pWorker.ReportProgress(state.Percentage, state);
                return;
            }

            if (mode == "DEMO")
            {
                //Etape 5 : fin
                state.Etape = 5;
                state.Message = "Mode CONT OK - Fin de préparation";
                state.Percentage = 100;
                pWorker.ReportProgress(state.Percentage, state);
                System.Threading.Thread.Sleep(100);
            }
            //Si pas en mode DEMO, on archive le dossier
            else
            {
                //Etape 5 : Archivage
                state.Etape = 6;
                state.Message = "Mode CONT OK";
                state.Percentage = 80;
                pWorker.ReportProgress(state.Percentage, state);
                System.Threading.Thread.Sleep(100);
                try
                {
                    string dossierArchivage = CoucheMetier.GlobalSettings.Default.strQueuePath + "\\" + CoucheMetier.GlobalSettings.Default.strDefaultArchiveQueueName;
                    DirectoryInfo diArchivageSession = new DirectoryInfo(dossierArchivage + "\\" + SessionInfos.Utilisateur.connexionID.ToString());
                    if (diArchivageSession.Exists == false)
                        diArchivageSession.Create();
                    DirectoryInfo diArchivageDossier = new DirectoryInfo(dossierArchivage + "\\" + SessionInfos.Utilisateur.connexionID.ToString() + "\\" + pDossier.InfosDossierOrigine.Name);
                    if (diArchivageDossier.Exists)
                        diArchivageDossier.Delete(true);
                    //Archivage
                    pDossier.InfosDossierOrigine.MoveTo(dossierArchivage + "\\" + SessionInfos.Utilisateur.connexionID.ToString() + "\\" + pDossier.InfosDossierOrigine.Name);
                }
                catch (IOException ex)
                {
                    state.Message = ex.Message;
                    state.Etape = 10;
                    pWorker.ReportProgress(state.Percentage, state);
                    return;
                }             
                state.Etape = 7;
                state.Message = "Archivage OK - Fin de préparation";
                state.Percentage =100;
                pWorker.ReportProgress(state.Percentage, state);
                System.Threading.Thread.Sleep(100);
            }    
        }
        #endregion

        #region Traitement des évènements des objets métier
        
        /// <summary>
        /// Traitement de l'évènement Lecteur.StatusChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void LecteurBadge_statusChanged(object sender, CustomEventArgs e)
        {
            Trace.TraceInformation(DateTime.Now + " : " + e.Message);
            INSTANCE.OnStatusLecteurChanged(new CustomEventArgs(e.Message));
        }

        /// <summary>
        /// Traitement de l'évènement Lecteur.CarteLue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void LecteurBadge_CarteLue(object sender, CustomEventArgs e)
        {
            Trace.TraceInformation(DateTime.Now + " : Carte lue : " + e.Message);
            DialogResult r;
            Carte lCarte = new Carte(e.Message.ToString()); 
                    
            //Création d'un utilisateur
            Utilisateur lutilisateur = null;  

            switch (lCarte.Type)
            {                   
                case "DEMOAIPL":
                    try
                    {
                        //Création d'un utilisateur "DEMO"
                        lutilisateur = new Utilisateur(lCarte);
                        //Dossier = celui sur la carte
                        DossierDeFabrication d=INSTANCE.EmplacementDEMOS.GetDossierByName(lCarte.Dossier);
                        lutilisateur.SetDossierCourant(d.InfosDossierOrigine);
                        List<ProgrammePiece> l = d.GetProgrammesPiece();
                        lutilisateur.SetProgrammeCourant(l[0].InfosFichier.FullName);
                    }                     
                    catch (Exception ex)
                    {
                        r = MessageBox.Show(ex.Message, "Exception");
                        return;
                    }    
                   SessionInfos.Utilisateur = lutilisateur;
                   INSTANCE.mode = "DEMO";
                   
                    break;
                   
                case "UL":

                   
                    List<Utilisateur> listeUtilisateurs = ServiceUHP.GetUtilisateursAD(lCarte.Nom, lCarte.Prenom, lCarte.Role);

                    switch (listeUtilisateurs.Count)
                    {
                        case 0:
                            r = MessageBox.Show("Aucun utilisateur trouvé dans l'AD", "Exception");
                            return;
                        case 1:
                            lutilisateur = listeUtilisateurs[0];
                            INSTANCE.mode = "CFAO";
                            break;
                        default:
                            frmSelUtilisateur f = new frmSelUtilisateur(listeUtilisateurs);
                            if (f.ShowDialog() == DialogResult.OK)
                            {
                                lutilisateur = f.Utilisateur;
                                INSTANCE.mode = "CFAO";
                                f.Dispose();
                            }
                            else
                            {
                                f.Dispose();
                                r = MessageBox.Show("Aucun utilisateur trouvé dans l'AD", "Exception");
                                return;
                            }
                            break;
                    }
                    break;
                default:
                    r = MessageBox.Show("La carte est muette", "Exception");
                    lutilisateur = new Utilisateur("test","test","test","Etudiant");;
                    INSTANCE.mode = "CFAO";
                    break;
                    //return;
            }
            Trace.TraceInformation(DateTime.Now + " : Récupération des dossiers");   
            List<DossierDeFabrication> lListeDossiers = Pilotage.INSTANCE.FileAttenteCFAO.GetDossiersByUser(lutilisateur);
            if (lListeDossiers == null){
                Trace.TraceInformation(DateTime.Now + " : pas de dossier pour l'utilisateur");
            }
            //On regarde s'il existe un seul dossier contenant un seul fichier pour initailiser
            if (lListeDossiers.Count == 1)
            {
                lutilisateur.SetDossierCourant(lListeDossiers[0]);
                List<ProgrammePiece> l = lutilisateur.DossierCourant.GetProgrammesPiece();
                if (l.Count == 1)
                    lutilisateur.SetProgrammeCourant(l[0]);
            }

            SessionInfos.Utilisateur = lutilisateur;
            //BD.CONNEXION.EnregistrerConnexionStart();
            frmPrincipal frmPrincipal= new frmPrincipal();
            frmPrincipal.ShowDialog();
        }


        /// <summary>
        /// Traitement des évenements venant de la CN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void INSTANCE_NotifierEtat(object sender, Num1050.CNEventArgs e)
        {
            if (!(e.Etat.IsConnected))
            {
                Trace.TraceError(System.DateTime.Now + "Erreur lors de la connexion à la CN");
                //MessageBox.Show(System.DateTime.Now + "Erreur lors de la connexion à la BD.");
                DialogResult result1 = MessageBox.Show(System.DateTime.Now + "Erreur lors de la connexion à la CN.",
                     "CN Communication error",
                     MessageBoxButtons.OK);   
            }

            if (e.Etat.FdBrocheTourne)
                BD.RESOURCEEVENT.Enregistrer("Arrêt broche");
            if (e.Etat.FmBrocheTourne)
                BD.RESOURCEEVENT.Enregistrer("Démarrage broche");
        }
        #endregion

    }
}

     
