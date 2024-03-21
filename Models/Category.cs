namespace DataAccess.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public int Order { get; set; }
        public string Description { get; set; } = null!;
        public bool Featured { get; set; }
    }
}
