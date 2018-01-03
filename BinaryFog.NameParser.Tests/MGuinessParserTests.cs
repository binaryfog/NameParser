using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace BinaryFog.NameParser.Tests {


	/// <summary>
	///This is a test class for FullNameParserTest and is intended
	///to contain all FullNameParserTest Unit Tests
	///</summary>
	public class MGuinessParserTests
    {
        //Unit test suggested by mguiness
        [Fact]
        public void Parse_JohnTDOTHancockCommaSpaceJr()
        {
            var fullName = "John T. Hancock, Jr.";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("John", target.FirstName);
            Assert.Equal("Hancock", target.LastName);
            Assert.Equal("T.", target.MiddleName);
            Assert.Equal("Jr.", target.Suffix);
            Assert.Equal("John T. Hancock", target.DisplayName);

        }



    }
}
