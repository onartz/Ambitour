using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Timers;
using SpringProxAPI;
using System.Xml;

namespace Ambitour
{
    public class LecteurBadge
    {
        #region Evènements

        /// <summary>
        /// Déclaration de l'Evènement envoyé lorsqu'un badge est reconnu
        /// </summary>
        /// 
        public event EventHandler<CustomEventArgs> CarteLue;
        /// <summary>
        /// Déclaration de l'évènement statusChanged
        /// </summary>
        public event EventHandler<CustomEventArgs> StatusChanged;

        /// <summary>
        /// Déclaration de l'évènement statusChanged
        /// </summary>
        public event EventHandler<CustomEventArgs> LogChanged;

        #endregion

        #region Constantes
        private const int CARD_UNKNOWN = -1;
        private const int CARD_NONE = 0;
        private const int CARD_MIFARE_1K = 1;
        private const int CARD_MIFARE_4K = 2;
        private const int CARD_MIFARE_UL = 3;
        private const int FREQUENCE = 1000;
        #endregion

        #region Variables privées

        private bool card_is_tcl;
        private bool card_is_type_a;
        private bool card_is_type_b;
        private int card_is_type;

        private byte[] card_iso_a_atq = new byte[2];
        private byte[] card_iso_a_sak = new byte[1];
        private byte[] card_iso_a_snr = new byte[12];
        private byte card_iso_a_snrlen;
        private byte[] card_iso_b_atq = new byte[11];

        private byte[] card_tcl_ats = new byte[32];
        private byte card_tcl_atslen;

        //Base de temps du cycle
        private System.Timers.Timer tmrBasetime;

        private bool isActif;


        /// <summary>
        /// La carte détectée
        /// </summary>
        private Carte carte;
        #region Mifare reading

        private byte[] MIFARE_HEADER =
          new byte[16] { 0x00, 0x00, 0x50, 0x72, 0x6F, 0x2D, 0x41, 0x63, 0x74,
      0x69, 0x76, 0x65, 0x00, 0x11, 0x22, 0x33
    };
        //public byte[] MIFARE_KEY_A =
        //  new byte[6] { 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5 };

        private byte[] MIFARE_KEY_A =
         new byte[6] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

        #endregion
 

        #endregion

        #region Attributs publics

        public bool IsActif { get { return isActif; } set { isActif = value; } }

        #endregion

        #region Méthodes publiques
        /// <summary>
        /// Methode pour démarrer le cycle de lecture
        /// </summary>
        public void Start()
        {
            workerAllowed = true;
            workerRunning = false;
            workerNextTask = 0;
            workerDoneTask = 0;
            workerResult = false;
            isActif = false;

            tmrBasetime = new System.Timers.Timer();
            tmrBasetime.Enabled = true;
            tmrBasetime.Interval = FREQUENCE;
            tmrBasetime.Elapsed += new ElapsedEventHandler(tmrBasetime_Elapsed);

            workerEvent = new ManualResetEvent(false);
           
            workerThread = new Thread(new ThreadStart(workerLoop));
            workerThread.Start();
         
            workerStart(WORK_OPEN_READER);
           
        }

        /// <summary>
        /// Méthode pour stopper le cycle de lecture
        /// </summary>
        public void Stop()
        {
            EventHandler<CustomEventArgs> handler = StatusChanged;
            EventHandler<CustomEventArgs> handlerLog = LogChanged;

            // Event will be null if there are no subscribers
            if (handler != null)
            {
                handler(null, new CustomEventArgs("Arrêt..."));
            }

            // Event will be null if there are no subscribers
            if (handlerLog != null)
            {
                handlerLog(null, new CustomEventArgs("Arrêt..."));
            }

            int retry = 50;

            workerAllowed = false;
            if(workerEvent != null)
                workerEvent.Set();

            while ((workerRunning) && (retry-- > 0))
                Thread.Sleep(20);

            CloseReader();
        }

        public bool Open()
        {
            /* In case of a communcation error, we must close it before */

            SPROX.ReaderClose();
            SPROX.ControlLed(1, 0, 0);
            short rc = 0;
            /* Try to open the reader */
            rc = SPROX.ReaderOpen("");
            return rc == SPROX.MI_OK;
           
            
        }
        #endregion

        #region Méthodes privées

        /// <summary>
        /// Arret du lecteur
        /// </summary>
        private void CloseReader()
        {
            if (isActif)
                SPROX.ReaderClose();
            //EventHandler<CustomEventArgs> eventStatusChanged = statusChanged;
            //eventStatusChanged(null, new CustomEventArgs("Arrêté"));
            EventHandler<CustomEventArgs> handler = StatusChanged;

            // Event will be null if there are no subscribers
            if (handler != null)
            {
                handler(null, new CustomEventArgs("Arrêté"));
            }

        }
        /// <summary>
        /// A chaque top...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrBasetime_Elapsed(object sender, System.EventArgs e)
        {
            if (workerNextTask != 0)
                return;
            if (workerDoneTask != 0)
                return;

            /* Next run in 250ms */
            tmrBasetime.Interval = 250;

            /* Polling */
            workerStart(WORK_FIND_CARD);
        }

     

        /// <summary>
        /// Lecture de carte de type miFare
        /// </summary>
        /// <returns></returns>
        private bool ReadCardMifare()
        {
            carte = new Carte();
            byte[] buffer = new byte[48];
            int i;
            short rc;
            
            EventHandler<CustomEventArgs> HandlerCarteLue = CarteLue;

            //Lecture du SNR de la carte
            for (i = 0; i < card_iso_a_snrlen; i++)
                carte.CardId += String.Format("{0:X02}", card_iso_a_snr[i]);

            //Lecture du secteur carte 4 permettant de savoir si c'est une carte AIPL
            rc = SPROX.MifStReadSector(null, 4, buffer, MIFARE_KEY_A);
            //La lecture est ok, le secteur n'est pas protégé, il se peut que ce soit une carte DEMOAIPL
            if (rc == SPROX.MI_OK)
            {
            //Lecture des données
                for (i = 0; i < 48; i++)
                {
                    if (buffer[i] == 0)
                        break;
                    carte.Type+= (char)buffer[i];
                }
                //C'est une carte AIPL
                if (carte.Type == "DEMOAIPL")
                {
                    // lecture du nom 32 octets Secteur 1 blocs 0 et 1 (adresses 4 et 5)
                    rc = SPROX.MifStReadSector(null, 0x1, buffer, MIFARE_KEY_A);
                    for (i = 0; i < 48; i++)
                    {
                        if (buffer[i] == 0)
                            break;
                        carte.Nom += (char)buffer[i];
                    }
                    // lecture du prénom 32 octets Secteur 2 blocs 0 et 1 (adresses 8 et 9)
                    rc = SPROX.MifStReadSector(null, 0x2, buffer, MIFARE_KEY_A);
                    for (i = 0; i < 48; i++)
                    {
                        if (buffer[i] == 0)
                            break;
                        carte.Prenom += (char)buffer[i];
                    }
                    // lecture du dossier 32 octets Secteur 3 blocs 0 et 1 (adresses 12 et 13)
                    rc = SPROX.MifStReadSector(null, 0x3, buffer, MIFARE_KEY_A);
                    for (i = 0; i < 48; i++)
                    {
                        if (buffer[i] == 0)
                            break;
                        carte.Dossier += (char)buffer[i];
                    }
                }
            }
            else
                //Le secteur n'est pas lisible, c'est peut-être une carte UL (lecture du secteur 15)
                //Format xxxxxxxxNom0000...Prenom00000...P ou E
            {
                // lecture du secteur 15
                rc = SPROX.MifStReadSector(null, 0xf, buffer, MIFARE_KEY_A);
                //Secteur 15 lisible, c'est peut-être une carte UL
                if (rc == SPROX.MI_OK)
                {
                    int index = 0;
                    

                    //Lecture de UIdUL sur 8 caracteres
                    for (i = 0; i < 7; i++)
                    {
                        if (buffer[i] == 0x0)
                        {
                             break;
                        }
                        carte.UidUL += (char)buffer[i];
                    }

                    index = 8;

                    ////Lecture des 00
                    //for (i = index; i < 48; i++)
                    //{
                    //    if (buffer[i] != 0x0)
                    //    {
                    //        index = i;
                    //        break;
                    //    }
                    //}
                    //Lecture du nom
                    for (i = index; i < 48; i++)
                    {
                        if (buffer[i] == 0x0)
                        {
                            index = i;
                            break;
                        }
                        carte.Nom += (char)buffer[i];
                    }
                    //Lecture des 00
                    for (i = index; i < 48; i++)
                    {
                        if (buffer[i] != 0x0)
                        {
                            index = i;
                            break;
                        }
                    }
                    //Lecture du prénom
                    for (i = index; i < 48; i++)
                    {
                        if (buffer[i] == 0x0)
                        {
                            index = i;
                            break;
                        }
                        carte.Prenom += (char)buffer[i];
                    }

                    //Lecture des 00
                    for (i = index; i < 48; i++)
                    {
                        if (buffer[i] != 0x0)
                        {
                            index = i;
                            break;
                        }
                    }

                    ////Lecture du code
                    //for (i = index; i < 48; i++)
                    //{
                    //    if (buffer[i] == 0x0)
                    //    {
                    //        index = i;
                    //        break;
                    //    }
                    //    carte.Prenom += (char)buffer[i];
                    //}

                    //Lecture du dernier caractere : P = Personnel
                    if ((char)buffer[index] == 'P')
                        carte.Role= "employee";
                    else
                        carte.Role = "student";

                    carte.Type = "UL";
                }                  
            }   
            
            //On déclenche l'évènement carteLue
            if (HandlerCarteLue != null)
            {
                HandlerCarteLue(null, new CustomEventArgs(carte.ToString()));
            }
            return true;
        }

        #endregion

        #region BackgroundWorker

        private Thread workerThread;
        private ManualResetEvent workerEvent;
        private bool workerAllowed;
        private bool workerRunning;
        private int workerNextTask;
        private int workerDoneTask;
        private bool workerResult;

        private const int WORK_NONE = 0;
        private const int WORK_OPEN_READER = 1;
        private const int WORK_FIND_CARD = 2;
        private const int WORK_READ_CARD = 3;
        private const int WORK_ERASE_CARD = 4;

        private bool OpenReaderTask()
        {
            short rc;

            if (isActif)
                return true;

            /* In case of a communcation error, we must close it before */
          
           SPROX.ReaderClose();

            /* Try to open the reader */
            rc = SPROX.ReaderOpen("");
            if (rc == SPROX.MI_OK)
            {
                isActif = true;
                SPROX.ControlLed(0, 0, 0);
            }
            return isActif;
        }

        private bool FindCardTask()
        {
            short rc;

            /* We must halt the card first (in case we were already connected) */
            if (card_is_tcl)
            {
                SPROX.Tcl_Deselect(0xFF);
            }
            else if (card_is_type_a)
            {
                SPROX.A_Halt();
            }
            else if (card_is_type_b)
            {
                SPROX.B_Halt(card_iso_b_atq);
            }

            card_is_type_a = false;
            card_is_type_b = false;
            card_is_tcl = false;

            if (SPROX.ControlRF(1) != SPROX.MI_OK)
            {
                /* Fatal error, this one shoul'd never fail ! */
                isActif = false;
                SPROX.ReaderClose();
                return false;
            }

            /* At first try to find an ISO-14443-A card */
            SPROX.SetConfig(SPROX.CFG_MODE_ISO_14443_A);

            card_iso_a_snrlen = 12;
            rc =
              SPROX.A_SelectAny(card_iso_a_atq, card_iso_a_snr,
                                ref card_iso_a_snrlen, card_iso_a_sak);

            if (rc == SPROX.MI_OK)
            {
                /* A card has been found */
                card_is_type = CARD_UNKNOWN;

                if ((card_iso_a_sak[0] & 0x20) != 0)
                {
                    /* Card support T=CL, try to activate it */
                    card_tcl_atslen = 32;
                    rc = SPROX.TclA_GetAts(0xFF, card_tcl_ats, ref card_tcl_atslen);
                    if (rc == SPROX.MI_OK)
                    {
                        /* Found a type A T=CL card */
                        card_is_type_a = true;
                        card_is_tcl = true;


                        return true;
                    }
                }
                else
                {
                    /* No T=CL support, but type A card found anyway */
                    card_is_type_a = true;

                    if ((card_iso_a_atq[1] == 0x00) && (card_iso_a_atq[0] == 0x04))
                    {
                        card_is_type = CARD_MIFARE_1K;
                    }
                    else
                        if ((card_iso_a_atq[1] == 0x00) && (card_iso_a_atq[0] == 0x02))
                        {
                            card_is_type = CARD_MIFARE_4K;
                        }
                        else
                            if ((card_iso_a_atq[1] == 0x00) && (card_iso_a_atq[0] == 0x44))
                            {
                                card_is_type = CARD_MIFARE_UL;
                            }

                    return true;
                }

                /* Type A card found but with error */
                goto failed;
            }

            /* Nox try to find an ISO-14443-B card */
            SPROX.SetConfig(SPROX.CFG_MODE_ISO_14443_B);

            rc = SPROX.B_SelectAny(0x00, card_iso_b_atq);

            if (rc == SPROX.MI_OK)
            {
                /* A card has been found */
                card_is_type = CARD_UNKNOWN;

                if ((card_iso_b_atq[10] & 0x01) != 0)
                {
                    /* Card support T=CL, try to activate it */
                    card_tcl_atslen = 32;
                    rc =
                      SPROX.TclB_Attrib(card_iso_b_atq, 0xFF, card_tcl_ats,
                                        ref card_tcl_atslen);
                    if (rc == SPROX.MI_OK)
                    {
                        /* Found a type B T=CL card */
                        card_is_type_b = true;
                        card_is_tcl = true;
                        return true;
                    }
                }
                else
                {
                    /* No T=CL support, but type Bcard found anyway */
                    card_is_type_b = true;
                    return true;
                }

                /* Type B card found but with error */
                goto failed;
            }

        failed:
            /* Power down the antenna to preserve batteries */
            if (SPROX.ControlRF(0) != SPROX.MI_OK)
            {
                /* Fatal error, this one shoul'd never fail ! */
                isActif = false;
                SPROX.ReaderClose();
            }
            return false;
        }

        /// <summary>
        /// Tache de lecture de carte
        /// </summary>
        /// <returns></returns>
        private bool ReadCardTask()
        {
            bool rc = false;
            ReadCardMifare();
            /* Power down the antenna to preserve batteries */
            if (SPROX.ControlRF(0) != SPROX.MI_OK)
            {
                /* Fatal error, this one shoul'd never fail ! */
                isActif = false;
                SPROX.ReaderClose();
                return false;
            }
            return rc;
        }

    

        /// <summary>
        /// Démarrage et gestion du cycle du lecteur
        /// Ouverture, lecture...
        /// </summary>
        private void workerLoop()
        {
            int task;
            bool rc;

            workerRunning = true;
            while (workerAllowed)
            {
                workerEvent.WaitOne();
                if (!workerAllowed)
                    break;
                task = workerNextTask;

                /* Until reader is found, the only allowed task is to open it... */
                if (!isActif)
                    task = WORK_OPEN_READER;

                switch (task)
                {
                    case WORK_OPEN_READER:
                        rc = OpenReaderTask();
  
                        break;
                    case WORK_FIND_CARD:
                        rc = FindCardTask();
                        break;
                    case WORK_READ_CARD:
                        rc = ReadCardTask();
                        break;


                    default:
                        rc = false;
                        break;
                }

                workerResult = rc;
                workerDoneTask = task;
                workerNextTask = 0;
                workerEvent.Reset();

                workerCallback();
            }
            workerRunning = false;
        }

       
        private void workerDone(int task, bool rc)
        {
            EventHandler<CustomEventArgs> eventStatusChanged = StatusChanged;

            
            switch (task)
            {
                case WORK_OPEN_READER:
                    if (rc)
                    {
                       
                        isActif = true;
                        EventHandler<CustomEventArgs> handler = StatusChanged;
                        // Event will be null if there are no subscribers
                        if (handler != null)
                        {
                            handler(null, new CustomEventArgs("Activé"));
                        }
                    }
                    else
                    {
                       // Console.WriteLine("Reader not available");
                        isActif = false;
                    }
                    break;

                case WORK_FIND_CARD:
                    if (rc)
                    {
                        switch (card_is_type)
                        {
                            case CARD_MIFARE_1K:
                                //Console.WriteLine("Reading Mifare 1k card");
                                break;
                            case CARD_MIFARE_4K:
                                //Console.WriteLine("Reading Mifare 4k card");
                                break;
                            //case CARD_DESFIRE:
                            //   Console.WriteLine("Reading Desfire card");
                            //    break;
                            default:
                                //Console.WriteLine("Unsupported card");
                                card_is_type = CARD_NONE;
                                break;
                        }

                        if (card_is_type != CARD_NONE)
                            workerStart(WORK_READ_CARD);
                    }
                    else
                    {
                        //Console.WriteLine("Put your card on the reader");
                    }
                    break;

                case WORK_READ_CARD:
                    //lbFingerprintOK.Visible = false;
                    //lbFingerprintKO.Visible = false;

                    if (rc)
                    {
                        eventStatusChanged(null, new CustomEventArgs("Lecture"));
                        //Console.WriteLine("Read success");

                        /* Next run in 5 seconds */
                        tmrBasetime.Interval = 5000;
                    }
                    else
                    {
                        //Console.WriteLine("Read failed");
                        /* Next run in 1,5 seconds */
                        tmrBasetime.Interval = 1500;
                    }
                    break;

                default:
                    //Console.WriteLine("???");
                    break;
            }
        }

        private void workerStart(int task)
        {
            workerDoneTask = 0;
            workerNextTask = task;
            workerEvent.Set();
        }


        private void workerCallback()
        {
            if (workerDoneTask != 0)
            {
                workerDone(workerDoneTask, workerResult);
                workerDoneTask = 0;
            }
        }
        #endregion 
    }
}
