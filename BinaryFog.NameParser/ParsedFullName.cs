using System;
using System.Diagnostics;
using System.Reflection;

namespace BinaryFog.NameParser {
	public class ParsedFullName
    {
#if DEBUG_FULL_NAME_PATTERN_RESULTS
		/*
		 * This is used for determining indisputably which IFullNamePattern
		 * implementation constructed this ParsedFullName instance.
		 * You might have to adjust this for your specific platform.
		 */

	    private static Type GetOuterStackFrameType(int frameOffset = 0) {
		    var st = (StackTrace) typeof(StackTrace).GetConstructor(Type.EmptyTypes).Invoke(null);
		    var frames = st.GetFrames();
		    var thisType = typeof(ParsedFullName);
		    var frameIndex = 0;
			// find this call
		    while (frames[frameIndex].GetMethod().DeclaringType != thisType)
			    ++frameIndex;
			// find the outermost call to the class
		    while (frames[frameIndex].GetMethod().DeclaringType == thisType)
			    ++frameIndex;
		    return frames[frameIndex+frameOffset].GetMethod().DeclaringType;
	    }

	    public Type Pattern { get; } = GetOuterStackFrameType();
#endif
		public const int MaxScore = int.MaxValue;

		public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string NickName { get; set; }
        public string Suffix { get; set; }
        public string DisplayName { get; set; }

        public int Score { get; set; }
    }
}
