namespace LMSApi.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation property for the related courses
        public ICollection<Course> Courses { get; set; }
    }
}
