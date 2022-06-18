namespace GameForum.Domain.Entities
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public List<Post> Posts { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
