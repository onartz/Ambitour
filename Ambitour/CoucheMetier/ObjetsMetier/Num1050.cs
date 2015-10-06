using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.ComponentModel;
using System.Threading;

namespace Ambitour
{
    public class Num1050
    {
        #region Classe InfosCN
        public class InfosCN
        {
            private bool isConnected;
            private UInt16 programme;
            private string mode;
            private string message;
            private bool demandePrechauffage;
            private bool pomOK;
            private bool enCycle;
            private bool departCycle;
            private bool brocheTourne;
            private DateTime dernierStopBroche;
            private bool fmBrocheTourne;
            private bool fdBrocheTourne;

            public bool IsConnected
            { get { return isConnected; } set { isConnected = value; } }
            public UInt16 Programme
            { get { return programme; } set { programme = value; } }
            public string Mode
            { get { return mode; } set { mode = value; } }
            public string Message
            { get { return message; } set { message = value; } }
            public bool DemandePrechauffage
            { get { return demandePrechauffage; } set { demandePrechauffage = value; } }
            public bool POMOK
            { get { return pomOK; } set { pomOK = value; } }
            public bool EnCycle
            { get { return enCycle; } set { enCycle = value; } }
            public bool BrocheTourne
            { get { return brocheTourne; } set { brocheTourne = value; } }
            public bool DepartCycle
            { get { return departCycle; } set { departCycle = value; } }
            public bool FmBrocheTourne
            { get { return fmBrocheTourne; } set { fmBrocheTourne = value; } }
            public bool FdBrocheTourne
            { get { return fdBrocheTourne; } set { fdBrocheTourne = value; } }
            public DateTime DernierStopBroche
            { get { return dernierStopBroche; } set { dernierStopBroche = value; } }

            /// <summary>
            /// Tester l'égalité de tous les champs de 2 objets infosCN
            /// </summary>
            /// <param name="p"></param>
            /// <returns></returns>
            private bool Equals(InfosCN p)
            {
                if ((object)p == null)
                {
                    return false;
                }
                // Retourne True si tous les champs correspondent
                return (isConnected == p.IsConnected) && (message == p.Message) && (mode == p.Mode) && (demandePrechauffage = p.DemandePrechauffage);

            }
        }
      
        #endregion  

        #region Constantes

        private const string MESSAGE_ATTENTE = "En cours...";
        private const string MESSAGE_COMMUNICATIONCNOK = "CN OK";
        /// <summary>
        /// Numéro du programme de préchauffage
        /// </summary>
        public const Int32 NUMEROPROGRAMMEPRECHAUFFAGE = 9006;
        /// <summary>
        /// Periode du cycle de scrutation en ms
        /// </summary>
        private const Int16 TPSDECYCLE = 1000;
        #endregion
       
        #region Constantes et Variables UNITE
        private const string CONFIG = "XIP01,0,254,0,0,0";
        private const Int16 RETOK = 0;
  
        //POMs
        private const Byte SEG_POM = 149;
        //Entrées venant de la CN
        private const Byte SEG_R = 164;
        //Sorties vers la CN
        private const Byte SEG_W = 165;
        //Entrées venant du bornier
        private const Byte SEG_I = 168;
        //Mode courant
        private const Byte SEG_SELECTIONMODE = 180;
        //Programme courant
        private const Byte SEG_PROGRAMMECOURANT = 181;
       
        //Valeurs des Modes
        private const UInt16 VAL_MODECONT = 0;
        private const UInt16 VAL_MODESEQ = 1;
        private const UInt16 VAL_MODEPOM = 8;

        private const UInt16 RET_REQUETENONPRETE = 8;
        private const UInt16 IB = 0xA840;
        private const UInt16 I503 = 0X503;

        //numéro du port de communication avec la CN
        private short numPortBgWorker = 0;
       
        #endregion

        #region Constructeurs
        private static Num1050 instance;

        /// <summary>
        /// Constructeur simple
        /// </summary>
        private Num1050()
        {
            //Initialisation du dictionnaire des modes
            Mode.Add(0x0, "CONT");
            Mode.Add(0x1, "SEQ");
            Mode.Add(0x2, "IMD");
            Mode.Add(0x3, "RAP");
            Mode.Add(0x5, "MODIF");
            Mode.Add(0x6, "TEST");
            Mode.Add(0x7, "MANU");
            Mode.Add(0x8, "POM");
            Mode.Add(0xD, "CHARG");
            Mode.Add(0xF, "DECHARG");
            ModeTestSansCN = !CoucheMetier.GlobalSettings.Default.PresenceCN;       
            status.DernierStopBroche = BD.RESOURCEEVENT.GetLastStopBroche();
       }

        /// <summary>
        /// Singleton
        /// </summary>
        public static Num1050 INSTANCE
        {
            get
            {
                if (instance == null)
                    instance = new Num1050();
                return instance;
            }
        }
        #endregion

        #region  Evenements

        /// <summary>
        /// Classe evènement personnalisé. L'évènement contient les infos CN
        /// </summary>
        public class CNEventArgs : EventArgs
        {
            public CNEventArgs(InfosCN e)
            {
                etat = e;
            }
            private InfosCN etat;

            public InfosCN Etat
            {
                get { return etat; }
                set { etat = value; }
            }
        }
        
       
        /// <summary>
        /// Evenement déclenché lors d'un changement d'état
        /// </summary>
        public event EventHandler<CNEventArgs> StatusChanged;

        /// <summary>
        /// Evenement déclenché lorsque la boucle de communication s'arrete
        /// </summary>
        public event EventHandler<CNEventArgs> CommunicationFailed;

        // Wrap event invocations inside a protected virtual method
        // to allow derived classes to override the event invocation behavior
        protected virtual void OnNotifierEtat(CNEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<CNEventArgs> handler = StatusChanged;

            // Event will be null if there are no subscribers
            if (handler != null)
           {  
                handler(this, e);
            }
        }

        
        protected virtual void onNotifierCommunicationFailed(CNEventArgs e)
        {
            EventHandler<CNEventArgs> handler = CommunicationFailed;
            // Event will be null if there are no subscribers
            if (handler != null)
            {
                handler(this, e);
            }

        }

        #endregion

        #region BackgroundWorker

        private static Thread workerThread;
        private static bool workerAllowed;
        private static bool workerRunning;

  
        /// <summary>
        /// Cycle CN
        /// </summary>
        private void workerLoop()
        {
            short intRet;
            //Nécessaire pour passer des paramètres par ref
            UInt16 lPgmCourant=0;
            UInt16 lModeCourant=0;
            UInt16 lI503 = 0;
            bool bitBrocheTourne = false;
           
            //Pour les tests
            //if(!ModeTestSansCN)
            //{

            
          /*  try
            {
                intRet = SetPLCTool(CONFIG);
            }
            catch (DllNotFoundException ex)
            {
                return;
                //throw ex;
                //return;
            }*/
           

            intRet = SetPLCTool(CONFIG);

            if (intRet != RETOK)
            {
                status.IsConnected = false;
                status.Message = "Failed to init CN";
                onNotifierCommunicationFailed(new CNEventArgs(status));
                //OnNotifierEtat(new CNEventArgs(status));
                return;
            }

            if ((intRet = Get_Port(ref numPortBgWorker)) != RETOK)
            {
                status.IsConnected = false;
                status.Message = "Failed to connect to CN";
                onNotifierCommunicationFailed(new CNEventArgs(status));
                return;
            }



                //try
                //{                  
                //    intRet = SetPLCTool(CONFIG);
                //    if (intRet != RETOK) throw new Exception("Initialisation driver impossible");
                //    Thread.Sleep(1000);
                   
                //    intRet = Get_Port(ref numPortBgWorker);
                //    if (intRet != RETOK) throw new Exception("Communication impossible.");
                //    else OnNotifierEtat(new CNEventArgs(status));
                //}
                //catch (Exception ex)
                //{
                //    status.IsConnected = false;
                //    status.Message = ex.Message;
                //    OnNotifierEtat(new CNEventArgs(status));
                //    return;
                //}
            //}
            //else
            //{
            //    status.Message = "Mode test sans CN";
            //    status.IsConnected = true;
            //    OnNotifierEtat(new CNEventArgs(status));
            //}
            //Cycle
            while (workerAllowed)
            {     
                System.Threading.Thread.Sleep(TPSDECYCLE);
                if (!workerAllowed)
                    break;
                try
                {
                    //Lecture des entrées
                    UInt16 eProgrammeCourant=readProgrammeCourant(lPgmCourant);
                    string eMode = readMode(lModeCourant);
                    bool ePOMOK = isPOMOK();
                    bool eIsEnCycle = isEnCycle();
                    bool eBrocheTourne = readBit(IB, I503, 7);

                    //Calcul si besoin de préchauffage
                    //1er cas : la broche n'a pas tourné dans les 20 dernières minutes                   
                    TimeSpan elapsedTime = DateTime.Now.Subtract(status.DernierStopBroche);

                    //Front descendant broche tourne
                    bool fdBroche = ((bitBrocheTourne) && (!eBrocheTourne));
                    //Front montant broche tourne
                    bool fmBroche = ((!bitBrocheTourne) && (eBrocheTourne));
                                                        
                    //Ecriture des sorties                    
                    status.Programme = eProgrammeCourant;
                    status.Mode = eMode;
                    status.POMOK = ePOMOK;
                    status.EnCycle = eIsEnCycle;
                    status.DemandePrechauffage = (elapsedTime.TotalMinutes > 20);
                    status.BrocheTourne = eBrocheTourne;
                    status.FdBrocheTourne=fdBroche;
                    status.FmBrocheTourne=fmBroche; 
                    OnNotifierEtat(new CNEventArgs(status));
                    bitBrocheTourne = eBrocheTourne;
                }
                catch (Exception ex)
                {
                    status.Message = ex.Message;
                    OnNotifierEtat(new CNEventArgs(status));
                    System.Threading.Thread.Sleep(10);
                }
            }
        }
         #endregion
       
        #region Variables locales
        /// <summary>
        /// Dictionnaire des modes
        /// </summary>
        private Dictionary<int, string> Mode = new Dictionary<int, string>();
        //pour pouvoir tester sur un pc non relié à la cn
        private bool ModeTestSansCN;
 
      
        #endregion

        #region Attributs publics
        /// <summary>
        /// Classe des informations à afficher dans les formulaires
        /// </summary>
        private InfosCN status = new InfosCN();
        public InfosCN Status
        { get { return status; }}

   
        #endregion

        #region Méthodes privées

        // Déclaration des fonctions définies dans les DLL NUM
        [DllImport("LIB_UNIT2.dll")]
        private static extern short SetPLCTool(string c);
        [DllImport("LIB_UNIT2.dll")]
        private static extern short Get_Port(ref short n);
        [DllImport("LIB_UNIT2.dll")]
        private static extern short Free_Port(short n);
        [DllImport("DNC2.dll")]
        public static extern short ReadObjectReq(short port, short Segment, short ObjectAdress, short Quantity);
        [DllImport("DNC2.dll")]
        public static extern short ReadObjectResp(short port, ref UInt16 Data, UInt16 Lg_Result);
        [DllImport("DNC2.dll")]
        public static extern short WriteObjectReq(short port, short Segment, short ObjectAdress, short Quantity, ref byte Data, short Lg_Data);
        [DllImport("DNC2.dll")]
        public static extern short WriteObjectResp(short port, ref byte Data);
        [DllImport("DNC2.dll")]
        private static extern short DncDeleteFile(short n, int m);
        [DllImport("DNC2.dll")]
        private static extern short DownLoadFile(short n, int m, string c);
        [DllImport("DNC2.dll")]
        private static extern short WriteObjectVarious2(short n, int m, ref int o);
        [DllImport("DNC2.dll")]
        //private static extern short ReadLadderVar(short port, short eLadderVar, short first, short quantity, ref byte[] pData, short mode );
        private static extern short ReadLadderVar(short n, UInt16 Qui, UInt16 Num, UInt16 Nb, ref UInt16 data, UInt16 Mode);

        /// <summary>
        /// Lecture du numéro de programme courant
        /// </summary>
        /// <param name="pProgram">Variable ou sera rangé le numéro de programme lu dans la CN</param>
        private UInt16 readProgrammeCourant(UInt16 pProgram)
        {
            //short lIntRet = Get_Port(ref numPort);
            //if (lIntRet != RETOK) throw new Exception("Erreur GetPort");
            short lIntRet;
            //Envoi requete 
            lIntRet = ReadObjectReq(numPortBgWorker, SEG_PROGRAMMECOURANT, 0, 1);
            if (lIntRet != RETOK) throw new Exception("Erreur lecture pgm courant");
            //Lecture réponse
            do
                lIntRet = ReadObjectResp(numPortBgWorker, ref pProgram, 2);
            while (lIntRet == RET_REQUETENONPRETE);
            return (pProgram);
        }

        /// <summary>
        /// Lecture du mode courant
        /// </summary>
        /// <param name="pMode">Variable ou sera rangé le Mode lu dans la CN</param>
        /// Attention, pMode n'est pas utilisé car il renvoie un UInt16 et nous voulons une string
        private string readMode(UInt16 pMode)
        {
            //short lIntRet = Get_Port(ref numPort);
            //if (lIntRet != RETOK) throw new Exception("Erreur GetPort");
            short lIntRet;
            //Envoi requete 
            lIntRet = ReadObjectReq(numPortBgWorker, SEG_SELECTIONMODE, 0, 1);
            if (lIntRet != RETOK) throw new Exception("Erreur lecture mode courant");
            //Lecture réponse
            do
                lIntRet = ReadObjectResp(numPortBgWorker, ref pMode, 4);
            while (lIntRet == RET_REQUETENONPRETE);
            //Recherche de la string correpondant au UInt16 lu
            if (Mode.ContainsKey(pMode))
                return (Mode[pMode]);
            else
                return (string.Empty);
        }

        private bool isEnCycle()
        {
            //Bit 2 soit 2^2
            Int32 lMasque = 4;

            UInt16 lCycle = 0;

            //short lIntRet = Get_Port(ref numPort);
            //if (lIntRet != RETOK) throw new Exception("Erreur GetPort");
            short lIntRet;
            //Envoi requete 
            lIntRet = ReadObjectReq(numPortBgWorker, SEG_R, 3, 1);
            if (lIntRet != RETOK) throw new Exception("Erreur lecture Cycle");
            //Lecture réponse
            do
                lIntRet = ReadObjectResp(numPortBgWorker, ref lCycle, 1);
            while (lIntRet == RET_REQUETENONPRETE);
            //Recherche de la string correpondant au UInt16 lu
            return ((lCycle & lMasque) == lMasque);      
        }


        /// <summary>
        /// Lecture des POM
        /// </summary>
        /// <returns></returns>
        private bool isPOMOK()
        {
            UInt16 lPOMs = 0;
            //short lIntRet = Get_Port(ref numPort);
            //if (lIntRet != RETOK) throw new Exception("Erreur GetPort");
            short lIntRet;
            //Envoi requete 
            lIntRet = ReadObjectReq(numPortBgWorker, SEG_POM, 0, 1);
            if (lIntRet != RETOK) throw new Exception("Erreur lecture POMs");
            //Lecture réponse
            do
                lIntRet = ReadObjectResp(numPortBgWorker, ref lPOMs, 4);
            while (lIntRet == RET_REQUETENONPRETE);
            return ((lPOMs & 0x5) == 0);
        }


        //private void DepartCycle()
        //{
        //    UInt16 W3R = 0;
        //    byte W3B = (byte)W3R;
        //    byte W3W = 0;
        //    int i = 4;
        //    byte Mask = (byte)i;
        //    //Envoi requete 
        //    short intRet = ReadObjectReq(numPort, SEG_W,3, 1);
        //    if (intRet != RETOK) throw new Exception("Erreur lecture W3");
        //    //Lecture réponse
        //    do
        //        intRet = ReadObjectResp(numPort, ref W3R,1);
        //    while (intRet == RET_REQUETENONPRETE);
        //    intRet = WriteObjectReq(numPort, SEG_W, 3, 1, ref W3B, sizeof(UInt16));
        //    W3W = (W3B | Mask);
        //}

         

/// <summary>
/// Lecture d'un bit d'une variable automate
/// </summary>
/// <param name="pELadderVar">Voir dans dnc.h</param>
/// <param name="pVariableAutomate"></param>
/// <param name="pPosition">position du bit (0 à 7)</param>
/// <returns></returns>
        private bool readBit(UInt16 pELadderVar, UInt16 pVariableAutomate, UInt16 pPosition)
        {
            UInt16 lTemp = 0;
            Int32 lMasque = System.Convert.ToInt32(Math.Pow(2, pPosition));

            //short lIntRet = Get_Port(ref numPort);
            //if (lIntRet != RETOK) throw new Exception("Erreur GetPort");
            short lIntRet;
            lIntRet = ReadLadderVar(numPortBgWorker, pELadderVar, pVariableAutomate, 1, ref lTemp, 0);
            if (lIntRet != RETOK) throw new Exception("Erreur de lecture du bit " + pPosition.ToString() + " de la variable : " + pVariableAutomate.ToString());
            else
            {
                return ((lTemp & lMasque) == lMasque);            
            }       
        }
    
        #endregion

        #region Méthodes publiques

       /// <summary>
       /// Chargement d'un programme pièce
       /// </summary>
       /// <param name="p"></param>
       /// <returns></returns>
        public RetourFonction ChargerProg(ProgrammePiece p)
        {
            short lNumPort = 0;
            //Nméro de programme
            Int32 numProg = p.NumeroDeProgramme * 10;

            short lIntRet = SetPLCTool(CONFIG);
            if (lIntRet != RETOK) throw new Exception("Initialisation driver impossible");
            //il faut obtenir un numéro de port différent de celui du bgWorker pour cette méthode qui prend du temps
            lIntRet = Get_Port(ref lNumPort);     
            lIntRet = DownLoadFile(lNumPort, numProg, p.InfosFichier.FullName);
            //Libération du port utilisé
            Free_Port(lNumPort);
            switch (lIntRet)
            {
                case 0x3A01:
                    return new RetourFonction(lIntRet, "OpenDownloadSequence() Fichier existant");
                case 0x3B:
                    return new RetourFonction(lIntRet, "Erreur WriteDownLoadSegment()");
                case 0x3C:
                    return new RetourFonction(lIntRet, "Erreur CloseDownLoadSequence()");
                default:
                    return new RetourFonction(lIntRet, lIntRet.ToString()); ;
            }
        }
       /// <summary>
       /// Suppression d'un programme chargé
       /// </summary>
       /// <param name="p"></param>
       /// <returns></returns>
        public RetourFonction SupprimerProg(ProgrammePiece p)
        {
            short lNumPort = 0;
            //Nméro de programme
            Int32 numProg = p.NumeroDeProgramme;

            short lIntRet = SetPLCTool(CONFIG);
            if (lIntRet != RETOK) throw new Exception("Initialisation driver impossible");
            //il faut obtenir un numéro de port différent de celui du bgWorker pour cette méthode qui prend du temps
            lIntRet = Get_Port(ref lNumPort);
            if (lIntRet != RETOK)
                throw(new Exception("Erreur dans le get_Port : " + lIntRet.ToString()));

            switch (lIntRet)
            {
                case 2:
                    return new RetourFonction(lIntRet, "Fonction Open_Unite non faite");
                case 3:
                    return new RetourFonction(lIntRet, "Pas de ports libres");                                 
            }
            lIntRet = DncDeleteFile(lNumPort, (numProg * 10));
            //Libération du port utilisé
            Free_Port(lNumPort);

            switch (lIntRet)
            {
                case 0:
                    return new RetourFonction(lIntRet, "Programme supprimé");
                case 2:
                    return new RetourFonction(lIntRet, "CNC RAM Unavailable");
                case 5:
                    return new RetourFonction(lIntRet, "Program number not found");
                case 0xA:
                    return new RetourFonction(lIntRet, "Program being executed");       
                default:
                    return new RetourFonction(lIntRet, lIntRet.ToString());
            }
        }


        /// <summary>
        /// Mettre un programme en programme courant
        /// </summary>
        /// <param name="intNumProg">Numéro de programme</param>
        /// <returns></returns>
        public RetourFonction SetProgCourant(Int32 pNumeroDeProgramme)
        {
            short lNumPort = 0;
            Int32 lNumeroDeProgramme = pNumeroDeProgramme;
            short lIntRet = SetPLCTool(CONFIG);
            if (lIntRet != RETOK) throw new Exception("Initialisation driver impossible");
            //il faut obtenir un numéro de port différent de celui du bgWorker pour cette méthode qui prend du temps
            lIntRet = Get_Port(ref lNumPort);
            lIntRet = WriteObjectVarious2(lNumPort, SEG_PROGRAMMECOURANT, ref lNumeroDeProgramme);
            //Libération du port utilisé
            Free_Port(lNumPort);
            switch (lIntRet)
            {
                case 0:
                    return new RetourFonction(lIntRet, "OK");
                case 0xFD:
                    return new RetourFonction(lIntRet, "Objet Inexistant");
                default:
                    return new RetourFonction(lIntRet, lIntRet.ToString()); ;
            }   
        }

        /// <summary>
        /// Changer de mode
        /// </summary>
        /// <param name="strMode"></param>
        /// <returns></returns>
        public RetourFonction SetMode(string strMode)
        {
            Int32 intValMode = 0;
            foreach(KeyValuePair<int, string> kvp in Mode)
            {
                if (kvp.Value == strMode)
                {
                    intValMode = kvp.Key;
                    break;
                }
            }
            //short lIntRet = Get_Port(ref numPort);
            //if (lIntRet != 0) return new RetourFonction(lIntRet, "Erreur Getport");

            short lNumPort = 0;           
            short lIntRet = SetPLCTool(CONFIG);
            if (lIntRet != RETOK) throw new Exception("Initialisation driver impossible");
            //il faut obtenir un numéro de port différent de celui du bgWorker pour cette méthode qui prend du temps
            lIntRet = Get_Port(ref lNumPort);
            lIntRet = WriteObjectVarious2(lNumPort, SEG_SELECTIONMODE, ref intValMode);
            //Libération du port utilisé
            Free_Port(lNumPort);
            switch (lIntRet)
            {
                case 0:
                    return new RetourFonction(lIntRet, "OK");
                default:
                    return new RetourFonction(lIntRet, "Erreur de changement de mode"); ;
            }                 
        }

        /// <summary>
        /// Démarrage backgroundWorker
        /// </summary>
        public void start()
        {
            workerAllowed = true;
            workerRunning = false;
            try
            {
                workerThread = new Thread(new ThreadStart(workerLoop));
            }
            catch (DllNotFoundException ex)
            {
                return;
            }
            workerThread.Start();
        }


        /// <summary>
        /// Arret backgroundWorker
        /// </summary>
        public void stop()
        {
            int retry = 50;
            workerAllowed = false;
            while ((workerRunning) && (retry-- > 0))
                Thread.Sleep(20);
            //Libération du port
            short lIntRet=Free_Port(numPortBgWorker);
        }

        /// <summary>
        /// Préchauffage de la broche
        /// </summary>
        public void Prechauffer()
        {
             SetMode("CONT");         
             SetProgCourant(NUMEROPROGRAMMEPRECHAUFFAGE);
             FocusToIHMNUM();
        }
        #region MAJ interface NUM
        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        // Remettre le focus sur la fenêtre NUM
        public static void FocusToIHMNUM()
        {
            //Récupération du handle de fenêtre avec Spy++
            //window class, window name
            IntPtr NUM1050Handle = FindWindow("IHMClass", "IHM NUM");

            if (NUM1050Handle == IntPtr.Zero)
            {
                return;
            }
            SetForegroundWindow(NUM1050Handle);
        }
        #endregion
        #endregion
  
        }
    }

