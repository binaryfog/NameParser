using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace BinaryFog.NameParser.Tests {


	/// <summary>
	///This is a test class for FullNameParserTest and is intended
	///to contain all FullNameParserTest Unit Tests
	///</summary>
	public class PFornessParserTests
    {
        //Unit test suggested by pforness
        [Fact]
        public void Parse_FornessCommaPaulSpaceTDot()
        {
            var fullName = "Forness, Paul T.";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Paul", target.FirstName);
            Assert.Equal("Forness", target.LastName);
            Assert.Equal("T", target.MiddleName);
            Assert.Equal("Paul T. Forness", target.DisplayName);

        }



    }
}
