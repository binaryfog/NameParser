using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BinaryFog.NameParser.Tests
{
    [TestClass]
    public class AfricanNamesTests
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }



        [TestMethod]
        [DeploymentItem(@".\DataFiles\AfricanNames.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\AfricanNames.xml", "Person", DataAccessMethod.Sequential)]
        public void AfricanNames_Test()
        {
            //ARRANGE 
            string firstName = Convert.ToString(TestContext.DataRow["FirstName"]);
            string lastName = Convert.ToString(TestContext.DataRow["LastName"]);
            string expectedDisplayName = Convert.ToString(TestContext.DataRow["Name"]);

            string fullName = $"{firstName} {lastName}";
            Console.WriteLine(fullName);

            //ACT
            var target = new FullNameParser(fullName);
            target.Parse();

            //ASSERT
            
            Assert.AreEqual( firstName, target.FirstName, "First Name doesn't match");
            Assert.AreEqual( lastName, target.LastName, "Last Name doesn't match");

            Assert.AreEqual(expectedDisplayName, target.DisplayName, "DisplayName doesn't match");
        }


        
    }
}
