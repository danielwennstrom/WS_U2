namespace WS_U2.Models
{
    public class Post
    {
        public int Id { get; set; }
        public Guid TopicId { get; set; }
        public string PostedBy { get; set; }
        public DateTime PostedDateTime { get; set; }
        public string PostBody { get; set; }
    }
}