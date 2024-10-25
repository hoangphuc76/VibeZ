using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Track : BaseEntity
    {
        public Guid TrackId { get; set; }
        public Guid? AlbumId { get; set; }
        [JsonIgnore]
        public virtual Album Album { get; set; } = null!;
        public Guid? CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; } = null!;
        public string Name { get; set; }
        public string Lyrics { get; set; }
        public string Genre { get; set; }  
        public string Path { get; set; }
        public string Image { get; set; }
        public int Listener { get; set; }

        public TimeOnly Time { get; set; }
        public Guid? ArtistId { get; set; }
        [JsonIgnore]
        public virtual Artist Artist { get; set; } = null!;
        public virtual ICollection<Like>? Likes { get; set; }
        public virtual ICollection<Track_Playlist>? TrackPlayLists { get; set; }


    }
}
