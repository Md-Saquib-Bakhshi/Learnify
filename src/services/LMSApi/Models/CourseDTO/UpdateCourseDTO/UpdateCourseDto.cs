namespace LMSApi.Models.CourseDTO.UpdateCourseDTO
{
    public class UpdateCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public int PlaylistId { get; set; }
    }
}
