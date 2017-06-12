using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;


namespace BinaryFog.NameParser.Tests
{
    public class CanadianMPsTests
    {
		
	    public static IEnumerable<object[]> GetCanadianMPs()
		    => DataFiles.GetXDocument("CanadianMPs.xml")?
			    .Root?.Elements("MemberOfParliament")
			    .Select(xe => new object[] {
				    xe.Element("PersonShortHonorific")?.Value,
				    xe.Element("PersonOfficialFirstName")?.Value,
				    xe.Element("PersonOfficialLastName")?.Value,
			    });


        [Theory]
		[MemberData(nameof(GetCanadianMPs),DisableDiscoveryEnumeration = true)]
		public void CanadianMPs_Test(string title, string firstName, string lastName)
        {
            //ARRANGE
	        var firstNames = firstName.Split(new[] {' '}, 2);
	        var firstFirstName = firstNames[0];
	        var secondFirstName = firstNames.Length == 2 ? firstNames[1] : null;
	        var expectedDisplayName = $"{firstName} {lastName}";
	        var fullName = !String.IsNullOrEmpty(title)
		        ? $"{title} {expectedDisplayName}"
		        : $"{expectedDisplayName}";
	        //Console.WriteLine(fullName);

            //ACT
            var target = new FullNameParser(fullName);
            target.Parse();

            //ASSERT
            if (!String.IsNullOrEmpty(title))
                Assert.Equal(title, target.Title);
            else
                Assert.Null(target.Title);

			// Ruth Ellen Brasseau is more likely to be  first "Ruth" middle "Ellen" last "Brasseau"
			// or first "Ruth" last "Ellen Brasseau" than first "Ruth Ellen" last "Brasseau".
			// It would likely be hyphenated ("Ruth-Elen") or joined ("RuthEllen") in general usage.
	        if (firstName != target.FirstName) {
		        Assert.Equal(firstFirstName, target.FirstName);
		        Assert.Equal(secondFirstName, target.MiddleName);
		        //Assert.Contains(firstName, target.Results.Select(r => r.FirstName));
	        }
	        //Assert.Equal( firstName, target.FirstName);
	        Assert.Equal( lastName, target.LastName);

            Assert.Equal(expectedDisplayName, target.DisplayName);
        }


        [Fact]
        public void Parse_KevinLamoureux()
        {
            var fullName = "Kevin Lamoureux";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Kevin", target.FirstName);
            Assert.Equal("Lamoureux", target.LastName);
            Assert.Equal("Kevin Lamoureux", target.DisplayName);
            Assert.Null(target.Title);
        }

        [Fact]
        public void Parse_JenniferOConnell()
        {
            var fullName = "Jennifer O'Connell";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Jennifer", target.FirstName);
            Assert.Equal("O'Connell", target.LastName);
            Assert.Equal("Jennifer O'Connell", target.DisplayName);
            Assert.Null(target.Title);
        }

        [Fact]
        public void Parse_FayçalElDASHKhoury()
        {
            var fullName = "Fayçal El-Khoury";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Fayçal", target.FirstName);
            Assert.Equal("El-Khoury", target.LastName);
            Assert.Equal("Fayçal El-Khoury", target.DisplayName);
            Assert.Null(target.Title);
        }

        [Fact]
        public void Parse_HonDOTGinettePetitpasTaylor()
        {
            var fullName = "Hon. Ginette Petitpas Taylor";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Hon.", target.Title);
            Assert.Equal("Ginette", target.FirstName);
            Assert.Equal("Petitpas Taylor", target.LastName);
            Assert.Equal("Ginette Petitpas Taylor", target.DisplayName);
            
        }

        [Fact]
        public void Parse_HonDOTDeepakObhrai()
        {
            var fullName = "Hon. Deepak Obhrai";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Hon.", target.Title);
            Assert.Equal("Deepak", target.FirstName);
            Assert.Equal("Obhrai", target.LastName);
            Assert.Equal("Deepak Obhrai", target.DisplayName);

        }

        [Fact]
        public void Parse_HonDOTJodyWilsonDASHRaybould()
        {
            var fullName = "Hon. Jody Wilson-Raybould";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Hon.", target.Title);
            Assert.Equal("Jody", target.FirstName);
            Assert.Equal("Wilson-Raybould", target.LastName);
            Assert.Equal("Jody Wilson-Raybould", target.DisplayName);

        }

    }
}
