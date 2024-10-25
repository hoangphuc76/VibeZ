using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibeZDTO
{
    [PrimaryKey(nameof(TrackId), nameof(PlaylistId))]
    public class TrackPlayListDTO : BaseEntity
    {
        public Guid TrackId { get; set; }
        public Guid PlaylistId { get; set; }
    }
}
