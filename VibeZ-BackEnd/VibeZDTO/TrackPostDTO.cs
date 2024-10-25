using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibeZDTO
{
    public class TrackPostDTO
    {
        public Guid TrackId { get; set; }
        public Guid AlbumId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Lyrics { get; set; }
        public TimeOnly Time { get; set; }
    }
}
