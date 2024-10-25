using BusinessObjects.Shared.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Data.Config
{
    internal sealed class TrackArtistConfiguration : BaseConfiguration<Tracks_artist>
    {
        public override void Configure(EntityTypeBuilder<Tracks_artist> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.Track)
                .WithMany(x => x.TrackArtists)
                .HasForeignKey(x => x.TrackId);
            builder.HasOne(x => x.Artist)
                .WithMany(x => x.Tracks_Artists)
                .HasForeignKey(x => x.ArtistId);
        }
    }
}

