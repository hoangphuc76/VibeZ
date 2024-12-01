using BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibeZDTO
{
    public class ArtistPendingDTO : BaseEntity
    {
        public Guid Id { get; set; } = new Guid();
        public string ArtistName { get; set; }
        public string Genre { get; set; }
        public string Email { get; set; }
        public string SongName { get; set; }
        public string Nation {  get; set; }

        public string Song { get; set; }
        public string LyricLRC { get; set; }
        public string Lyrics { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Section { get; set; }
        public string Image { get; set; }
        public string ImgBackground { get; set; }
      
        public string SongImg {  get; set; }
        public Guid UserId { get; set; }
    }
}
