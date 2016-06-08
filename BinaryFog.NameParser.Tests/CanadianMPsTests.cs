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
            if ( !String.IsNullOrEmpty(title))
                fullName = $"{title} {firstName} {lastName}";
            else
                fullName = $"{firstName} {lastName}";

            Console.WriteLine(fullName);

            //ACT
            var target = new FullNameParser(fullName);
            target.Parse();

            //ASSERT
            if (!String.IsNullOrEmpty(title))
                Assert.AreEqual(title, target.Title);
            else
                Assert.IsNull(target.Title);

            Assert.AreEqual( firstName, target.FirstName);
            Assert.AreEqual( lastName, target.LastName);
            Assert.AreEqual(fullName, target.DisplayName);
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
    }
}
