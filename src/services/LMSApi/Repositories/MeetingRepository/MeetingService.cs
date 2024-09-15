using LMSApi.Data;
using LMSApi.Models;
using LMSApi.Models.MeetingDTO;
using LMSApi.Models.MeetingDTO.CreateMeetingRequestDTO;
using LMSApi.Models.MeetingDTO.GetMeetingRequestDTO;
using LMSApi.Models.MeetingDTO.MeetingResponseDTO;
using LMSApi.Repositories.MeetingRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSApi.Repositories.MeetingRepository
{
    public class MeetingService : IMeetingService
    {
        private readonly LMSDbContext _context;

        public MeetingService(LMSDbContext context)
        {
            _context = context;
        }

        // Create a new meeting request
        public async Task<ResponseWithData<bool>> CreateMeetingRequest(CreateMeetingRequestDto meetingRequestDto)
        {
            var existingRequest = await _context.MeetingRequests
                .FirstOrDefaultAsync(mr => mr.Email == meetingRequestDto.Email && mr.MeetingLink == null && mr.Time == null);

            if (existingRequest != null)
            {
                return new ResponseWithData<bool>
                {
                    Status = "Failure",
                    Message = "A pending meeting request already exists.",
                    Data = false
                };
            }

            var meetingRequest = new MeetingRequest
            {
                Email = meetingRequestDto.Email,
                RequestMessage = meetingRequestDto.RequestMessage,
                RequestStatus = "Pending"
            };

            _context.MeetingRequests.Add(meetingRequest);
            var result = await _context.SaveChangesAsync() > 0;

            return new ResponseWithData<bool>
            {
                Status = result ? "Success" : "Failure",
                Message = result ? "Meeting request created successfully." : "Failed to create meeting request.",
                Data = result
            };
        }

        // Get all meeting requests
        public async Task<ResponseWithData<IEnumerable<GetMeetingRequestDto>>> GetAllRequests()
        {
            var requests = await _context.MeetingRequests
                .Select(mr => new GetMeetingRequestDto
                {
                    Id = mr.Id,
                    Email = mr.Email,
                    RequestMessage = mr.RequestMessage,
                    RequestStatus = mr.RequestStatus,
                    MeetingLink = mr.MeetingLink,
                    Time = mr.Time
                })
                .ToListAsync();

            return new ResponseWithData<IEnumerable<GetMeetingRequestDto>>
            {
                Status = "Success",
                Message = "Fetched all meeting requests successfully.",
                Data = requests
            };
        }

        // Get a specific meeting request by student email
        public async Task<ResponseWithData<GetMeetingRequestDto>> GetByStudentEmail(string email)
        {
            var request = await _context.MeetingRequests
                .Where(mr => mr.Email == email)
                .Select(mr => new GetMeetingRequestDto
                {
                    Id = mr.Id,
                    Email = mr.Email,
                    RequestMessage = mr.RequestMessage,
                    RequestStatus = mr.RequestStatus,
                    MeetingLink = mr.MeetingLink,
                    Time = mr.Time
                })
                .FirstOrDefaultAsync();

            if (request == null)
            {
                return new ResponseWithData<GetMeetingRequestDto>
                {
                    Status = "Failure",
                    Message = "No meeting request found for this email.",
                    Data = null
                };
            }

            return new ResponseWithData<GetMeetingRequestDto>
            {
                Status = "Success",
                Message = "Fetched meeting request successfully.",
                Data = request
            };
        }

        // Admin fills in the meeting details (link and time)
        public async Task<ResponseWithData<bool>> MakeMeetingResponse(MeetingResponseDto meetingResponseDto)
        {
            var meetingRequest = await _context.MeetingRequests
                .FirstOrDefaultAsync(mr => mr.Email == meetingResponseDto.Email);

            if (meetingRequest == null)
            {
                return new ResponseWithData<bool>
                {
                    Status = "Failure",
                    Message = "No meeting request found for this email.",
                    Data = false
                };
            }

            // Update the meeting request with the provided details
            meetingRequest.MeetingLink = meetingResponseDto.MeetingLink;
            meetingRequest.Time = meetingResponseDto.Time;
            meetingRequest.RequestStatus = "Confirmed";

            _context.MeetingRequests.Update(meetingRequest);
            var result = await _context.SaveChangesAsync() > 0;

            return new ResponseWithData<bool>
            {
                Status = result ? "Success" : "Failure",
                Message = result ? "Meeting response updated successfully." : "Failed to update meeting response.",
                Data = result
            };
        }

        // Check if a student has a pending meeting request
        public async Task<ResponseWithData<bool>> HasPendingRequest(string email)
        {
            var existingRequest = await _context.MeetingRequests
                .FirstOrDefaultAsync(mr => mr.Email == email && mr.MeetingLink == null && mr.Time == null);

            return new ResponseWithData<bool>
            {
                Status = existingRequest != null ? "Success" : "Failure",
                Message = existingRequest != null ? "There is a pending meeting request." : "No pending meeting request.",
                Data = existingRequest != null
            };
        }
    }
}
