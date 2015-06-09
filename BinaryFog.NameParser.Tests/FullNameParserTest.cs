using BinaryFog.NameParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BinaryFog.NameParser.Tests
{
    
    
    /// <summary>
    ///This is a test class for FullNameParserTest and is intended
    ///to contain all FullNameParserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FullNameParserTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod()]
        public void Parse_JackJohnson()
        {
            string fullName = "Jack Johnson"; 
            FullNameParser target = new FullNameParser(fullName); 
            target.Parse();

            Assert.AreEqual("Jack", target.FirstName);
            Assert.AreEqual("Johnson", target.LastName);
            Assert.AreEqual("Jack Johnson", target.DisplayName);
            Assert.IsNull(target.Title);
        }

        [TestMethod()]
        public void Parse_MrDotJackJohnson()
        {
            string fullName = "Mr. Jack Johnson";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Jack", target.FirstName);
            Assert.AreEqual("Johnson", target.LastName);
            Assert.AreEqual("Jack Johnson", target.DisplayName);
            Assert.AreEqual("Mr.", target.Title);
        }

        [TestMethod()]
        public void Parse_MrJackJohnson()
        {
            string fullName = "Mr Jack Johnson";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Jack", target.FirstName);
            Assert.AreEqual("Johnson", target.LastName);
            Assert.AreEqual("Jack Johnson", target.DisplayName);
            Assert.AreEqual("Mr", target.Title);
        }

        [TestMethod()]
        public void Parse_MrJackJohnsonJrDOT()
        {
            string fullName = "Mr Jack Johnson Jr.";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Jack", target.FirstName);
            Assert.AreEqual("Johnson", target.LastName);
            Assert.AreEqual("Jack Johnson", target.DisplayName);
            Assert.AreEqual("Mr", target.Title);
        }

        [TestMethod()]
        public void Parse_MrJayJPositano()
        {
            string fullName = "Mr Jay J Positano";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Jay", target.FirstName);
            Assert.AreEqual("Positano", target.LastName);
            Assert.AreEqual("Jay Positano", target.DisplayName);
            Assert.AreEqual("Mr", target.Title);
        }

        [TestMethod()]
        public void Parse_MrJayJDOTPositano()
        {
            string fullName = "Mr Jay J. Positano";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Jay", target.FirstName);
            Assert.AreEqual("Positano", target.LastName);
            Assert.AreEqual("Jay Positano", target.DisplayName);
            Assert.AreEqual("Mr", target.Title);
        }

        [TestMethod()]
        public void Parse_MrJackJohnsonJr()
        {
            string fullName = "Mr Jack Johnson Jr";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Jack", target.FirstName);
            Assert.AreEqual("Johnson", target.LastName);
            Assert.AreEqual("Jack Johnson", target.DisplayName);
            Assert.AreEqual("Mr", target.Title);
        }

        [TestMethod()]
        public void Parse_AffiliatedForkliftServices()
        {
            string fullName = "AFFILIATED FORKLIFT SERVICES";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("AFFILIATED FORKLIFT SERVICES", target.DisplayName);
            Assert.IsNull(target.FirstName);
            Assert.IsNull(target.LastName);
            Assert.IsNull(target.Title);
            Assert.IsNull(target.NickName);
        }


        [TestMethod()]
        public void Parse_AkContractingSCOPEKenoraSCOPELtd()
        {
            string fullName = "AK CONTRACTING (KENORA) LTD.";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("AK CONTRACTING (KENORA) LTD.", target.DisplayName);

        }

        [TestMethod()]
        public void Parse_PasqualeSCOPEPatSCOPEJohnson()
        {
            string fullName = "Pasquale (Pat) Johnson";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Pasquale", target.FirstName);
            Assert.AreEqual("Johnson", target.LastName);
            Assert.AreEqual("Pasquale Johnson", target.DisplayName);
            Assert.AreEqual("Pat", target.NickName);
        }

        [TestMethod()]
        public void Parse_MrDOTPasqualeSCOPEPatSCOPEJohnson()
        {
            string fullName = "Mr. Pasquale (Pat) Johnson";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Mr.", target.Title);
            Assert.AreEqual("Pasquale", target.FirstName);
            Assert.AreEqual("Johnson", target.LastName);
            Assert.AreEqual("Pasquale Johnson", target.DisplayName);
            Assert.AreEqual("Pat", target.NickName);
        }

        [TestMethod()]
        public void Parse_MrDOTPasqualeSCOPEPatSCOPEJohnsonJr()
        {
            string fullName = "Mr. Pasquale (Pat) Johnson Jr";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Mr.", target.Title);
            Assert.AreEqual("Pasquale", target.FirstName);
            Assert.AreEqual("Johnson", target.LastName);
            Assert.AreEqual("Pasquale Johnson", target.DisplayName);
            Assert.AreEqual("Pat", target.NickName);
            Assert.AreEqual("Jr", target.Suffix);
        }

        [TestMethod()]
        public void Parse_CompanyNamesAsPersonNames()
        {
            string[] companyNamesAsPersonNames = new string[] { "AL HUGHES (MARINE)", "HI TECH HYDRAULICS (1985) LT", "ALFALFA BEEKEEPERS LTD",
            "ALAA SALAH   AELSAYAD@TORCC."};

            foreach (var item in companyNamesAsPersonNames)
            {
                Console.WriteLine(item);
                string fullName = item;
                FullNameParser target = new FullNameParser(fullName);
                target.Parse();

                Assert.AreEqual(fullName, target.DisplayName);
                Assert.IsNull(target.FirstName);
                Assert.IsNull(target.LastName);
                Assert.IsNull(target.Title);
                Assert.IsNull(target.NickName);
            }
        }


        [TestMethod()]
        public void Parse_ATTNMrEricKing()
        {
                string fullName = "ATTN: MR. ERIC KING";
                FullNameParser target = new FullNameParser(fullName);
                target.Parse();

                Assert.AreEqual("ERIC KING", target.DisplayName);
                Assert.AreEqual("ERIC", target.FirstName);
                Assert.AreEqual("KING", target.LastName);
                Assert.AreEqual("MR.", target.Title);
            
        }

        [TestMethod()]
        public void Parse_Catalin()
        {
            string fullName = "Catalin";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Catalin", target.FirstName);
            

        }

        [TestMethod()]
        public void Parse_Arroyo()
        {
            string fullName = "Arroyo";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Arroyo", target.LastName);


        }

        [TestMethod()]
        public void Parse_MrGiocomoVanExan()
        {
            string fullName = "Mr Giocomo Van Exan";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Mr", target.Title);
            Assert.AreEqual("Giocomo", target.FirstName);
            Assert.AreEqual("Van Exan", target.LastName);


        }

        [TestMethod()]
        public void Parse_GiovanniVanDerHutte()
        {
            string fullName = "Giovanni Van Der Hutte";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Giovanni", target.FirstName);
            Assert.AreEqual("Van Der Hutte", target.LastName);


        }

        [TestMethod()]
        public void Parse_MsSandyAccountsReceivable()
        {
            string fullName = "Ms Sandy Accounts Receivable";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Ms", target.Title);
            Assert.AreEqual("Sandy", target.FirstName);
            Assert.AreEqual("Sandy Accounts Receivable", target.DisplayName);


        }

        [TestMethod()]
        public void Parse_SandyAccountsReceivable()
        {
            string fullName = "Sandy Accounts Receivable";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Sandy", target.FirstName);
        }

    }
}
