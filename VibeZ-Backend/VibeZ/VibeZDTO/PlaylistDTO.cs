using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibeZDTO
{
    public class PlaylistDTO : BaseEntity, INameable
    {
        public Guid PlaylistId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string CreateBy { get; set; }
        public string Image { get; set; }
        public Guid? UserId { get; set; }

    }
}
