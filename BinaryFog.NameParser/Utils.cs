using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryFog.NameParser
{
    internal static class Utils
    {
        public static string[] GetAllPrefixes()
        {
            string[] parts = (from c in 
                                  Resources.DutchLastNamesPrefixes.Split('\r', '\n').Union(Resources.SpanishLastNamesPrefixes.Split('\r', '\n'))
                              where String.IsNullOrEmpty(c) == false
                              select c).Distinct<string>().ToArray<string>();

            return parts;
        }
    }
}
