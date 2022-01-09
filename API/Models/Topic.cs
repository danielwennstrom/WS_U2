using System;

namespace API.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public Guid TopicId { get; set; }
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastPostDateTime { get; set; }
        public string PostBody { get; set; }
    }
}