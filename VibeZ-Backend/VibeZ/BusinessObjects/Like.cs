using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [PrimaryKey(nameof(UserId), nameof(TrackId))]
    public class Like : BaseEntity
    {
        public Guid UserId { get; set; }
        [JsonIgnore]
        public virtual Track Track { get; set; } = null!;
        public Guid TrackId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
    }
}
