using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;


namespace BinaryFog.NameParser.Tests {
	public class AfricanNamesTests {
		public static IEnumerable<object[]> GetAfricanNames()
			=> DataFiles.GetXDocument("AfricanNames.xml")?
			.Root?.Elements("Person")
			.Select(xe => new object[] {
				xe.Element("FirstName")?.Value,
				xe.Element("LastName")?.Value,
				xe.Element("Name")?.Value,
			});

		[Theory]
		[MemberData(nameof(GetAfricanNames), DisableDiscoveryEnumeration = true)]
		public void AfricanNames_Test(string firstName, string lastName, string expectedDisplayName) {
			// ARRANGE
			string fullName = $"{firstName} {lastName}";
			//Console.WriteLine(fullName);

			// ACT
			var target = new FullNameParser(fullName);
			target.Parse();

			// ASSERT
			Assert.Equal(firstName, target.FirstName);
			Assert.Equal(lastName, target.LastName);
			Assert.Equal(expectedDisplayName, target.DisplayName);
		}

        [Fact]
        public void Parse_SenQUOTEDerrickMarks()
        {
            var fullName = "Sen'Derrick Marks";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Sen'Derrick", target.FirstName);
            Assert.Equal("Marks", target.LastName);
            Assert.Equal("Sen'Derrick Marks", target.DisplayName);
            Assert.Null(target.Title);
        }

    }
}