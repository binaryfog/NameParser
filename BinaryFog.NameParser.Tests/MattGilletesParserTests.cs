using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryFog.NameParser.Tests {


	/// <summary>
	///This is a test class for FullNameParserTest and is intended
	///to contain all FullNameParserTest Unit Tests
	///</summary>
	[TestClass]
	public class MattGilletesParserTests
    {
		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		[ExcludeFromCodeCoverage]
		public TestContext TestContext { get; set; }

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


		[TestMethod]
		public void Parse_IfoEkpreDASHOlomu() {
			var fullName = "Ifo Ekpre-Olomu";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Ifo", target.FirstName);
			Assert.AreEqual("Ekpre-Olomu", target.LastName);
			Assert.AreEqual("Ifo Ekpre-Olomu", target.DisplayName);
			
		}

        [TestMethod]
        public void Parse_IsaAbdulDASHQuddus()
        {
            var fullName = "Isa Abdul-Quddus";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Isa", target.FirstName);
            Assert.AreEqual("Abdul-Quddus", target.LastName);
            Assert.AreEqual("Isa Abdul-Quddus", target.DisplayName);

        }

        [TestMethod]
        public void Parse_EDOTJDOTSPACEManuel()
        {
            var fullName = "E.J. Manuel";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("E.J.", target.FirstName);
            Assert.AreEqual("Manuel", target.LastName);
            Assert.AreEqual("E.J. Manuel", target.DisplayName);

        }

        [TestMethod]
        public void Parse_DDOTSpaceJDotSpaceFoster()
        {
            var fullName = "D. J. Foster";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("D.J.", target.FirstName);
            Assert.AreEqual("Foster", target.LastName);
            Assert.AreEqual("D.J. Foster", target.DisplayName);

        }

        [TestMethod]
        public void Parse_AlDASHHajjSPACEShabazz()
        {
            var fullName = "Al-Hajj Shabazz";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Al-Hajj", target.FirstName);
            Assert.AreEqual("Shabazz", target.LastName);
            Assert.AreEqual("Al-Hajj Shabazz", target.DisplayName);

        }

    }
}
