using System;

namespace RecastAIHelper.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Nickname { get; set; }
        public string Image { get; set; }
        public string Role { get; set; }
        public string Slug { get; set; }
        public DateTime Created_at { get; set; }
        public int Bots_count { get; set; }
        public int Collaborations_count { get; set; }
    }
}
