namespace DataAccess.Models
{
    public class CareerItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;

        public Guid CourseId { get; set; }
        public Course Course { get; set; } = new Course();
    }
}
