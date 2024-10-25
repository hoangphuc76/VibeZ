using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibeZDTO
{
    [PrimaryKey(nameof(TrackId), nameof(ArtistId))]
    public class Tracks_artistDTO : BaseEntity
    {
        public Guid TrackId { get; set; }
        public Guid ArtistId { get; set; }
    }
}
