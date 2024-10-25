using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Data.Config
{
    internal sealed class TrackPlaylistConfiguration : BaseConfiguration<Track_Playlist>
    {
         public override void Configure(EntityTypeBuilder<Track_Playlist> builder)
        {
            base.Configure(builder);
            builder.HasKey(e => new { e.TrackId, e.PlaylistId });
            builder.HasOne(x => x.Playlist)
                .WithMany(x => x.TrackPlayLists)
                .HasForeignKey(x => x.PlaylistId);
            builder.HasOne(x => x.Track)
                .WithMany(x => x.TrackPlayLists)
                .HasForeignKey(x => x.TrackId);
        }
    }
}
