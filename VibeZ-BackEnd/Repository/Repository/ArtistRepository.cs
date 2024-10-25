using BusinessObjects;
using DataAccess;
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        public async Task<IEnumerable<Artist>> GetAllArtists()
        {
            return await ArtistDAO.Instance.GetAllArtists();
        }

        public async Task<Artist> GetArtistById(Guid artistId)
        {
            return await ArtistDAO.Instance.GetArtistById(artistId);
        }

        public async Task AddArtist(Artist artist)
        {
            await ArtistDAO.Instance.Add(artist);
        }

        public async Task UpdateArtist(Artist artist)
        {
            await ArtistDAO.Instance.Update(artist);
        }

        public async Task DeleteArtist(Artist artist)
        {
            await ArtistDAO.Instance.Delete(artist);
        }
        public async Task<IEnumerable<Track>> GetAllTrackByArtistId(Guid artistId)
        {
           return await ArtistDAO.Instance.GetAllTrackByArtistId(artistId);
        }
    }
}
