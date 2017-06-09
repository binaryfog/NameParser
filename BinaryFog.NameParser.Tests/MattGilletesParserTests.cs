using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace BinaryFog.NameParser.Tests {


	/// <summary>
	///This is a test class for FullNameParserTest and is intended
	///to contain all FullNameParserTest Unit Tests
	///</summary>
	public class MattGilletesParserTests
    {
		[Fact]
		public void Parse_IfoEkpreDASHOlomu() {
			var fullName = "Ifo Ekpre-Olomu";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Ifo", target.FirstName);
			Assert.Equal("Ekpre-Olomu", target.LastName);
			Assert.Equal("Ifo Ekpre-Olomu", target.DisplayName);
			
		}

        [Fact]
        public void Parse_IsaAbdulDASHQuddus()
        {
            var fullName = "Isa Abdul-Quddus";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Isa", target.FirstName);
            Assert.Equal("Abdul-Quddus", target.LastName);
            Assert.Equal("Isa Abdul-Quddus", target.DisplayName);

        }

        [Fact]
        public void Parse_EDOTJDOTSPACEManuel()
        {
            var fullName = "E.J. Manuel";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("E.J.", target.FirstName);
            Assert.Equal("Manuel", target.LastName);
            Assert.Equal("E.J. Manuel", target.DisplayName);

        }

        [Fact]
        public void Parse_DDOTSpaceJDotSpaceFoster()
        {
            var fullName = "D. J. Foster";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("D.J.", target.FirstName);
            Assert.Equal("Foster", target.LastName);
            Assert.Equal("D.J. Foster", target.DisplayName);

        }

        [Fact]
        public void Parse_AlDASHHajjSPACEShabazz()
        {
            var fullName = "Al-Hajj Shabazz";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Al-Hajj", target.FirstName);
            Assert.Equal("Shabazz", target.LastName);
            Assert.Equal("Al-Hajj Shabazz", target.DisplayName);

        }

        [Fact]
        public void Parse_DeAndreHoustonDASHCarson()
        {
            var fullName = "DeAndre Houston-Carson";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("DeAndre", target.FirstName);
            Assert.Equal("Houston-Carson", target.LastName);
            Assert.Equal("DeAndre Houston-Carson", target.DisplayName);

        }

        [Fact]
        public void Parse_CDOTSpaceJSpaceFoster()
        {
            var fullName = "C. J Smith";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("C.J", target.FirstName);
            Assert.Equal("Smith", target.LastName);
            Assert.Equal("C.J Smith", target.DisplayName);

        }

    }
}
