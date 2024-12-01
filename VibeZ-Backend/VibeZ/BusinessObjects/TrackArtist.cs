using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [PrimaryKey(nameof(TrackId), nameof(ArtistId))]
    public class TrackArtist : BaseEntity
    {
        public Guid TrackId {  get; set; }
        public Track Track { get; set; } = null!;
        public Guid ArtistId {  get; set; }
        public Artist Artist { get; set; }
    }
}
