using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ambitour
{
    public class Fichier
    {
        private List<String> listLine;
        private string fileName;      

        public Fichier(string pFileName)
        {
            listLine = new List<string>();
            fileName = pFileName;
            readAllLines();
        }

        public List<String> getAllLines()
        {
            readAllLines();
            return listLine;
        }

        public void deleteLine(int lineNumber)
        {
            try
            {
                listLine.RemoveAt(lineNumber);
            }
            catch (IndexOutOfRangeException)
            { }
            deleteFile();
            writeAllLines();
        }

        private void readAllLines()
        {
            if (File.Exists(fileName))
            {
                string line;
                using (StreamReader file = new StreamReader(fileName))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        listLine.Add(line);
                    }
                }
            }
        }


        private void writeAllLines()
        {
            using (TextWriter file = new StreamWriter(fileName, true))
            {
                foreach (string line in listLine)
                {
                    file.WriteLine(line);
                }
            }
        }

        private void deleteFile()
        {
            File.Delete(fileName);
        }
    }
}
