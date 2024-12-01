using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibeZDTO
{
    public class TrackDTO : BaseEntity, INameable
    {
        public Guid TrackId { get; set; }
        public Guid? AlbumId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? artistId { get; set; }
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public string Name { get; set; }
        public string Lyrics { get; set; }
        public string Genre {  get; set; }
        public string Path { get; set; }
        public string TrackLRC {  get; set; }
        public string Image { get; set; }
        public string SongInfoImg {  get; set; }
        public bool PendingApproval { get; set; }
        public int Listener { get; set; }
        public TimeOnly Time { get; set; }
        public DateOnly CreateDate { get; set; }    



    }
}
