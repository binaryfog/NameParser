using Xunit;

namespace BinaryFog.NameParser.Tests {
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
	public class ExtraCasesTests {
		[Fact]
		public void Parse_DehartCommaSpacePhilip() {
			var fullName = "DeHart, Philip";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Philip", target.FirstName);
			Assert.Equal("DeHart", target.LastName);
			Assert.Equal("Philip DeHart", target.DisplayName);
			Assert.Null(target.Title);
		}

        [Fact]
        public void Parse_InesDelaCuna()
        {
            //ARRANGE
            var fullName = "Ines De La Cuna";
            var target = new FullNameParser(fullName);

            //ACT
            target.Parse();

            //ASSERT
            Assert.Equal("Ines", target.FirstName);
            Assert.Equal("De La Cuna", target.LastName);
            Assert.Equal("Ines De La Cuna", target.DisplayName);
            Assert.Null(target.Title);
        }

        [Fact]
        public void Parse_JohnKennedyIII()
        {
            //ARRANGE
            var fullName = "John Kennedy III";
            var target = new FullNameParser(fullName);

            //ACT
            target.Parse();

            //ASSERT
            Assert.Equal("John", target.FirstName);
            Assert.Equal("Kennedy", target.LastName);
            Assert.Equal("John Kennedy", target.DisplayName);
            Assert.Null(target.Title);
            Assert.Equal("III", target.Suffix);
        }

        [Fact]
		public void Parse_DehartCommaTwoSpacesPhilip() {
			var fullName = "DeHart,  Philip";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Philip", target.FirstName);
			Assert.Equal("DeHart", target.LastName);
			Assert.Equal("Philip DeHart", target.DisplayName);
			Assert.Null(target.Title);
		}

		[Fact]
		public void Parse_DehartCommaPhilip() {
			var fullName = "DeHart,Philip";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Philip", target.FirstName);
			Assert.Equal("DeHart", target.LastName);
			Assert.Equal("Philip DeHart", target.DisplayName);
			Assert.Null(target.Title);
		}

		[Fact]
		public void Parse_PhilipDeHartEsq() {
			var fullName = "Philip DeHart ESQ";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Philip", target.FirstName);
			Assert.Equal("DeHart", target.LastName);
			Assert.Equal("ESQ", target.Suffix);
			Assert.Equal("Philip DeHart", target.DisplayName);
			Assert.Null(target.Title);
		}

		[Fact]
		public void Parse_MrJackJohnsonEsq() {
			var fullName = @"Mr.Jack Johnson, ESQ""";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Mr.", target.Title);
			Assert.Equal("Jack", target.FirstName);
			Assert.Equal("Johnson", target.LastName);
			Assert.Equal(@"ESQ""", target.Suffix);
			Assert.Equal("Jack Johnson", target.DisplayName);
		}

		[Fact]
		public void Parse_PasqualeQuotePatQuoteJohnson() {
			var fullName = "Pasquale 'Pat' Johnson";
			var target = new FullNameParser(fullName);
			target.Parse();


			Assert.Equal("Pasquale", target.FirstName);
			Assert.Equal("Johnson", target.LastName);
			Assert.Equal("Pasquale Johnson", target.DisplayName);
			Assert.Equal("Pat", target.NickName);
		}

		[Fact]
		public void Parse_JoseMiguelDelaVega() {
			var fullName = "Jose Miguel de la Vega";
			var target = new FullNameParser(fullName);
			target.Parse();


			Assert.Equal("Jose", target.FirstName);
			Assert.Equal("Miguel", target.MiddleName);
			Assert.Equal("de la Vega", target.LastName);
			Assert.Equal(fullName, target.DisplayName);
		}

		//kennedy, john(jack) f
		[Fact]
		public void Parse_KennedyCommaJohnScopeJackScopeSpaceF() {
			var fullName = "Kennedy, John(Jack) F";
			var target = new FullNameParser(fullName);
			target.Parse();


			Assert.Equal("John", target.FirstName);
			Assert.Equal("Kennedy", target.LastName);
			Assert.Equal("John Kennedy", target.DisplayName);
		}

		//kennedy, john(jack) f
		[Fact]
		public void Parse_JohnScopeJackScopeSpaceFSpaceKennedy() {
			var fullName = "John(Jack) F Kennedy";
			var target = new FullNameParser(fullName);
			target.Parse();


			Assert.Equal("John", target.FirstName);
			Assert.Equal("Kennedy", target.LastName);
			Assert.Equal("John F. Kennedy", target.DisplayName);
		}

		//Maria Delores Esquivel-Moreno
		[Fact]
		public void Parse_MariaSpaceDeloresSpaceEsquivelDashMoreno() {
			var fullName = "Maria Delores Esquivel-Moreno";
			var target = new FullNameParser(fullName);
			target.Parse();


			Assert.Equal("Maria", target.FirstName);
			Assert.Equal("Delores", target.MiddleName);
			Assert.Equal("Esquivel-Moreno", target.LastName);
			Assert.Equal(fullName, target.DisplayName);
		}

		//Esquivel-Moreno, Maria Delores 
		[Fact]
		public void Parse_EsquivelDashMorenoCommaMariaSpaceDelores()
		{
			var fullName = "Esquivel-Moreno, Maria Delores";
			var target = new FullNameParser(fullName);
			target.Parse();


			Assert.Equal("Maria", target.FirstName);
			Assert.Equal("Delores", target.MiddleName);
			Assert.Equal("Esquivel-Moreno", target.LastName);
			Assert.Equal("Maria Esquivel-Moreno", target.DisplayName);
		}

		//Esquivel-Moreno, Maria Delores III
		[Fact]
		public void Parse_EsquivelDashMorenoCommaMariaSpaceDeloresSpaceThird()
		{
			var fullName = "Esquivel-Moreno, Maria Delores III";
			var target = new FullNameParser(fullName);
			target.Parse();


			Assert.Equal("Maria", target.FirstName);
			Assert.Equal("Delores", target.MiddleName);
			Assert.Equal("III", target.Suffix);
			Assert.Equal("Esquivel-Moreno", target.LastName);
			Assert.Equal("Maria Esquivel-Moreno", target.DisplayName);
		}

		//MANUEL ESQUIPULAS SOHOM
		[Fact]
		public void Parse_ManuelSpaceEsquipulasSpaceSohom() {
			var fullName = "Manuel Esquipulas Sohom";
			var target = new FullNameParser(fullName);
			target.Parse();


			Assert.Equal("Manuel", target.FirstName);
			Assert.Equal("Esquipulas", target.MiddleName);
			Assert.Equal("Sohom", target.LastName);
			Assert.Equal("Manuel Esquipulas Sohom", target.DisplayName);
		}

        //Tammy L. Baker
        [Fact]
        public void Parse_TammySpaceLDotBaker()
        {
            var fullName = "Tammy L. Baker";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.Equal("Tammy", target.FirstName);
            Assert.Equal("L.", target.MiddleName);
            Assert.Equal("Baker", target.LastName);
            Assert.Equal("Tammy L. Baker", target.DisplayName);
        }

        //Tammy L. van Baker
        [Fact]
        public void Parse_TammySpaceLDotVanSpaceBaker()
        {
            var fullName = "Tammy L. van Baker";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.Equal("Tammy", target.FirstName);
            Assert.Equal("L.", target.MiddleName);
            Assert.Equal("van Baker", target.LastName);
            Assert.Equal("Tammy L. van Baker", target.DisplayName);
        }

        //Tammy L. Blythe-Baker
        [Fact]
        public void Parse_TammySpaceLDotBlytheHyphenBaker()
        {
            var fullName = "Tammy L. Blythe-Baker";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.Equal("Tammy", target.FirstName);
            Assert.Equal("L.", target.MiddleName);
            Assert.Equal("Blythe-Baker", target.LastName);
            Assert.Equal("Tammy L. Blythe-Baker", target.DisplayName);
        }

        //Jimmy Lee Dabney II
        [Fact]
        public void Parse_JimmySpaceLeeSpaceDabneySpaceII()
        {
            var fullName = "Jimmy Lee Dabney II";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.Equal("Jimmy", target.FirstName);
            Assert.Equal("Lee", target.MiddleName);
            Assert.Equal("Dabney", target.LastName);
            Assert.Equal("II", target.Suffix);
            Assert.Equal("Jimmy Lee Dabney", target.DisplayName);
        }

        //Tammy L. Baker II
        [Fact]
        public void Parse_TammySpaceLDotBakerSpaceII()
        {
            var fullName = "Tammy L. Baker II";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.Equal("Tammy", target.FirstName);
            Assert.Equal("L.", target.MiddleName);
            Assert.Equal("Baker", target.LastName);
            Assert.Equal("II", target.Suffix);
            Assert.Equal("Tammy L. Baker", target.DisplayName);
        }

        //Tammy L. Blythe-Baker II
        [Fact]
        public void Parse_TammySpaceLDotBlytheHyphenBakerSpaceII()
        {
            var fullName = "Tammy L. Blythe-Baker II";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.Equal("Tammy", target.FirstName);
            Assert.Equal("L.", target.MiddleName);
            Assert.Equal("Blythe-Baker", target.LastName);
            Assert.Equal("II", target.Suffix);
            Assert.Equal("Tammy L. Blythe-Baker", target.DisplayName);
        }

        //Tammy L. van Baker II
        [Fact]
        public void Parse_TammySpaceLDotVanSpaceBakerSpaceII()
        {
            var fullName = "Tammy L. van Baker II";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.Equal("Tammy", target.FirstName);
            Assert.Equal("L.", target.MiddleName);
            Assert.Equal("van Baker", target.LastName);
            Assert.Equal("II", target.Suffix);
            Assert.Equal("Tammy L. van Baker", target.DisplayName);
        }

        [Fact]
        public void Parse_SpaceTammySpaceLDotVanSpaceBakerSpaceIISpace()
        {
            var fullName = " Tammy L. van Baker II ";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.Equal("Tammy", target.FirstName);
            Assert.Equal("L.", target.MiddleName);
            Assert.Equal("van Baker", target.LastName);
            Assert.Equal("II", target.Suffix);
            Assert.Equal("Tammy L. van Baker", target.DisplayName);
        }

        [Fact]
        public void Parse_AlSpaceBundy()
        {
            var fullName = "Al Bundy";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.Equal("Al", target.FirstName);
            Assert.Equal("Bundy", target.LastName);
            Assert.Equal("Al Bundy", target.DisplayName);
        }

        [Fact]
        public void Parse_QuincyDeLaRosaSr()
        {
            var fullName = "Quincy De La Rosa Sr";
            var target = new FullNameParser(fullName);
            target.Parse();


            Assert.Equal("Quincy", target.FirstName);
            Assert.Equal("De La Rosa", target.LastName);
            Assert.Equal("Sr", target.Suffix);
            Assert.Equal("Quincy De La Rosa", target.DisplayName);
        }

        /* No, you can not have a title of SR.
        //SR. John Henry William dela Vega, Jr Esq.
        [Fact]
        public void Parse_SrJohnHenryWilliamdelaVegaCommaJrEsq()
        {
            string fullName = "SR. John Henry William dela Vega, Jr Esq.";
            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("SR.", target.Title);
            Assert.Equal("John", target.FirstName);
            Assert.Equal("Henry William", target.MiddleName);
            Assert.Equal("dela Vega", target.LastName);
            Assert.Equal("Jr Esq.", target.Suffix);
            Assert.Equal("John dela Vega", target.DisplayName);
        }
		*/
        [Fact]
		public void FirstPrefixedLastSuffix() {
			var fullName = "Tammy van Baker II";
			var target = new FullNameParser(fullName);
			target.Parse();
			
			Assert.Equal("Tammy", target.FirstName);
			Assert.Equal("van Baker", target.LastName);
			Assert.Equal("Tammy van Baker", target.DisplayName);
		}
		 
		[Fact]
		public void FirstMiddlePrefixedLastSuffix() {
			var fullName = "Tammy Lee van Baker II";
			var target = new FullNameParser(fullName);
			target.Parse();
			
			Assert.Equal("Tammy", target.FirstName);
			Assert.Equal("Lee", target.MiddleName);
			Assert.Equal("van Baker", target.LastName);
			Assert.Equal("Tammy Lee van Baker", target.DisplayName);
		}
		 
		[Fact]
		public void FirstNickHyphenatedLast() {
			var fullName = "Manuel \"Manny\" Esquipulas-Sohom";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Manuel", target.FirstName);
			Assert.Equal("Esquipulas-Sohom", target.LastName);
			Assert.Equal("Manuel Esquipulas-Sohom", target.DisplayName);
		}
		 
		[Fact]
		public void FirstNickMiddleHyphenatedLast() {
			var fullName = "Maria \"Mary\" Delores Esquipulas-Sohom";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Maria", target.FirstName);
			Assert.Equal("Delores", target.MiddleName);
			Assert.Equal("Esquipulas-Sohom", target.LastName);
			Assert.Equal("Maria Delores Esquipulas-Sohom", target.DisplayName);
		}

		[Fact]
		public void FirstMiddleHyphenatedLastSuffix() {
			var fullName = "Manuel Miguel Esquipulas-Sohom II";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Manuel", target.FirstName);
			Assert.Equal("Miguel", target.MiddleName);
			Assert.Equal("Esquipulas-Sohom", target.LastName);
			Assert.Equal("Manuel Miguel Esquipulas-Sohom", target.DisplayName);
		}

		[Fact]
		public void FirstTwoMiddleHyphenatedLast() {
			var fullName = "Manuel Miguel Montoya Esquipulas-Sohom";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Manuel", target.FirstName);
			Assert.Equal("Miguel Montoya", target.MiddleName);
			Assert.Equal("Esquipulas-Sohom", target.LastName);
			Assert.Equal("Manuel Miguel Montoya Esquipulas-Sohom", target.DisplayName);
		}
		
		[Fact]
		public void FemaleFirstTwoMiddleHyphenatedLast() {
			var fullName = "Maria Ellen Delores Esquipulas-Sohom";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Maria", target.FirstName);
			Assert.Equal("Ellen Delores", target.MiddleName);
			Assert.Equal("Esquipulas-Sohom", target.LastName);
			Assert.Equal("Maria Ellen Delores Esquipulas-Sohom", target.DisplayName);
		}
		
		[Fact]
		public void FirstLastNick() {
			var fullName = "Manuel Sohom \"Manny\"";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Manuel", target.FirstName);
			Assert.Equal("Sohom", target.LastName);
			Assert.Equal("Manuel Sohom", target.DisplayName);
		}
		
		[Fact]
		public void FirstHyphenatedLastNick() {
			var fullName = "Manuel Esquipulas-Sohom \"Manny\"";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Manuel", target.FirstName);
			Assert.Equal("Esquipulas-Sohom", target.LastName);
			Assert.Equal("Manuel Esquipulas-Sohom", target.DisplayName);
            Assert.Equal("Manny", target.NickName);
        }

		[Fact]
		public void SingleHyphenated() {
			var fullName = "Esquipulas-Sohom";
			var target = new FullNameParser(fullName);
			target.Parse();
			
			Assert.Equal("Esquipulas-Sohom", target.LastName);
			Assert.Equal("Esquipulas-Sohom", target.DisplayName);
		}

		[Fact]
		public void LastNameCommaFirstNameMiddleNameSuffixPattern_Test()
		{
			var fullName = "SAWYER, JONATHAN DAVID JR";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("JONATHAN", target.FirstName);
			Assert.Equal("SAWYER", target.LastName);
			Assert.Equal("DAVID", target.MiddleName);
			Assert.Equal("JR", target.Suffix);
			Assert.Equal("JONATHAN SAWYER", target.DisplayName);
		}

	}
}