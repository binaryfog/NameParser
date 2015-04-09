using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryFog.NameParser
{
    public interface IPattern
    {
        ParsedName Parse( string rawName);
    }
}
