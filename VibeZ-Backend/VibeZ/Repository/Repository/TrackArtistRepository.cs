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
    public class TrackArtistRepository : ITracksArtistRepository
    {
        public async Task<IEnumerable<Tracks_artist>> GetAllTracksArtist()
        {
            return await TracksArtistDAO.Instance.GetAllTracksArtist();
        }

        public async Task<Tracks_artist> GetTracksArtistsById(Guid trackArtistId, Guid artistId)
        {
            return await TracksArtistDAO.Instance.GetTracksArtistById(trackArtistId,artistId);
        }

        public async Task AddTracksArtist(Tracks_artist trackArtist)
        {
            await TracksArtistDAO.Instance.Add(trackArtist);
        }

        public async Task UpdateTracksArtist(Tracks_artist trackArtist)
        {
            await TracksArtistDAO.Instance.Update(trackArtist);
        }

        public async Task DeleteTracksArtist(Guid trackId, Guid artistId)
        {
            await TracksArtistDAO.Instance.Delete(trackId, artistId);
        }
    }

}
