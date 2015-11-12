using System;
using System.Text;
using System.Collections.Generic;
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
        public ExtraCasesTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
            string fullName = "DeHart, Philip";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Philip", target.FirstName);
            Assert.AreEqual("DeHart", target.LastName);
            Assert.AreEqual("Philip DeHart", target.DisplayName);
            Assert.IsNull(target.Title);
        }

        [TestMethod]
        public void Parse_DehartCommaTwoSpacesPhilip()
        {
            string fullName = "DeHart,  Philip";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Philip", target.FirstName);
            Assert.AreEqual("DeHart", target.LastName);
            Assert.AreEqual("Philip DeHart", target.DisplayName);
            Assert.IsNull(target.Title);
        }

        [TestMethod]
        public void Parse_DehartCommaPhilip()
        {
            string fullName = "DeHart,Philip";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Philip", target.FirstName);
            Assert.AreEqual("DeHart", target.LastName);
            Assert.AreEqual("Philip DeHart", target.DisplayName);
            Assert.IsNull(target.Title);
        }

        [TestMethod]
        public void Parse_PhilipDeHartEsq()
        {
            string fullName = "Philip DeHart ESQ";
            FullNameParser target = new FullNameParser(fullName);
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
            string fullName = @"Mr.Jack Johnson, ESQ""";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Mr.", target.Title);
            Assert.AreEqual("Jack", target.FirstName);
            Assert.AreEqual("Johnson", target.LastName);
            Assert.AreEqual(@"ESQ""", target.Suffix);
            Assert.AreEqual("Jack Johnson", target.DisplayName);
        }

        [TestMethod()]
        public void Parse_PasqualeQuotePatQuoteJohnson()
        {
            string fullName = "Pasquale 'Pat' Johnson";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            
            Assert.AreEqual("Pasquale", target.FirstName);
            Assert.AreEqual("Johnson", target.LastName);
            Assert.AreEqual("Pasquale Johnson", target.DisplayName);
            Assert.AreEqual("Pat", target.NickName);
        }
    }
}
