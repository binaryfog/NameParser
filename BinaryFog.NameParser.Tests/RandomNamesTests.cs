using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using RandomNameGeneratorLibrary;
using Xunit;

namespace BinaryFog.NameParser.Tests {
	public class RandomNamesTests {
		private static PersonNameGenerator NameGen { get; }
			= new PersonNameGenerator();

		private static RandomNumberGenerator Rng { get; }
			= RandomNumberGenerator.Create();

		public static IEnumerable<object[]> GetRandomNames(int count) {
			var randomBytes = new byte[count];
			Rng.GetBytes(randomBytes);

			for (var i = 0 ; i < count ; ++i) {
				var randomBit1 = (randomBytes[i] & (1 << 0)) != 0;
				var randomBit2 = (randomBytes[i] & (1 << 1)) != 0;
				var randomBit3 = (randomBytes[i] & (1 << 2)) != 0;
				var randomBit4 = (randomBytes[i] & (1 << 3)) != 0;
				var randomBit5 = (randomBytes[i] & (1 << 4)) != 0;
				var randomBit6 = (randomBytes[i] & (1 << 5)) != 0;
				var nameCount = 1 + ((randomBytes[i] & (3 << 6)) >> 6); // 1 to 4 names


				var names = new string[nameCount];


				var gender = randomBit1;
				names[0] = gender
					? NameGen.GenerateRandomMaleFirstName()
					: NameGen.GenerateRandomFemaleFirstName();

				if (names.Length > 2) {
					names[1] = randomBit2
						? NameGen.GenerateRandomFirstName()
						: NameGen.GenerateRandomLastName();
					if (randomBit6)
						names[1] = $@"""{names[1]}""";
				}


				if (names.Length > 3)
					names[2] = randomBit3
						? NameGen.GenerateRandomFirstName()
						: NameGen.GenerateRandomLastName();

				if (names.Length > 1 || randomBit5 && names.Length == 1)
					names[names.Length - 1] =
						randomBit4
							? NameGen.GenerateRandomLastName()
							: NameGen.GenerateRandomLastName() + "-" + NameGen.GenerateRandomLastName();

				yield return new object[] {
					names
				};
			}
		}

		[Theory]
		[MemberData(nameof(GetRandomNames), 400, DisableDiscoveryEnumeration = true)]
		public void RandomNamesTest(string[] names) {
			var firstName = names[0];
			var singleName = names.Length == 1;
			var lastName = singleName ? null : names[names.Length - 1];
			var fullName = string.Join(" ", names);
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
					lastName = string.Join(" ", names[names.Length - 2], lastName);
				}

				Assert.Equal(lastName, target.LastName);
				
				var displayName = string.Join(" ", firstName, lastName);

				Assert.Equal(displayName, target.DisplayName);
			}
		}
	}
}