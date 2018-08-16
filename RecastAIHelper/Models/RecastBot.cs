using System;
using System.Collections.Generic;

namespace RecastAIHelper.Models
{
    public class RecastBot
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public bool Private_data { get; set; }
        public bool Public { get; set; }
        public bool Is_privatisable { get; set; }
        public int Strictness { get; set; }
        public string Request_token { get; set; }
        public string Developer_token { get; set; }
        public string Children_count { get; set; }
        public int Intents_count { get; set; }
        public int Gazettes_count { get; set; }
        public int Logs_count { get; set; }
        public bool Manual_training { get; set; }
        public bool Big_bot { get; set; }
        public bool Creating { get; set; }
        public int Classifier { get; set; }
        public string User_id { get; set; }
        public User User { get; set; }
        public RecastBotLight Parent { get; set; }
        public List<Intent> Intents { get; set; }
        public List<Actions> Actions { get; set; }
        public List<Gazette> Gazettes { get; set; }
        public Language Language { get; set; }
        public DateTime Created_at { get; set; }
    }
}
