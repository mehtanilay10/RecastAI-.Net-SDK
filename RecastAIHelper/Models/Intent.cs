using System.Collections.Generic;

namespace RecastAIHelper.Models
{
    public class Intent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public bool Is_activated { get; set; }
        public int Position { get; set; }
        public long Expressions_count { get; set; }
        public long Suggestions_count { get; set; }
        public long Logs_count { get; set; }
        public List<Expression> Expressions { get; set; }
    }
}
