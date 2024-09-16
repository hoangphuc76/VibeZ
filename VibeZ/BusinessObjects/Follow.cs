using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [PrimaryKey(nameof(UserId), nameof(ArtistId))]
    public class Follow : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ArtistId { get; set; }

    }
}
