using System.ComponentModel.DataAnnotations;

namespace LMSApi.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public DateTime CreatedAt { get; set; }

        // Foreign key for Playlist
        public int PlaylistId { get; set; }

        // Navigation property
        public Playlist Playlist { get; set; }

        public Course()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
