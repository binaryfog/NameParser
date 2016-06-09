using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BinaryFog.NameParser.Tests
{
    [TestClass]
    public class CanadianMPsTests
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }



        [TestMethod]
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
                Assert.AreEqual(title, target.Title, "Titles doesn't match");
            else
                Assert.IsNull(target.Title, "Title was expected to be null");

            Assert.AreEqual( firstName, target.FirstName, "First Name doesn't match");
            Assert.AreEqual( lastName, target.LastName, "Last Name doesn't match");

            Assert.AreEqual(expectedDisplayName, target.DisplayName, "DisplayName doesn't match");
        }


        [TestMethod]
        public void Parse_KevinLamoureux()
        {
            var fullName = "Kevin Lamoureux";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Kevin", target.FirstName);
            Assert.AreEqual("Lamoureux", target.LastName);
            Assert.AreEqual("Kevin Lamoureux", target.DisplayName);
            Assert.IsNull(target.Title);
        }

        [TestMethod]
        public void Parse_JenniferOConnell()
        {
            var fullName = "Jennifer O'Connell";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Jennifer", target.FirstName);
            Assert.AreEqual("O'Connell", target.LastName);
            Assert.AreEqual("Jennifer O'Connell", target.DisplayName);
            Assert.IsNull(target.Title);
        }

        [TestMethod]
        public void Parse_FayçalElDASHKhoury()
        {
            var fullName = "Fayçal El-Khoury";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual("Fayçal", target.FirstName);
            Assert.AreEqual("El-Khoury", target.LastName);
            Assert.AreEqual("Fayçal El-Khoury", target.DisplayName);
            Assert.IsNull(target.Title);
        }
    }
}
