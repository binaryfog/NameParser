using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryFog.NameParser.Tests
{
    /// <summary>
    /// Tests for 
    /// SR. John Henry William dela Vega, Jr Esq.
    /// MANUEL ESQUIPULAS SOHOM
    /// Maria Delores Esquivel-Moreno
    /// PHILIP DEHART ESQ
    /// DEHART, PHILIP
    /// john 'jack' kennedy
    /// john(jack) f kennedy
    /// kennedy, john(jack) f
    /// Mr.Jack Johnson, ESQ"
    /// Jose Miguel De La Vega
    /// </summary>
    [TestClass]
    public class ExtraCasesTests
    {
	    /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        [ExcludeFromCodeCoverage]
        public TestContext TestContext { get; set; }

	    #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Parse_DehartCommaSpacePhilip()
        {
            var fullName = "DeHart, Philip";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Philip", target.FirstName);
            Assert.AreEqual("DeHart", target.LastName);
            Assert.AreEqual("Philip DeHart", target.DisplayName);
            Assert.IsNull(target.Title);
        }

        [TestMethod]
        public void Parse_DehartCommaTwoSpacesPhilip()
        {
            var fullName = "DeHart,  Philip";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Philip", target.FirstName);
            Assert.AreEqual("DeHart", target.LastName);
            Assert.AreEqual("Philip DeHart", target.DisplayName);
            Assert.IsNull(target.Title);
        }

        [TestMethod]
        public void Parse_DehartCommaPhilip()
        {
            var fullName = "DeHart,Philip";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Philip", target.FirstName);
            Assert.AreEqual("DeHart", target.LastName);
            Assert.AreEqual("Philip DeHart", target.DisplayName);
            Assert.IsNull(target.Title);
        }

        [TestMethod]
        public void Parse_PhilipDeHartEsq()
        {
            var fullName = "Philip DeHart ESQ";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Philip", target.FirstName);
            Assert.AreEqual("DeHart", target.LastName);
            Assert.AreEqual("ESQ", target.Suffix);
            Assert.AreEqual("Philip DeHart", target.DisplayName);
            Assert.IsNull(target.Title);
        }

        [TestMethod]
        public void Parse_MrJackJohnsonEsq()
        {
            var fullName = @"Mr.Jack Johnson, ESQ""";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Mr.", target.Title);
            Assert.AreEqual("Jack", target.FirstName);
            Assert.AreEqual("Johnson", target.LastName);
            Assert.AreEqual(@"ESQ""", target.Suffix);
            Assert.AreEqual("Jack Johnson", target.DisplayName);
        }

        [TestMethod]
        public void Parse_PasqualeQuotePatQuoteJohnson()
        {
            var fullName = "Pasquale 'Pat' Johnson";
            var target = new FullNameParser(fullName);
            target.Parse();

            
            Assert.AreEqual("Pasquale", target.FirstName);
            Assert.AreEqual("Johnson", target.LastName);
            Assert.AreEqual("Pasquale Johnson", target.DisplayName);
            Assert.AreEqual("Pat", target.NickName);
        }

        [TestMethod]
        public void Parse_JoseMiguelDelaVega()
        {
            var fullName = "Jose Miguel de la Vega";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.AreEqual("Jose", target.FirstName);
            Assert.AreEqual("Miguel", target.MiddleName);
            Assert.AreEqual("de la Vega", target.LastName);
            Assert.AreEqual("Jose de la Vega", target.DisplayName);
        }

        //kennedy, john(jack) f
        [TestMethod]
        public void Parse_KennedyCommaJohnScopeJackScopeSpaceF()
        {
            var fullName = "Kennedy, John(Jack) F";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.AreEqual("John", target.FirstName);
            Assert.AreEqual("Kennedy", target.LastName);
            Assert.AreEqual("John Kennedy", target.DisplayName);
        }

        //kennedy, john(jack) f
        [TestMethod]
        public void Parse_JohnScopeJackScopeSpaceFSpaceKennedy()
        {
            var fullName = "John(Jack) F Kennedy";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.AreEqual("John", target.FirstName);
            Assert.AreEqual("Kennedy", target.LastName);
            Assert.AreEqual("John Kennedy", target.DisplayName);
        }

        //Maria Delores Esquivel-Moreno
        [TestMethod]
        public void Parse_MariaSpaceDeloresSpaceEsquivelDashMoreno()
        {
            var fullName = "Maria Delores Esquivel-Moreno";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.AreEqual("Maria", target.FirstName);
            Assert.AreEqual("Delores", target.MiddleName);
            Assert.AreEqual("Esquivel-Moreno", target.LastName);
            Assert.AreEqual("Maria Esquivel-Moreno", target.DisplayName);
        }

        //MANUEL ESQUIPULAS SOHOM
        [TestMethod]
        public void Parse_ManuelSpaceEsquipulasSpaceSohom()
        {
            var fullName = "Manuel Esquipulas Sohom";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.AreEqual("Manuel", target.FirstName);
            Assert.AreEqual("Esquipulas Sohom", target.LastName);
            Assert.AreEqual("Manuel Esquipulas Sohom", target.DisplayName);
        }

        //Tammy L. Baker
        [TestMethod]
        public void Parse_TammySpaceLDotBaker()
        {
            var fullName = "Tammy L. Baker";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.AreEqual("Tammy", target.FirstName);
            Assert.AreEqual("L", target.MiddleName);
            Assert.AreEqual("Baker", target.LastName);
            Assert.AreEqual("Tammy Baker", target.DisplayName);
        }

        //Tammy L. van Baker
        [TestMethod]
        public void Parse_TammySpaceLDotVanSpaceBaker()
        {
            var fullName = "Tammy L. van Baker";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.AreEqual("Tammy", target.FirstName);
            Assert.AreEqual("L", target.MiddleName);
            Assert.AreEqual("van Baker", target.LastName);
            Assert.AreEqual("Tammy van Baker", target.DisplayName);
        }

        //Tammy L. Blythe-Baker
        [TestMethod]
        public void Parse_TammySpaceLDotBlytheHyphenBaker()
        {
            var fullName = "Tammy L. Blythe-Baker";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.AreEqual("Tammy", target.FirstName);
            Assert.AreEqual("L", target.MiddleName);
            Assert.AreEqual("Blythe-Baker", target.LastName);
            Assert.AreEqual("Tammy Blythe-Baker", target.DisplayName);
        }

        //Jimmy Lee Dabney II
        [TestMethod]
        public void Parse_JimmySpaceLeeSpaceDabneySpaceII()
        {
            var fullName = "Jimmy Lee Dabney II";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.AreEqual("Jimmy", target.FirstName);
            Assert.AreEqual("Lee", target.MiddleName);
            Assert.AreEqual("Dabney", target.LastName);
            Assert.AreEqual("II", target.Suffix);
            Assert.AreEqual("Jimmy Dabney", target.DisplayName);
        }

        //Tammy L. Baker II
        [TestMethod]
        public void Parse_TammySpaceLDotBakerSpaceII()
        {
            var fullName = "Tammy L. Baker II";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.AreEqual("Tammy", target.FirstName);
            Assert.AreEqual("L", target.MiddleName);
            Assert.AreEqual("Baker", target.LastName);
            Assert.AreEqual("II", target.Suffix);
            Assert.AreEqual("Tammy Baker", target.DisplayName);
        }

        //Tammy L. Blythe-Baker II
        [TestMethod]
        public void Parse_TammySpaceLDotBlytheHyphenBakerSpaceII()
        {
            var fullName = "Tammy L. Blythe-Baker II";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.AreEqual("Tammy", target.FirstName);
            Assert.AreEqual("L", target.MiddleName);
            Assert.AreEqual("Blythe-Baker", target.LastName);
            Assert.AreEqual("II", target.Suffix);
            Assert.AreEqual("Tammy Blythe-Baker", target.DisplayName);
        }

        //Tammy L. van Baker II
        [TestMethod]
        public void Parse_TammySpaceLDotVanSpaceBakerSpaceII()
        {
            var fullName = "Tammy L. van Baker II";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.AreEqual("Tammy", target.FirstName);
            Assert.AreEqual("L", target.MiddleName);
            Assert.AreEqual("van Baker", target.LastName);
            Assert.AreEqual("II", target.Suffix);
            Assert.AreEqual("Tammy van Baker", target.DisplayName);
        }

        /* No, you can not have a title of SR.
        //SR. John Henry William dela Vega, Jr Esq.
        [TestMethod()]
        public void Parse_SrJohnHenryWilliamdelaVegaCommaJrEsq()
        {
            string fullName = "SR. John Henry William dela Vega, Jr Esq.";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("SR.", target.Title);
            Assert.AreEqual("John", target.FirstName);
            Assert.AreEqual("Henry William", target.MiddleName);
            Assert.AreEqual("dela Vega", target.LastName);
            Assert.AreEqual("Jr Esq.", target.Suffix);
            Assert.AreEqual("John dela Vega", target.DisplayName);
        }
		*/
    }
}
