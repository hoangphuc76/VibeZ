using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibeZDTO
{
    public class AdminHomeDTO
    {
        public IEnumerable<AdminArtistDTO> DataTable { get; set; }
        public TotalDataDTO DataTotal { get; set; }
    }
}