using LMSApi.Models;
using LMSApi.Models.MeetingDTO;
using LMSApi.Models.MeetingDTO.CreateMeetingRequestDTO;
using LMSApi.Models.MeetingDTO.GetMeetingRequestDTO;
using LMSApi.Models.MeetingDTO.MeetingResponseDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSApi.Repositories.MeetingRepository
{
    public interface IMeetingService
    {
        Task<ResponseWithData<bool>> CreateMeetingRequest(CreateMeetingRequestDto meetingRequestDto);
        Task<ResponseWithData<IEnumerable<GetMeetingRequestDto>>> GetAllRequests();
        Task<ResponseWithData<GetMeetingRequestDto>> GetByStudentEmail(string email);
        Task<ResponseWithData<bool>> MakeMeetingResponse(MeetingResponseDto meetingResponseDto);
        Task<ResponseWithData<bool>> HasPendingRequest(string email);
    }
}
