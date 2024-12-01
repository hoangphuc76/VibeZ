using BusinessObjects.Shared.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Data.Config
{
    internal sealed class BlockedArtistConfiguration : BaseConfiguration<BlockedArtist>
    {
        public override void Configure(EntityTypeBuilder<BlockedArtist> builder)
        {
            base.Configure(builder);;
            builder.HasOne(x => x.Artist)
                .WithMany(x => x.BlockedArtists)
                .HasForeignKey(x => x.ArtistId);
            builder.HasOne(x => x.User)
               .WithMany(x => x.BlockedArtists)
               .HasForeignKey(x => x.UserId);
        }
    }
}
