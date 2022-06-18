namespace GameForum.Domain.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public List<Topic> Topics { get; set; }
    }
}
