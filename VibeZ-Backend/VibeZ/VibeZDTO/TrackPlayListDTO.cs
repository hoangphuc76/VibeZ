using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibeZDTO
{
    public class TrackPlayListDTO 
    {
        public Guid TrackId { get; set; }
        public Guid PlaylistId { get; set; }
        public DateOnly CreateDate { get; set; }
        public DateOnly UpdateDate {  get; set; }
    }
}
