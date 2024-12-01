using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace BusinessObjects
{
    public class Album : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid? ArtistId { get; set; }
        [JsonIgnore]
        public virtual Artist Artist { get; set; } = null!;
        public string Name { get; set; }
        public DateOnly DateOfRelease { get; set; }
        public string Image { get; set; }
        public string Nation { get; set; }
        public virtual ICollection<Library_Album>? Library_Albums { get; set; }

        [JsonIgnore]
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
