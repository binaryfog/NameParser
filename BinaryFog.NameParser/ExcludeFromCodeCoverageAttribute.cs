using System;

namespace BinaryFog.NameParser {
	[AttributeUsage(AttributeTargets.All,Inherited = false)]
	internal class ExcludeFromCodeCoverageAttribute : Attribute {
	}
}