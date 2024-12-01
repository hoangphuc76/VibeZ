using BusinessObjects.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Data.Config
{
    internal sealed class LibraryConfiguration : BaseConfiguration<Library>
    {
        public override void Configure(EntityTypeBuilder<Library> builder)
        {
            base.Configure(builder);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasDefaultValueSql(UniqueType.Algorithm)
                .ValueGeneratedOnAdd();

            builder.HasOne(x => x.User)
                  .WithOne(x => x.Library)
                  .HasForeignKey<Library>(e => e.UserId)
                  .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Library_Albums)
                .WithOne(x => x.Library)
                .HasForeignKey(x => x.LibraryId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Library_Playlists)
               .WithOne(x => x.Library)
               .HasForeignKey(x => x.LibraryId)
               .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Library_Artist)
              .WithOne(x => x.Library)
              .HasForeignKey(x => x.LibraryId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
