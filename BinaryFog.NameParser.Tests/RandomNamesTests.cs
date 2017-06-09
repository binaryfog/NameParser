using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using RandomNameGeneratorLibrary;
using Xunit;

namespace BinaryFog.NameParser.Tests {
	public class RandomNamesTests {
		/* There are names like "Miss", and "Lady" in the female first names generated...
		private static PersonNameGenerator NameGen { get; }
			= new PersonNameGenerator();

		private static RandomNumberGenerator Rng { get; }
			= RandomNumberGenerator.Create();
		
		public static IEnumerable<object[]> GetSimpleRandomNames(int count) {
			var randomBytes = new byte[count];
			Rng.GetBytes(randomBytes);

			for (var i = 0 ; i < count ; ++i) {
				var randomBit1 = (randomBytes[i] & (1 << 0)) != 0;
				var randomBit2 = (randomBytes[i] & (1 << 1)) != 0;
				var randomBit3 = (randomBytes[i] & (1 << 2)) != 0;
				var randomBit4 = (randomBytes[i] & (1 << 3)) != 0;
				var randomBit5 = (randomBytes[i] & (1 << 4)) != 0;
				var randomBit6 = (randomBytes[i] & (1 << 5)) != 0;
				var nameCount = 1 + ((randomBytes[i] & (3 << 6)) >> 6); // 1 to 4 nameParts


				var nameParts = new string[nameCount];


				var gender = randomBit1;
				nameParts[0] = gender
					? NameGen.GenerateRandomMaleFirstName()
					: NameGen.GenerateRandomFemaleFirstName();

				if (nameParts.Length > 2) {
					nameParts[1] = randomBit2
						? ( gender
							? NameGen.GenerateRandomMaleFirstName()
							: NameGen.GenerateRandomFemaleFirstName() )
						: NameGen.GenerateRandomLastName();
					if (randomBit6)
						nameParts[1] = $@"""{nameParts[1]}""";
				}


				if (nameParts.Length > 3)
					nameParts[2] = randomBit3
						? ( gender
							? NameGen.GenerateRandomMaleFirstName()
							: NameGen.GenerateRandomFemaleFirstName() )
						: NameGen.GenerateRandomLastName();

				if (nameParts.Length > 1 || randomBit5 && nameParts.Length == 1)
					nameParts[nameParts.Length - 1] =
						randomBit4
							? NameGen.GenerateRandomLastName()
							: NameGen.GenerateRandomLastName() + "-" + NameGen.GenerateRandomLastName();

				yield return new object[] {
					nameParts
				};
			}
		}

		[Theory]
		[MemberData(nameof(GetSimpleRandomNames), 400, DisableDiscoveryEnumeration = true)]
		public void SimpleRandomNamesTest(string[] nameParts) {
			var firstName = nameParts[0];
			var singleName = nameParts.Length == 1;
			var lastName = singleName ? null : nameParts[nameParts.Length - 1];
			var fullName = string.Join(" ", nameParts.Where(s => s[0] != '"'));
			//Console.WriteLine(fullName);

			var target = new FullNameParser(fullName);
			target.Parse();

			if (singleName) {
				if (target.FirstName == null) {
					//Assert.Null(target.FirstName);
					Assert.Equal(firstName, target.LastName);
				}
				else {
					Assert.Equal(firstName, target.FirstName);
					Assert.Null(target.LastName);
				}
				Assert.Equal(firstName, target.DisplayName);
			}
			else {
				Assert.Equal(firstName, target.FirstName);
				if (target.LastName.Contains(" ")) {
					lastName = string.Join(" ", nameParts[nameParts.Length - 2], lastName);
				}

				Assert.Equal(lastName, target.LastName);

				Assert.Equal(fullName, target.DisplayName);
			}
		}

		
		public static IEnumerable<object[]> GetTitledRandomNames(int count) {
			var randomBytes = new byte[count];
			Rng.GetBytes(randomBytes);

			for (var i = 0 ; i < count ; ++i) {
				var randomBit1 = (randomBytes[i] & (1 << 0)) != 0;
				var randomBit2 = (randomBytes[i] & (1 << 1)) != 0;
				var randomBit3 = (randomBytes[i] & (1 << 2)) != 0;
				var randomBit4 = (randomBytes[i] & (1 << 3)) != 0;
				var randomBit5 = (randomBytes[i] & (1 << 4)) != 0;
				var randomBit6 = (randomBytes[i] & (1 << 5)) != 0;
				var nameCount = 1 + ((randomBytes[i] & (3 << 6)) >> 6); // 1 to 4 nameParts


				var nameParts = new string[nameCount+1];


				var gender = randomBit1;

				nameParts[0] = randomBit3
					? "Dr."
					: gender
						? "Mr."
						: randomBit4
							? "Ms."
							: "Mrs.";
				if (randomBit5)
					nameParts[0] = nameParts[0].Replace(".", "");

				nameParts[1] = gender
					? NameGen.GenerateRandomMaleFirstName()
					: NameGen.GenerateRandomFemaleFirstName();

				if (nameParts.Length > 3) {
					nameParts[2] = randomBit2
						? ( gender
							? NameGen.GenerateRandomMaleFirstName()
							: NameGen.GenerateRandomFemaleFirstName() )
						: NameGen.GenerateRandomLastName();
					if (randomBit6)
						nameParts[2] = $@"""{nameParts[2]}""";
				}

				if (nameParts.Length > 4)
					nameParts[3] = randomBit3
						? ( gender
							? NameGen.GenerateRandomMaleFirstName()
							: NameGen.GenerateRandomFemaleFirstName() )
						: NameGen.GenerateRandomLastName();

				if (nameParts.Length > 1 || randomBit5 && nameParts.Length == 1)
					nameParts[nameParts.Length - 1] =
						randomBit4
							? NameGen.GenerateRandomLastName()
							: NameGen.GenerateRandomLastName() + "-" + NameGen.GenerateRandomLastName();

				yield return new object[] {
					nameParts.Where(namePart => namePart != null).ToArray()
				};
			}
		}

		[Theory]
		[MemberData(nameof(GetTitledRandomNames), 400, DisableDiscoveryEnumeration = true)]
		public void TitledRandomNamesTest(string[] nameParts) {
			var firstName = nameParts[1];
			var singleName = nameParts.Length == 2;
			var lastName = singleName ? null : nameParts[nameParts.Length - 1];
			var fullName = string.Join(" ", nameParts.Skip(1).Where(s => s[0] != '"'));
			//Console.WriteLine(fullName);

			var target = new FullNameParser(fullName);
			target.Parse();

			if (singleName) {
				if (target.FirstName == null) {
					//Assert.Null(target.FirstName);
					Assert.Equal(firstName, target.LastName);
				}
				else {
					Assert.Equal(firstName, target.FirstName);
					Assert.Null(target.LastName);
				}
				Assert.Equal(firstName, target.DisplayName);
			}
			else {
				Assert.Equal(firstName, target.FirstName);
				if (target.LastName.Contains(" ")) {
					lastName = string.Join(" ", nameParts[nameParts.Length - 2], lastName);
				}

				Assert.Equal(lastName, target.LastName);

				Assert.Equal(fullName, target.DisplayName);
			}
		}
		*/
	}
}