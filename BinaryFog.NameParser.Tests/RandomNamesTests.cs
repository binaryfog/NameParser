using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomNameGeneratorLibrary;

namespace BinaryFog.NameParser.Tests
{
    [TestClass]
    public class RandomNamesTests
    {
        [TestMethod]
        public void RandomNamesTest()
        {
            PersonNameGenerator g = new PersonNameGenerator();
            string firstName = g.GenerateRandomMaleFirstName();
            string lastName = g.GenerateRandomLastName();

            string fullName = String.Format("{0} {1}", firstName, lastName);
            Console.WriteLine(fullName);

            FullNameParser target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual( firstName, target.FirstName);
            Assert.AreEqual( lastName, target.LastName);
            Assert.AreEqual(fullName, target.DisplayName);
        }
    }
}
