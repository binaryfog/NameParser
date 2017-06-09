using System;
using Xunit;


namespace BinaryFog.NameParser.Tests
{
    public class AfricanNamesTests
    {
		/*
        [Fact]
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
            
            Assert.Equal( firstName, target.FirstName, "First Name doesn't match");
            Assert.Equal( lastName, target.LastName, "Last Name doesn't match");

            Assert.Equal(expectedDisplayName, target.DisplayName, "DisplayName doesn't match");
        }
		*/
    }
}
