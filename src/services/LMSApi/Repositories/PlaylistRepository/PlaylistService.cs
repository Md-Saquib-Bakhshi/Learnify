using LMSApi.Data;
using LMSApi.Models;
using LMSApi.Models.CourseDTO.GetCourseDTO;
using LMSApi.Models.PlaylistDTO;
using LMSApi.Models.PlaylistDTO.CreatePlaylistDTO;
using LMSApi.Models.PlaylistDTO.GetPlaylistDTO;
using LMSApi.Models.PlaylistDTO.UpdatePlaylistDTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSApi.Repositories.PlaylistRepository
{
    public class PlaylistService : IPlaylistService
    {
        private readonly LMSDbContext _context;

        public PlaylistService(LMSDbContext context)
        {
            _context = context;
        }

        // Add a new playlist
        public async Task<ResponseWithData<bool>> AddPlaylist(CreatePlaylistDto playlistDto)
        {
            var response = new ResponseWithData<bool>();

            try
            {
                var playlist = new Playlist
                {
                    Name = playlistDto.Name,
                    Description = playlistDto.Description
                };

                await _context.Playlists.AddAsync(playlist);
                await _context.SaveChangesAsync();

                response.Data = true;
                response.Status = "Success";
                response.Message = "Playlist added successfully.";
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Status = "Error";
                response.Message = ex.Message;
            }

            return response;
        }

        // Get all playlists
        public async Task<ResponseWithData<IEnumerable<GetPlaylistDto>>> GetAllPlaylists()
        {
            var response = new ResponseWithData<IEnumerable<GetPlaylistDto>>();

            try
            {
                var playlists = await _context.Playlists
                    .Select(p => new GetPlaylistDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Courses = p.Courses.Select(c => new GetCourseDto
                        {
                            Id = c.Id,
                            Title = c.Title,
                            Link = c.Link
                        }).ToList()
                    })
                    .ToListAsync();

                response.Data = playlists;
                response.Status = "Success";
                response.Message = "Playlists retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Status = "Error";
                response.Message = ex.Message;
            }

            return response;
        }

        // Get playlist by ID
        public async Task<ResponseWithData<GetPlaylistDto>> GetPlaylist(int id)
        {
            var response = new ResponseWithData<GetPlaylistDto>();

            try
            {
                var playlist = await _context.Playlists
                    .Where(p => p.Id == id)
                    .Select(p => new GetPlaylistDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Courses = p.Courses.Select(c => new GetCourseDto
                        {
                            Id = c.Id,
                            Title = c.Title,
                            Link = c.Link
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (playlist == null)
                {
                    response.Data = null;
                    response.Status = "Error";
                    response.Message = "Playlist not found.";
                }
                else
                {
                    response.Data = playlist;
                    response.Status = "Success";
                    response.Message = "Playlist retrieved successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Status = "Error";
                response.Message = ex.Message;
            }

            return response;
        }

        // Update playlist
        public async Task<ResponseWithData<bool>> UpdatePlaylist(int id, UpdatePlaylistDto playlistDto)
        {
            var response = new ResponseWithData<bool>();

            try
            {
                var playlist = await _context.Playlists.FindAsync(id);
                if (playlist == null)
                {
                    response.Data = false;
                    response.Status = "Error";
                    response.Message = "Playlist not found.";
                    return response;
                }

                // Update playlist details
                playlist.Name = playlistDto.Name;
                playlist.Description = playlistDto.Description;

                _context.Playlists.Update(playlist);
                await _context.SaveChangesAsync();

                response.Data = true;
                response.Status = "Success";
                response.Message = "Playlist updated successfully.";
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Status = "Error";
                response.Message = ex.Message;
            }

            return response;
        }

        // Delete playlist
        public async Task<ResponseWithData<bool>> DeletePlaylist(int id)
        {
            var response = new ResponseWithData<bool>();

            try
            {
                var playlist = await _context.Playlists.FindAsync(id);
                if (playlist == null)
                {
                    response.Data = false;
                    response.Status = "Error";
                    response.Message = "Playlist not found.";
                    return response;
                }

                _context.Playlists.Remove(playlist);
                await _context.SaveChangesAsync();

                response.Data = true;
                response.Status = "Success";
                response.Message = "Playlist deleted successfully.";
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Status = "Error";
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
