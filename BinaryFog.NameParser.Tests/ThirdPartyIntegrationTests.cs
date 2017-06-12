using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BinaryFog.NameParser.Tests {
	public class ThirdPartyIntegrationTests {
		[Fact]
		public void FirstMiddlePrefixedLastSuffix() {
			var fullName = "EXAMPLE";
			var target = new FullNameParser(fullName);

			Assert.False(FullNameParser.EnableAutomaticThirdPartyIntegration);

			target.Parse();
			
			Assert.Equal("EXAMPLE", target.FirstName);
			Assert.Null(target.LastName);
			Assert.Equal("EXAMPLE", target.DisplayName);

			FullNameParser.EnableAutomaticThirdPartyIntegration = true;

			target.Parse();
			
			Assert.Equal("Success", target.FirstName);
			Assert.Equal("Success", target.LastName);
			Assert.Equal("Success", target.DisplayName);
		}
	}
}