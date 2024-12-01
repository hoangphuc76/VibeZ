using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibeZDTO
{
    public class AdminApprovalDTO
    {
        public Guid TrackId { get; set; }
        public string Image { get; set; }
        public string AlbumName { get; set; }
        public string WriterName { get; set; }
        public DateOnly DateCreated { get; set; }
        public string SongName { get; set; }
        public string Path { get; set; }
    }
}