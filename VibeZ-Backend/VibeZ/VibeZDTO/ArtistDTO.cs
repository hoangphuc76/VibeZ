using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibeZDTO
{
    public class ArtistDTO : BaseEntity, INameable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }

        public string ImgBackground { get; set; }
        public string Nation { get; set; }

    }
}
