﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BinaryFog.NameParser.Patterns
{
    internal class FirstNameJobTitlePattern :IPattern
    {
        public ParsedName Parse(string rawName)
        {
            string[] parts = (from c in Resources.JobTitles.Split('\r', '\n')
                              where String.IsNullOrEmpty(c) == false
                              select c).ToArray<string>();


            foreach (string part in parts)
            {
                StringBuilder patternBuilder = new StringBuilder(@"^(?<first>\w+) (?<jobTitle>");
                patternBuilder.Append(part);
                patternBuilder.Append(@"+)$");

                string regexPattern = patternBuilder.ToString();

                Match match = Regex.Match(rawName, regexPattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string jobTitle = match.Groups["jobTitle"].Value;
                    ParsedName pn = new ParsedName()
                    {
                        FirstName = match.Groups["first"].Value,

                        DisplayName = String.Format("{0} {1}", match.Groups["first"].Value, jobTitle),
                        Score = 200
                    };
                    return pn;
                }
            }
            return null;
        }
    }
}
