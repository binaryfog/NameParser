using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BinaryFog.NameParser {
	static internal class NameComponentSets {
		public static readonly ImmutableHashSet<string> MaleFirstNames =
			new HashSet<string>( ( from c in Resources.MaleFirstNames.Split('\r', '\n') select c.ToLowerInvariant()) ).ToImmutableHashSet();
		public static readonly ImmutableHashSet<string> FemaleFirstNames =
			new HashSet<string>((from c in Resources.FemaleFirstNames.Split('\r', '\n') select c.ToLowerInvariant()) ).ToImmutableHashSet();
		public static readonly ImmutableHashSet<string> FirstNames =
			new HashSet<string>(MaleFirstNames.Concat(FemaleFirstNames)).ToImmutableHashSet(); 
		public static readonly ImmutableHashSet<string> LastNamesInUppercase =
			new HashSet<string>(Resources.USCensusLastNames.Split('\r', '\n')).ToImmutableHashSet();
	}

}