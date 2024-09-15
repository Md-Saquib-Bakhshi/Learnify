using LMSApi.Models;
using LMSApi.Models.PlaylistDTO;
using LMSApi.Models.PlaylistDTO.CreatePlaylistDTO;
using LMSApi.Models.PlaylistDTO.GetPlaylistDTO;
using LMSApi.Models.PlaylistDTO.UpdatePlaylistDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSApi.Repositories.PlaylistRepository
{
    public interface IPlaylistService
    {
        Task<ResponseWithData<bool>> AddPlaylist(CreatePlaylistDto playlistDto);
        Task<ResponseWithData<IEnumerable<GetPlaylistDto>>> GetAllPlaylists();
        Task<ResponseWithData<GetPlaylistDto>> GetPlaylist(int id);
        Task<ResponseWithData<bool>> UpdatePlaylist(int id, UpdatePlaylistDto playlistDto);
        Task<ResponseWithData<bool>> DeletePlaylist(int id);
    }
}
