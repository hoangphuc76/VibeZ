using BusinessObjects;
using VibeZDTO;
using Newtonsoft.Json;
namespace VibeZOData.Models
{
    public class SearchModel :INameable
    {
        public IEnumerable<PlaylistDTO> Playlists { get; set; }
        public IEnumerable<AlbumDTO> Albums { get; set; }
        public IEnumerable<ArtistDTO> Artists { get; set; }
        public IEnumerable<TrackDTO> Tracks { get; set; }
        public object TopResult {  get; set; }
        [JsonIgnore]
        public string Name { get; set; }
    }
}
