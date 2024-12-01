using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class TrackListener
    {
        public Guid Id { get; set; } = new Guid();
        public int Listener { get; set; }
        public int SavedTrack {  get; set; }
        public DateOnly Date { get; set; } // Đổi từ DateTime sang DateOnly
        public Guid TrackId { get; set; }

        public Track Track { get; set; }
    }

}
