namespace RecastAIHelper.Models
{
    public class RecastBotLight
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public bool Private_data { get; set; }
        public bool Public { get; set; }
        public string Children_count { get; set; }
        public int Intents_count { get; set; }
        public int Gazettes_count { get; set; }
        public int Logs_count { get; set; }
        public bool Is_privatisable { get; set; }
        public string Parent { get; set; }
        public string User_id { get; set; }
    }
}
