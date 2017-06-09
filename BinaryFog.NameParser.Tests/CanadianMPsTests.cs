using System;
using Xunit;


namespace BinaryFog.NameParser.Tests
{
    public class CanadianMPsTests
    {
		/*
        [Fact]
        [DeploymentItem(@".\DataFiles\CanadianMPs.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\CanadianMPs.xml", "MemberOfParliament", DataAccessMethod.Sequential)]
        public void CanadianMPs_Test()
        {
            //ARRANGE 
            string title = Convert.ToString( TestContext.DataRow["PersonShortHonorific"]);
            string firstName = Convert.ToString(TestContext.DataRow["PersonOfficialFirstName"]);
            string lastName = Convert.ToString(TestContext.DataRow["PersonOfficialLastName"]);

            string fullName;
            string expectedDisplayName = $"{firstName} {lastName}";
            if (!String.IsNullOrEmpty(title))
            {
                fullName = $"{title} {firstName} {lastName}";

            }
            else
            {
                fullName = $"{firstName} {lastName}";
            }

            Console.WriteLine(fullName);

            //ACT
            var target = new FullNameParser(fullName);
            target.Parse();

            //ASSERT
            if (!String.IsNullOrEmpty(title))
                Assert.Equal(title, target.Title, "Titles doesn't match");
            else
                Assert.IsNull(target.Title, "Title was expected to be null");

            Assert.Equal( firstName, target.FirstName, "First Name doesn't match");
            Assert.Equal( lastName, target.LastName, "Last Name doesn't match");

            Assert.Equal(expectedDisplayName, target.DisplayName, "DisplayName doesn't match");
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
            Assert.IsNull(target.Title);
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
            Assert.IsNull(target.Title);
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
            Assert.IsNull(target.Title);
        }

        [Fact]
        public void Parse_HonDOTGinettePetitpasTaylor()
        {
            var fullName = "Hon. Ginette Petitpas Taylor";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Hon.", target.Title, "Incorrect title");
            Assert.Equal("Ginette", target.FirstName, "Incorrect first name");
            Assert.Equal("Petitpas Taylor", target.LastName, "Incorrect last name");
            Assert.Equal("Ginette Petitpas Taylor", target.DisplayName, "Incorrect display name");
            
        }

        [Fact]
        public void Parse_HonDOTDeepakObhrai()
        {
            var fullName = "Hon. Deepak Obhrai";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Hon.", target.Title, "Incorrect title");
            Assert.Equal("Deepak", target.FirstName, "Incorrect first name");
            Assert.Equal("Obhrai", target.LastName, "Incorrect last name");
            Assert.Equal("Deepak Obhrai", target.DisplayName, "Incorrect display name");

        }

        [Fact]
        public void Parse_HonDOTJodyWilsonDASHRaybould()
        {
            var fullName = "Hon. Jody Wilson-Raybould";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Hon.", target.Title, "Incorrect title");
            Assert.Equal("Jody", target.FirstName, "Incorrect first name");
            Assert.Equal("Wilson-Raybould", target.LastName, "Incorrect last name");
            Assert.Equal("Jody Wilson-Raybould", target.DisplayName, "Incorrect display name");

        }
		*/
    }
}
