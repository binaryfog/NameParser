using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryFog.NameParser
{
    public class ParsedName
    {
        public const int MaxScore = Int32.MaxValue;

        public ParsedName() 
        {
 
        }

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
