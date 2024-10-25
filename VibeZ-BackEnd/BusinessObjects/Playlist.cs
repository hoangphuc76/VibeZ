using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Playlist : BaseEntity
    {
        public Guid PlaylistId { get; set; }
        public string Name  { get; set; }

        public string Image { get; set; }
        public string Description { get; set; }
        public string createBy { get; set; }
        public Guid? UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Library_Playlist>? Library_Playlists { get; set; }
        public virtual ICollection<Track_Playlist> TrackPlayLists{ get; set; }

    }
}
