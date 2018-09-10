using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace BinaryFog.NameParser.Tests {


	/// <summary>
	///This is a test class for FullNameParserTest and is intended
	///to contain all FullNameParserTest Unit Tests
	///</summary>
	public class InitialAsMiddleNameTests
    {
		[Fact]
		public void Parse_MrJonADOTvanderWaalJr() {
			var fullName = "Mr. Jon A. van der Waal Jr.";
			var target = new FullNameParser(fullName);
			target.Parse();

			Assert.Equal("Jon", target.FirstName);
			Assert.Equal("van der Waal", target.LastName);
            Assert.Equal("A.", target.MiddleName);
            Assert.Equal("Mr.", target.Title);
            Assert.Equal("Jr.", target.Suffix);

        }

        [Fact]
        public void Parse_MrJonADOTWaalJr()
        {
            var fullName = "Mr. Jon A. Waal Jr.";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Jon", target.FirstName);
            Assert.Equal("Waal", target.LastName);
            Assert.Equal("A.", target.MiddleName);
            Assert.Equal("Mr.", target.Title);
            Assert.Equal("Jr.", target.Suffix);

        }

        [Fact]
        public void Parse_JonADOTvanderWaalJr()
        {
            var fullName = "Jon A. van der Waal Jr.";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Jon", target.FirstName);
            Assert.Equal("van der Waal", target.LastName);
            Assert.Equal("A.", target.MiddleName);
            Assert.Equal("Jr.", target.Suffix);

        }

        [Fact]
        public void Parse_JonADOTWaalJr()
        {
            var fullName = "Jon A. Waal Jr.";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Jon", target.FirstName);
            Assert.Equal("Waal", target.LastName);
            Assert.Equal("A.", target.MiddleName);
            Assert.Equal("Jr.", target.Suffix);

        }

        [Fact]
        public void Parse_JamesAAdams()
        {
            var fullName = "James A Adams";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("James", target.FirstName);
            Assert.Equal("A Adams", target.LastName);
            
        }

        [Fact]
        public void Parse_JamesADOTAdams()
        {
            var fullName = "James A. Adams";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("James", target.FirstName);
            Assert.Equal("Adams", target.LastName);
            Assert.Equal("A.", target.MiddleName);
        }



    }
}
