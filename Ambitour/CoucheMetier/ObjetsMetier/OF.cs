using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Ambitour.CoucheMetier.ObjetsMetier
{
    [Serializable]
    public class OF
    {
        Guid id;
        int qty;
        DateTime dateDue;
        int scrappedQty;
        string scrapReason;
        int realizedQty;

        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        public DateTime DateDue
        {
            get { return dateDue; }
            set { dateDue = value; }
        }
        DateTime dateStarted;

        public DateTime DateStarted
        {
            get { return dateStarted; }
            set { dateStarted = value; }
        }
        DateTime dateEnded;

        public DateTime DateEnded
        {
            get { return dateEnded; }
            set { dateEnded = value; }
        }
        int productId;

        public int RealizedQty
        {
            get { return realizedQty; }
            set { realizedQty = value; }
        }

        public int ScrappedQty
        {
            get { return scrappedQty; }
            set { scrappedQty = value; }
        }

        public string ScrapReason
        {
            get { return scrapReason; }
            set { scrapReason = value; }
        }



        public void Start()
        {
            dateStarted = System.DateTime.Now;
        }

        public void Stop()
        {
            dateEnded = System.DateTime.Now;
        }

        public bool isStarted()
        {
            return dateStarted != null;
        }

        public bool isEnded()
        {
            return dateEnded != null;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public OF()
        {
            id = Guid.NewGuid();
            productId = -1;
            qty = 0;
            scrappedQty = 0;
            scrapReason = "";
           
        }

        /// <summary>
        /// Static method to generate an OF to produce  products of type 6
        /// </summary>
        /// <returns>a new OF</returns>
        public static OF Generate()
        {
            OF of = new OF();
            of.productId = 6;
            of.dateDue = DateTime.Now;
            Random r = new Random();
            of.qty = r.Next(5,20);
              
            return of;
        }

      /// <summary>
      /// Static method to generate an OF to produce  products of desired type
      /// </summary>
      /// <param name="productId">ProductId</param>
      /// <returns></returns>
        public static OF Generate(int productId)
        {
            OF of = OF.Generate();
            of.productId = productId;      
            return of;
        }


        /// <summary>
        /// Save a new OF in a particular directory
        /// </summary>
        /// <param name="of">Of to save</param>
        /// <param name="path">directory</param>
        public static void Save(OF of, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(OF));

            TextWriter tw = new StreamWriter(Path.Combine(path, of.Id.ToString() + ".xml"));
            serializer.Serialize(tw, of);
            tw.Close();
        }

       

        /// <summary>
        /// Static méthod to get an of object from a file
        /// </summary>
        /// <param name="path">file containing the of</param>
        /// <returns>Of</returns>
        public static OF Load(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(OF));
            TextReader tr = new StreamReader(path);
            OF of = (OF)serializer.Deserialize(tr);
            tr.Close();
            return of;
        }

        /// <summary>
        /// Static method to retrieve OFs from a directory
        /// </summary>
        /// <param name="path">directory where ofs are</param>
        /// <returns>A list of Ofs</returns>
        public static List<OF> GetFromDir(string path)
        {
            List<OF> ofs = new List<OF>();
            string[] filePaths = Directory.GetFiles(path);
            foreach (string s in filePaths)
            {
                try
                {
                    OF of = Load(s);
                    ofs.Add(of);
                    FileInfo fi = new FileInfo(s);
                    string fileName = fi.Name;
                    string directory = fi.DirectoryName;
                    string newFilePath = Path.Combine(directory, @"Archives", fileName);
                    if (File.Exists(newFilePath))
                    {
                        File.Delete(newFilePath);
                    }

                    File.Move(s, newFilePath);
                }
                catch (IOException ex)
                {
                    //break;
                }

            }
            return ofs;
        }
    }
}
