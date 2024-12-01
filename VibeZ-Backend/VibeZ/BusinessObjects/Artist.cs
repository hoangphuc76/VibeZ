using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Artist : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Image { get; set; }
        public string ImgBackground { get; set; }
        public string Nation { get; set; }
        public string? Email {  get; set; }
        //public virtual ICollection<TrackArtist> TrackArtists {  get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public virtual ICollection<Follow>? Follow { get; set; }
        public virtual ICollection<BlockedArtist>? BlockedArtists { get; set; }
        public virtual ICollection<Album>? Albums { get; set; }
        public virtual ICollection<Track>? Tracks { get; set; }
        public virtual ICollection<Library_Artist>? Library_Artist { get; set; }

    }
}
