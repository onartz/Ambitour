using Ambitour;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestAD
{
    
    
    /// <summary>
    ///Classe de test pour ServiceUHPTest, destinée à contenir tous
    ///les tests unitaires ServiceUHPTest
    ///</summary>
    [TestClass()]
    public class ServiceUHPTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        // 
        //Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        //Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test dans la classe
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Utilisez ClassCleanup pour exécuter du code après que tous les tests ont été exécutés dans une classe
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Test pour GetUtilisateursAD
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Ambitour.exe")]
        public void GetUtilisateursADTest()
        {
            string pNom = "Nartz"; // TODO: initialisez à une valeur appropriée
            string pPrenom = "Olivier"; // TODO: initialisez à une valeur appropriée
            string pEmployeeType = "employee"; // TODO: initialisez à une valeur appropriée
            Utilisateur u = new Utilisateur(pNom,pPrenom,"nartz5",pEmployeeType);
            List<Utilisateur> expected =new List<Utilisateur>();
            expected.Add(u);

            //List<Utilisateur> expected = new Utilisateur("Lacomba","Mathieu","lacomba1u","ET"); // TODO: initialisez à une valeur appropriée
            List<Utilisateur> actual;
            actual = ServiceUHP_Accessor.GetUtilisateursAD(pNom, pPrenom, pEmployeeType);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour GetStrUtilisateursAD
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Ambitour.exe")]
        public void GetStrUtilisateursADTest()
        {
            string pNom = "Lacomba"; // TODO: initialisez à une valeur appropriée
            string pPrenom = "Mathieu"; // TODO: initialisez à une valeur appropriée
            string pEmployeeType = "student"; // TODO: initialisez à une valeur appropriée
            List<string> expected = new List<string>(){"Lacomba Mathieu lacomba1u"}; // TODO: initialisez à une valeur appropriée
            List<string> actual;
            actual = ServiceUHP_Accessor.GetStrUtilisateursAD(pNom, pPrenom, pEmployeeType);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

 
    }
}
