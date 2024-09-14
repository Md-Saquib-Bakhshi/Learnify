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

        public Course()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
