using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibeZDTO
{
    public class AdminArtistDTO
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public DateOnly DOB { get; set; }
        public int TotalSong { get; set; }
        public int TotalAlbum { get; set; }
    }
}