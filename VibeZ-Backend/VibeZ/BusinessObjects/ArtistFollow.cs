using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    
        public class ArtistFollow
        {
            public Guid Id { get; set; } = new Guid();
            public int TotalFollow { get; set; }
            public int TotalUnfollow { get; set; }
            public DateOnly Date { get; set; } // Đổi từ DateTime sang DateOnly
            public Guid ArtistId { get; set; }

            public Artist artist { get; set; }

        }

    

}
