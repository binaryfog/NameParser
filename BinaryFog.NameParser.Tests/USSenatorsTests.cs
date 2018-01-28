using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;


namespace BinaryFog.NameParser.Tests
{
    public class USSenatorsTests
    {

        public static IEnumerable<object[]> GetUSSenators()
        {
            var j = JObject.Parse(DataFiles.GetJsonString("USSenators.json"));
            
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
		[MemberData(nameof(GetUSSenators),DisableDiscoveryEnumeration = true)]
		public void GetUSSenators_Test( string firstName, string lastName, string name, string nickname)
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
            Assert.Equal("Sen.", target.Title);
            if (String.IsNullOrEmpty( nickname ))
                Assert.Null(target.NickName);
            else
                Assert.Equal(nickname, target.NickName);
        }

        [Fact]
        public void Parse_TammyBaldwin()
        {
            var fullName = "Sen Tammy Baldwin";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Tammy", target.FirstName);
            Assert.Equal("Baldwin", target.LastName);
            Assert.Equal("Tammy Baldwin", target.DisplayName);
            Assert.Equal("Sen", target.Title);
        }


        [Fact]
        public void Parse_BernieSanders()
        {
            var fullName = "Sen. Bernard “Bernie” Sanders";
            var target = new FullNameParser(fullName);
            target.Parse();

            Assert.Equal("Bernard", target.FirstName);
            Assert.Equal("Sanders", target.LastName);
            Assert.Equal("Bernard Sanders", target.DisplayName);
            Assert.Equal("Sen.", target.Title);
            Assert.Equal("Bernie", target.NickName);
        }


    }
}
