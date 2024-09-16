using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Library : BaseEntity
    {
        public Guid Id { get; set; }    
        public Guid ArtistId { get; set; }
        public Guid PlaylistId { get; set; }
        public Guid AlbumId { get; set; }
        public Guid UserId { get; set; }
    }
}
