using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryFog.NameParser
{
    public class ParsedName
    {
        public ParsedName() 
        {
 
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string NickName { get; set; }
        public string Suffix { get; set; }
    }
}
