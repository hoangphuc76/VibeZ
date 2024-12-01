
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [PrimaryKey(nameof(LibraryId), nameof(ArtistId))]
    public class Library_Artist : BaseEntity
    {
        public Guid LibraryId { get; set; }
        [JsonIgnore]
        public virtual Library Library { get; set; } = null!;
        public Guid ArtistId { get; set; }
        [JsonIgnore]
        public virtual Artist Artist { get; set; } = null!;

    }
}
