using System;
using Xunit;

namespace BinaryFog.NameParser.Tests {
	/// <summary>
	///This is a test class for FullNameParserTest and is intended
	///to contain all FullNameParserTest Unit Tests
	///</summary>
	public class FullNameParserTest {
		/// <summary>
		///A test for Parse
		///</summary>
		[Fact]
		public void Parse_JackJohnson() {
			var fullName = "Jack Johnson";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Jack", target.FirstName);
			Assert.Equal("Johnson", target.LastName);
			Assert.Equal("Jack Johnson", target.DisplayName);
			Assert.Null(target.Title);
		}

		[Fact]
		public void Parse_MrDotJackJohnson() {
			var fullName = "Mr. Jack Johnson";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Jack", target.FirstName);
			Assert.Equal("Johnson", target.LastName);
			Assert.Equal("Jack Johnson", target.DisplayName);
			Assert.Equal("Mr.", target.Title);
		}

		[Fact]
		public void Parse_MrJackJohnson() {
			var fullName = "Mr Jack Johnson";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Jack", target.FirstName);
			Assert.Equal("Johnson", target.LastName);
			Assert.Equal("Jack Johnson", target.DisplayName);
			Assert.Equal("Mr", target.Title);
		}

		[Fact]
		public void Parse_MrJackJohnsonJrDOT() {
			var fullName = "Mr Jack Johnson Jr.";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Jack", target.FirstName);
			Assert.Equal("Johnson", target.LastName);
			Assert.Equal("Jack Johnson", target.DisplayName);
			Assert.Equal("Mr", target.Title);
		}

		[Fact]
		public void Parse_MrJayJPositano() {
			var fullName = "Mr Jay J Positano";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Jay", target.FirstName);
			Assert.Equal("Positano", target.LastName);
			Assert.Equal("Jay Positano", target.DisplayName);
			Assert.Equal("Mr", target.Title);
		}

		[Fact]
		public void Parse_MrJayJDOTPositano() {
			var fullName = "Mr Jay J. Positano";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Jay", target.FirstName);
			Assert.Equal("Positano", target.LastName);
			Assert.Equal("Jay Positano", target.DisplayName);
			Assert.Equal("Mr", target.Title);
		}

		[Fact]
		public void Parse_MrJackJohnsonJr() {
			var fullName = "Mr Jack Johnson Jr";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Mr", target.Title);
			Assert.Equal("Jack", target.FirstName);
			Assert.Equal("Johnson", target.LastName);
			Assert.Equal("Jack Johnson", target.DisplayName);

		}

		[Fact]
		public void Parse_AffiliatedForkliftServices() {
			var fullName = "AFFILIATED FORKLIFT SERVICES";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("AFFILIATED FORKLIFT SERVICES", target.DisplayName);
			Assert.Null(target.FirstName);
			Assert.Null(target.LastName);
			Assert.Null(target.Title);
			Assert.Null(target.NickName);
		}


		[Fact]
		public void Parse_AkContractingSCOPEKenoraSCOPELtd() {
			var fullName = "AK CONTRACTING (KENORA) LTD.";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("AK CONTRACTING (KENORA) LTD.", target.DisplayName);
		}

		[Fact]
		public void Parse_PasqualeSCOPEPatSCOPEJohnson() {
			var fullName = "Pasquale (Pat) Johnson";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Pasquale", target.FirstName);
			Assert.Equal("Johnson", target.LastName);
			Assert.Equal("Pasquale Johnson", target.DisplayName);
			Assert.Equal("Pat", target.NickName);
		}

		[Fact]
		public void Parse_MrDOTPasqualeSCOPEPatSCOPEJohnson() {
			var fullName = "Mr. Pasquale (Pat) Johnson";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Mr.", target.Title);
			Assert.Equal("Pasquale", target.FirstName);
			Assert.Equal("Johnson", target.LastName);
			Assert.Equal("Pasquale Johnson", target.DisplayName);
			Assert.Equal("Pat", target.NickName);
		}

		[Fact]
		public void Parse_MrDOTPasqualeSCOPEPatSCOPEJohnsonJr() {
			var fullName = "Mr. Pasquale (Pat) Johnson Jr";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Mr.", target.Title);
			Assert.Equal("Pasquale", target.FirstName);
			Assert.Equal("Johnson", target.LastName);
			Assert.Equal("Pasquale Johnson", target.DisplayName);
			Assert.Equal("Pat", target.NickName);
			Assert.Equal("Jr", target.Suffix);
		}

		[Fact]
		public void Parse_CompanyNamesAsPersonNames() {
			string[] companyNamesAsPersonNames = {
				"AL HUGHES (MARINE)", "HI TECH HYDRAULICS (1985) LT", "ALFALFA BEEKEEPERS LTD",
				"ALAA SALAH   AELSAYAD@TORCC."
			};

			foreach (var item in companyNamesAsPersonNames) {
				//Console.WriteLine(item);
				var fullName = item;
				var target = new FullNameParser(fullName);
				target.Parse();

				Assert.Equal(fullName, target.DisplayName);
				Assert.Null(target.FirstName);
				Assert.Null(target.LastName);
				Assert.Null(target.Title);
				Assert.Null(target.NickName);
			}
		}


		[Fact]
		public void Parse_ATTNMrEricKing() {
			var fullName = "ATTN: MR. ERIC KING";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("ERIC KING", target.DisplayName);
			Assert.Equal("ERIC", target.FirstName);
			Assert.Equal("KING", target.LastName);
			Assert.Equal("MR.", target.Title);
		}

		[Fact]
		public void Parse_Catalin() {
			var fullName = "Catalin";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Catalin", target.FirstName);
		}

		[Fact]
		public void Parse_Arroyo() {
			var fullName = "Arroyo";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Arroyo", target.LastName);
		}

		[Fact]
		public void Parse_MrGiocomoVanExan() {
			var fullName = "Mr Giocomo Van Exan";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Mr", target.Title);
			Assert.Equal("Giocomo", target.FirstName);
			Assert.Equal("Van Exan", target.LastName);
		}

		[Fact]
		public void Parse_GiovanniVanDerHutte() {
			var fullName = "Giovanni Van Der Hutte";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Giovanni", target.FirstName);
			Assert.Equal("Van Der Hutte", target.LastName);
		}

		[Fact]
		public void Parse_MsSandyAccountsReceivable() {
			var fullName = "Ms Sandy Accounts Receivable";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Ms", target.Title);
			Assert.Equal("Sandy", target.FirstName);
			Assert.Equal("Sandy Accounts Receivable", target.DisplayName);
		}

		[Fact]
		public void Parse_SandyAccountsReceivable() {
			var fullName = "Sandy Accounts Receivable";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Sandy", target.FirstName);
		}

		[Fact]
		public void Parse_MrJackFrancisVanDerWaalSr() {
			var fullName = "Mr. Jack Francis Van Der Waal Sr.";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Mr.", target.Title);
			Assert.Equal("Jack", target.FirstName);
			Assert.Equal("Francis", target.MiddleName);
			Assert.Equal("Van Der Waal", target.LastName);
			Assert.Equal("Sr.", target.Suffix);
            Assert.Equal("Jack Van Der Waal", target.DisplayName);
		}

		[Fact]
		public void Parse_MrJackFrancisMarionVanDerWaalSr() {
			var fullName = "Mr. Jack Francis Marion Van Der Waal Sr.";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Mr.", target.Title);
			Assert.Equal("Jack", target.FirstName);
			Assert.Equal("Francis Marion", target.MiddleName);
			Assert.Equal("Van Der Waal", target.LastName);
			Assert.Equal("Sr.", target.Suffix);
		}


		[Fact]
		public void Parse_Null() {
			string fullName = null;
			// ReSharper disable once ExpressionIsAlwaysNull
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Null(target.Title);
			Assert.Null(target.FirstName);
			Assert.Null(target.MiddleName);
			Assert.Null(target.LastName);
			Assert.Null(target.Suffix);
		}


		[Fact]
		public void Parse_Blank() {
			var fullName = "";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Null(target.Title);
			Assert.Null(target.FirstName);
			Assert.Null(target.MiddleName);
			Assert.Null(target.LastName);
			Assert.Null(target.Suffix);
		}


		[Fact]
		public void Parse_Space() {
			var fullName = " ";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Null(target.Title);
			Assert.Null(target.FirstName);
			Assert.Null(target.MiddleName);
			Assert.Null(target.LastName);
			Assert.Null(target.Suffix);
		}

		[Fact]
		public void Parse_Static() {
			var fullName = "";
			var target = FullNameParser.Parse(fullName);

			Assert.NotNull(target);
		}
	}
}