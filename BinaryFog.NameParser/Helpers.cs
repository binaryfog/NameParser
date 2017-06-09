using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Loader;
using System.Text;

namespace BinaryFog.NameParser {
	internal static class Helpers {
		/// <summary>
		/// A helper that returns the result of a <see cref="Func{TResult}"/>,
		/// or in the event of an exception, <paramref name="default"/>.
		/// </summary>
		/// <typeparam name="T">A given result type, the result of <paramref name="fn"/> and the type of <paramref name="default"/>.</typeparam>
		/// <param name="fn">A given function delegate that returns a <typeparamref name="T"/> or throws an exception.</param>
		/// <param name="default">An alternative in the event that an exception is caught while executing <paramref name="fn"/>.</param>
		/// <returns>The result of <paramref name="fn"/> or in the event of an exception, <paramref name="default"/>.</returns>
		internal static T TryOrDefault<T>(Func<T> fn, T @default = default(T)) {
			try {
				return fn();
			}
			catch {
				return @default;
			}
		}

		/// <summary>
		/// A shorthand helper for use with
		/// <see cref="Enumerable.Where{TSource}(System.Collections.Generic.IEnumerable{TSource},System.Func{TSource,bool})"/>.
		/// </summary>
		/// <typeparam name="T">A given class type, nullable.</typeparam>
		/// <param name="instance">A possibly <c>null</c> or instance of <typeparamref name="T"/>.</param>
		/// <returns>Returns <c>true</c> if <paramref name="instance"/> is not <c>null</c>, otherwise <c>false</c>.</returns>
		internal static bool NotNull<T>(T instance) where T : class => instance != null;
		
		private static readonly Stopwatch Stopwatch = Stopwatch.StartNew();
		/// <summary>
		/// Returns an arbitrary but incremental timestamp measured in milliseconds.
		/// Internally this uses the <see cref="System.Diagnostics.Stopwatch" /> class.
		/// This provides no guarantee of starting time prior to the first call of this method.
		/// That means that time values returned by this method should only be compared to
		/// other time values returned by this method.
		/// </summary>
		/// <returns>An arbitrary timestamp in milliseconds.</returns>
		internal static long CreateTimestamp() {
			return Stopwatch.ElapsedMilliseconds;
		}

		internal class AssemblyComparer : IComparer<Assembly> {
			internal static AssemblyComparer Instance = new AssemblyComparer();
			public int Compare(Assembly x, Assembly y) {
				Debug.Assert(x != null);
				Debug.Assert(y != null);
				Debug.Assert(!ReferenceEquals(x,y));
				Debug.Assert(!Equals(x,y));
				/* code coverage says no hits
				if (x == null && y == null) return 0;
				if (x == null) return -1;
				if (y == null) return 1;
				if (ReferenceEquals(x, y)||Equals(x, y)) return 0;
				*/
				var xHashCode = x.GetHashCode();
				var yHashCode = y.GetHashCode();
				return xHashCode == yHashCode
					? string.Compare(x.FullName, y.FullName, StringComparison.Ordinal)
					: (xHashCode < yHashCode ? -1 : 1);
			}
		}
		
		/// <summary>
		///		Scans the current process for all loaded <see cref="ProcessModule"/>s
		///		and discovers them as an <see cref="Assembly" /> if they are one.
		/// 
		///		This updates a cache of <see cref="KnownAssemblies" /> when called.
		/// </summary>
		/// <returns>A set of loaded assemblies.</returns>
		internal static ISet<Assembly> GetLoadedAssemblies() {

			_lastCheckedLoadedAssemblies = CreateTimestamp();
			var procMods = Process.GetCurrentProcess().Modules.OfType<ProcessModule>();
			var asmNames = procMods.Select(mod => TryOrDefault( ()
					=> AssemblyLoadContext.GetAssemblyName(mod.FileName)) )
				.Where(NotNull);
			var asms = asmNames.Select(asmName => TryOrDefault(()
					=> AssemblyLoadContext.Default.LoadFromAssemblyName(asmName)))
				.Where(NotNull);
			_knownAssemblies = asms.ToImmutableSortedSet(AssemblyComparer.Instance);
			return _knownAssemblies;
		}
		
		private static long _lastCheckedLoadedAssemblies;
		private static ISet<Assembly> _knownAssemblies;

		/// <summary>
		/// A set of known loaded assemblies.
		/// Accurate to <see cref="KnownAssembliesTimeout"/> milliseconds.
		/// </summary>
		internal static ISet<Assembly> KnownAssemblies
			=> _knownAssemblies ?? GetLoadedAssemblies();

		/// <summary>
		/// A minimum freshness in milliseconds of the <see cref="KnownAssemblies"/> set.
		/// </summary>
		internal static int KnownAssembliesTimeout = 60000;
	}
}