
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [PrimaryKey(nameof(LibraryId), nameof(PlaylistId))]
    public class Library_Playlist : BaseEntity
    {
        public Guid LibraryId { get; set; }
        [JsonIgnore]
        public virtual Library Library { get; set; } = null!;
        public Guid PlaylistId { get; set; }
        [JsonIgnore]
        public virtual Playlist Playlist { get; set; } = null!;

    }
}
