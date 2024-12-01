using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Data.Config
{
    internal sealed class Library_ArtistConfiguration : BaseConfiguration<Library_Artist>
    {
        public override void Configure(EntityTypeBuilder<Library_Artist> builder)
        {
            base.Configure(builder); ;
            builder.HasOne(x => x.Library)
                .WithMany(x => x.Library_Artist)
                .HasForeignKey(x => x.LibraryId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Artist)
                .WithMany(x => x.Library_Artist)
                .HasForeignKey(x => x.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
