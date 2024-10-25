using BusinessObjects;
using VibeZDTO;

namespace VibeZOData.Models
{
    public class LibraryDetails
    {
        public IEnumerable<PlaylistDTO> Playlists { get; set; }
        public IEnumerable<AlbumDTO> Albums { get; set; }
        public IEnumerable<ArtistDTO> Artists { get; set; }
    }
}
