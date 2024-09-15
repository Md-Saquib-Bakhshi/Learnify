using LMSApi.Models.CourseDTO.GetCourseDTO;

namespace LMSApi.Models.PlaylistDTO.GetPlaylistDTO
{
    public class GetPlaylistDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<GetCourseDto> Courses { get; set; }
    }
}
