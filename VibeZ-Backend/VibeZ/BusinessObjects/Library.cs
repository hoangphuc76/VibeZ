using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Library : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Library_Artist>? Library_Artist { get; set; }
        [JsonIgnore]
        public virtual ICollection<Library_Album>? Library_Albums { get; set; }
        [JsonIgnore]
        public virtual ICollection<Library_Playlist>? Library_Playlists { get; set; }
    }
}