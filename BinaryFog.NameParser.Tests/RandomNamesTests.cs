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
            var g = new PersonNameGenerator();
            var firstName = g.GenerateRandomMaleFirstName();
            var lastName = g.GenerateRandomLastName();

            string fullName = $"{firstName} {lastName}";
            Console.WriteLine(fullName);

            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.AreEqual( firstName, target.FirstName);
            Assert.AreEqual( lastName, target.LastName);
            Assert.AreEqual(fullName, target.DisplayName);
        }
    }
}
