using LMSApi.Models;
using LMSApi.Models.PlaylistDTO;
using LMSApi.Models.PlaylistDTO.CreatePlaylistDTO;
using LMSApi.Models.PlaylistDTO.UpdatePlaylistDTO;
using LMSApi.Repositories.PlaylistRepository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LMSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        // POST: api/Playlist
        [HttpPost]
        public async Task<IActionResult> AddPlaylist([FromBody] CreatePlaylistDto playlistDto)
        {
            var result = await _playlistService.AddPlaylist(playlistDto);

            if (result.Status == "Success")
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // GET: api/Playlist
        [HttpGet]
        public async Task<IActionResult> GetAllPlaylists()
        {
            var result = await _playlistService.GetAllPlaylists();

            if (result.Status == "Success")
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // GET: api/Playlist/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaylist(int id)
        {
            var result = await _playlistService.GetPlaylist(id);

            if (result.Status == "Success")
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // PUT: api/Playlist/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylist(int id, [FromBody] UpdatePlaylistDto playlistDto)
        {
            var result = await _playlistService.UpdatePlaylist(id, playlistDto);

            if (result.Status == "Success")
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        // DELETE: api/Playlist/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            var result = await _playlistService.DeletePlaylist(id);

            if (result.Status == "Success")
            {
                return Ok(result);
            }

            return NotFound(result);
        }
    }
}
