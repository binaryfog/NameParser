using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryFog.NameParser.Patterns
{
    internal class CompanyPattern : IPattern
    {

        public ParsedName Parse(string rawName)
        {
            if (rawName.ToLower().EndsWith(" ltd") ||
                rawName.ToLower().EndsWith(" ltd.") ||
                rawName.ToLower().EndsWith(" inc") ||
                rawName.ToLower().EndsWith(" inc.") ||
                rawName.ToLower().EndsWith(" limited") ||
                rawName.ToLower().EndsWith(" limited."))
            {
                ParsedName pn = new ParsedName() 
                {
                    DisplayName = rawName,
                    Score = ParsedName.MaxScore
                };

                return pn;
            }

            return null;
        }
    }
}
