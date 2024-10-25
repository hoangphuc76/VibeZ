using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [PrimaryKey(nameof(LibraryId), nameof(AlbumId))]
    public class Library_Album : BaseEntity
    {
        public Guid LibraryId { get; set; }
        [JsonIgnore]
        public virtual Library Library { get; set; } = null!;
        public Guid AlbumId { get; set; }
        [JsonIgnore]
        public virtual Album Album { get; set; } = null!;
    }
}