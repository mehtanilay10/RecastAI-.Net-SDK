using System.Collections.Generic;

namespace RecastAIHelper.Models
{
    public class Expression
    {
        public string Id { get; set; }
        public string Source { get; set; }
        public Language Language { get; set; }
        public List<Token> Tokens { get; set; }

        public Expression()
        {

        }

        public Expression(string source, string isoCode)
        {
            this.Source = source;
            this.Language = new Language { Isocode = isoCode };
        }
    }
}
