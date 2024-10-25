using BusinessObjects;
using DataAccess;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class TrackRepository : ITrackRepository
    {
        public async Task<IEnumerable<Track>> GetAllTracks()
        {
            return await TrackDAO.Instance.GetAllTracks();
        }
        public async Task<IEnumerable<Track>> GetAllTrackByAlbumId(Guid id)
        {
            return await TrackDAO.Instance.GetAllTrackByAlbumId(id);
        }

        public async Task<Track> GetTrackById(Guid trackId)
        {
            return await TrackDAO.Instance.GetTrackById(trackId);
        }
        public async Task UpdateListener(Track track)
        {
            await TrackDAO.Instance.UpdateListener(track);
        }

        public async Task AddTrack(Track track)
        {
            await TrackDAO.Instance.Add(track);
        }

        public async Task UpdateTrack(Track track)
        {
            await TrackDAO.Instance.Update(track);
        }

        public async Task DeleteTrack(Track track)
        {
            await TrackDAO.Instance.Delete(track);
        }
        public async Task<IEnumerable<Track>> GetTrackByIds(List<Guid> trackIds) => await TrackDAO.Instance.GetTrackByIds(trackIds);

        public async Task<IEnumerable<Track>> GetSongRecommendations(List<Guid> recentlyPlayedIds, Guid clickedTrackId, int topN = 10)
        {
            return await TrackDAO.Instance.GetSongRecommendations(recentlyPlayedIds, clickedTrackId, topN);
        }


    }
}
