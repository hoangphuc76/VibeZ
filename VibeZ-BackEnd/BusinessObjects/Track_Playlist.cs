using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [PrimaryKey(nameof(TrackId), nameof(PlaylistId))]
    public class Track_Playlist : BaseEntity
    {
        public Guid TrackId { get; set; }
        public Track Track { get; set; } = null!;
        public Guid PlaylistId { get; set; }
        public Playlist Playlist { get; set; } = null!;
    }
}