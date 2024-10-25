using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibeZDTO
{
    [PrimaryKey(nameof(UserId), nameof(ArtistId))]
    public class BlockedArtistDTO
    {
        public Guid UserId { get; set; }
        public Guid ArtistId { get; set; }
        public DateTime blocked_date { get; set; }
    }
}
