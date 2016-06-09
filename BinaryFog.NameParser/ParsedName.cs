namespace BinaryFog.NameParser {
	public class ParsedName
    {
        public const int MaxScore = int.MaxValue;
        private string _GeneratedBy;

        public ParsedName()
        {

        }

        public ParsedName( string generatedBy)
        {
            _GeneratedBy = generatedBy;
        }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string NickName { get; set; }
        public string Suffix { get; set; }
        public string DisplayName { get; set; }

        public int Score { get; set; }

        public string GeneratedBy { get { return _GeneratedBy; } }
    }
}
