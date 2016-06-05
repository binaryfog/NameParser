using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryFog.NameParser.Tests {


	/// <summary>
	///This is a test class for FullNameParserTest and is intended
	///to contain all FullNameParserTest Unit Tests
	///</summary>
	[TestClass]
	public class FullNameParserTest {
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


		/// <summary>
		///A test for Parse
		///</summary>
		[TestMethod]
		public void Parse_JackJohnson() {
			var fullName = "Jack Johnson";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Jack", target.FirstName);
			Assert.AreEqual("Johnson", target.LastName);
			Assert.AreEqual("Jack Johnson", target.DisplayName);
			Assert.IsNull(target.Title);
		}

		[TestMethod]
		public void Parse_MrDotJackJohnson() {
			var fullName = "Mr. Jack Johnson";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Jack", target.FirstName);
			Assert.AreEqual("Johnson", target.LastName);
			Assert.AreEqual("Jack Johnson", target.DisplayName);
			Assert.AreEqual("Mr.", target.Title);
		}

		[TestMethod]
		public void Parse_MrJackJohnson() {
			var fullName = "Mr Jack Johnson";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Jack", target.FirstName);
			Assert.AreEqual("Johnson", target.LastName);
			Assert.AreEqual("Jack Johnson", target.DisplayName);
			Assert.AreEqual("Mr", target.Title);
		}

		[TestMethod]
		public void Parse_MrJackJohnsonJrDOT() {
			var fullName = "Mr Jack Johnson Jr.";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Jack", target.FirstName);
			Assert.AreEqual("Johnson", target.LastName);
			Assert.AreEqual("Jack Johnson", target.DisplayName);
			Assert.AreEqual("Mr", target.Title);
		}

		[TestMethod]
		public void Parse_MrJayJPositano() {
			var fullName = "Mr Jay J Positano";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Jay", target.FirstName);
			Assert.AreEqual("Positano", target.LastName);
			Assert.AreEqual("Jay Positano", target.DisplayName);
			Assert.AreEqual("Mr", target.Title);
		}

		[TestMethod]
		public void Parse_MrJayJDOTPositano() {
			var fullName = "Mr Jay J. Positano";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Jay", target.FirstName);
			Assert.AreEqual("Positano", target.LastName);
			Assert.AreEqual("Jay Positano", target.DisplayName);
			Assert.AreEqual("Mr", target.Title);
		}

		[TestMethod]
		public void Parse_MrJackJohnsonJr() {
			var fullName = "Mr Jack Johnson Jr";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Jack", target.FirstName);
			Assert.AreEqual("Johnson", target.LastName);
			Assert.AreEqual("Jack Johnson", target.DisplayName);
			Assert.AreEqual("Mr", target.Title);
		}

		[TestMethod]
		public void Parse_AffiliatedForkliftServices() {
			var fullName = "AFFILIATED FORKLIFT SERVICES";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("AFFILIATED FORKLIFT SERVICES", target.DisplayName);
			Assert.IsNull(target.FirstName);
			Assert.IsNull(target.LastName);
			Assert.IsNull(target.Title);
			Assert.IsNull(target.NickName);
		}


		[TestMethod]
		public void Parse_AkContractingSCOPEKenoraSCOPELtd() {
			var fullName = "AK CONTRACTING (KENORA) LTD.";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("AK CONTRACTING (KENORA) LTD.", target.DisplayName);

		}

		[TestMethod]
		public void Parse_PasqualeSCOPEPatSCOPEJohnson() {
			var fullName = "Pasquale (Pat) Johnson";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Pasquale", target.FirstName);
			Assert.AreEqual("Johnson", target.LastName);
			Assert.AreEqual("Pasquale Johnson", target.DisplayName);
			Assert.AreEqual("Pat", target.NickName);
		}

		[TestMethod]
		public void Parse_MrDOTPasqualeSCOPEPatSCOPEJohnson() {
			var fullName = "Mr. Pasquale (Pat) Johnson";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Mr.", target.Title);
			Assert.AreEqual("Pasquale", target.FirstName);
			Assert.AreEqual("Johnson", target.LastName);
			Assert.AreEqual("Pasquale Johnson", target.DisplayName);
			Assert.AreEqual("Pat", target.NickName);
		}

		[TestMethod]
		public void Parse_MrDOTPasqualeSCOPEPatSCOPEJohnsonJr() {
			var fullName = "Mr. Pasquale (Pat) Johnson Jr";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Mr.", target.Title);
			Assert.AreEqual("Pasquale", target.FirstName);
			Assert.AreEqual("Johnson", target.LastName);
			Assert.AreEqual("Pasquale Johnson", target.DisplayName);
			Assert.AreEqual("Pat", target.NickName);
			Assert.AreEqual("Jr", target.Suffix);
		}

		[TestMethod]
		public void Parse_CompanyNamesAsPersonNames() {
			string[] companyNamesAsPersonNames = { "AL HUGHES (MARINE)", "HI TECH HYDRAULICS (1985) LT", "ALFALFA BEEKEEPERS LTD",
			"ALAA SALAH   AELSAYAD@TORCC."};

			foreach (var item in companyNamesAsPersonNames) {
				Console.WriteLine(item);
				var fullName = item;
				var target = new FullNameParser(fullName);
				target.Parse();

				Assert.AreEqual(fullName, target.DisplayName);
				Assert.IsNull(target.FirstName);
				Assert.IsNull(target.LastName);
				Assert.IsNull(target.Title);
				Assert.IsNull(target.NickName);
			}
		}


		[TestMethod]
		public void Parse_ATTNMrEricKing() {
			var fullName = "ATTN: MR. ERIC KING";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("ERIC KING", target.DisplayName);
			Assert.AreEqual("ERIC", target.FirstName);
			Assert.AreEqual("KING", target.LastName);
			Assert.AreEqual("MR.", target.Title);

		}

		[TestMethod]
		public void Parse_Catalin() {
			var fullName = "Catalin";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Catalin", target.FirstName);


		}

		[TestMethod]
		public void Parse_Arroyo() {
			var fullName = "Arroyo";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Arroyo", target.LastName);


		}

		[TestMethod]
		public void Parse_MrGiocomoVanExan() {
			var fullName = "Mr Giocomo Van Exan";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Mr", target.Title);
			Assert.AreEqual("Giocomo", target.FirstName);
			Assert.AreEqual("Van Exan", target.LastName);


		}

		[TestMethod]
		public void Parse_GiovanniVanDerHutte() {
			var fullName = "Giovanni Van Der Hutte";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Giovanni", target.FirstName);
			Assert.AreEqual("Van Der Hutte", target.LastName);


		}

		[TestMethod]
		public void Parse_MsSandyAccountsReceivable() {
			var fullName = "Ms Sandy Accounts Receivable";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Ms", target.Title);
			Assert.AreEqual("Sandy", target.FirstName);
			Assert.AreEqual("Sandy Accounts Receivable", target.DisplayName);


		}

		[TestMethod]
		public void Parse_SandyAccountsReceivable() {
			var fullName = "Sandy Accounts Receivable";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Sandy", target.FirstName);
		}

		[TestMethod]
		public void Parse_MrJackFrancisVanDerWaalSr() {
			var fullName = "Mr. Jack Francis Van Der Waal Sr.";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Mr.", target.Title);
			Assert.AreEqual("Jack", target.FirstName);
			Assert.AreEqual("Francis", target.MiddleName);
			Assert.AreEqual("Van Der Waal", target.LastName);
			Assert.AreEqual("Sr.", target.Suffix);
		}

		[TestMethod]
		public void Parse_MrJackFrancisMarionVanDerWaalSr() {
			var fullName = "Mr. Jack Francis Marion Van Der Waal Sr.";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual("Mr.", target.Title);
			Assert.AreEqual("Jack", target.FirstName);
			Assert.AreEqual("Francis Marion", target.MiddleName);
			Assert.AreEqual("Van Der Waal", target.LastName);
			Assert.AreEqual("Sr.", target.Suffix);
		}


		[TestMethod]
		public void Parse_Null() {
			string fullName = null;
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual(null, target.Title);
			Assert.AreEqual(null, target.FirstName);
			Assert.AreEqual(null, target.MiddleName);
			Assert.AreEqual(null, target.LastName);
			Assert.AreEqual(null, target.Suffix);
		}


		[TestMethod]
		public void Parse_Blank() {
			var fullName = "";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual(null, target.Title);
			Assert.AreEqual(null, target.FirstName);
			Assert.AreEqual(null, target.MiddleName);
			Assert.AreEqual(null, target.LastName);
			Assert.AreEqual(null, target.Suffix);
		}


		[TestMethod]
		public void Parse_Space() {
			var fullName = " ";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.AreEqual(null, target.Title);
			Assert.AreEqual(null, target.FirstName);
			Assert.AreEqual(null, target.MiddleName);
			Assert.AreEqual(null, target.LastName);
			Assert.AreEqual(null, target.Suffix);
		}

		[TestMethod]
		public void Parse_Static() {
			var fullName = "";
			var target = FullNameParser.Parse(fullName);

			Assert.IsNotNull(target);
		}
		


	}
}
