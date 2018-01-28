using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;


namespace BinaryFog.NameParser.Tests
{
    public class USRepresentativesTests
    {

        public static IEnumerable<object[]> GetUSRepresentatives()
        {
            var j = JObject.Parse(DataFiles.GetJsonString("USRepresentatives.json"));
            
            return ( from e in j["objects"]
                     select new object[] 
                     {
                         e["person"]["firstname"].ToString(),
                         e["person"]["lastname"].ToString(),
                         e["person"]["name"].ToString(),
                         e["person"]["nickname"].ToString(),
                     }).ToArray();
        }

        [Theory]
		[MemberData(nameof(GetUSRepresentatives),DisableDiscoveryEnumeration = true)]
		public void GetUSRepresentatives_Test( string firstName, string lastName, string name, string nickname)
        {
            //ARRANGE
            //Remove party suffix
            int i = name.IndexOf('[');
            string nameWithoutParty = name.Substring(0, i).Trim();

            //ACT
            var target = new FullNameParser(nameWithoutParty);
            target.Parse();

            //ASSERT
            Assert.Equal( firstName, target.FirstName);
	        Assert.Equal( lastName, target.LastName);

            if( lastName == "González-Colón")
                Assert.Equal("Commish.", target.Title);
            else
                Assert.Equal("Rep.", target.Title);

            if (String.IsNullOrEmpty( nickname ))
                Assert.Null(target.NickName);
            else
                Assert.Equal(nickname, target.NickName);
        }

        [Fact(Skip ="Pattern for nicknames with commas inside them needs to be added.")]
        public void Parse_GeorgeButterfield()
        {
            var fullName = "Rep. George “G.K.” Butterfield";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("George", target.FirstName);
            Assert.Equal("Butterfield", target.LastName);
            Assert.Equal("George Butterfield", target.DisplayName);
            Assert.Equal("Rep.", target.Title);
            Assert.Equal("G.K.", target.NickName);
        }



    }
}
