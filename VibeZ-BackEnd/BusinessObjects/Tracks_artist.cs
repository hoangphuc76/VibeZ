using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [PrimaryKey(nameof(TrackId),nameof(ArtistId))]
    public class Tracks_artist : BaseEntity
    {
        public Guid TrackId { get; set; }
        [JsonIgnore]
        public virtual  Track Track { get; set; } = null!;
        public Guid ArtistId { get; set; }
        [JsonIgnore]
        public virtual Artist Artist { get; set; } = null!;
    }
}
