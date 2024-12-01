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
    internal sealed class AlbumConfiguration : BaseConfiguration<Album>
    {
        public override void Configure(EntityTypeBuilder<Album> builder)
        {
            base.Configure(builder);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasDefaultValueSql(UniqueType.Algorithm)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasMaxLength(DataSchemaLength.Medium)
                .IsRequired();
            builder.Property(p => p.Image)
                .HasMaxLength(DataSchemaLength.SuperLarge)
                .IsRequired();
            builder.Property(p => p.Image)
                .HasMaxLength(DataSchemaLength.SuperLarge)
                .IsRequired();
            builder.HasOne(x => x.Artist)
                .WithMany(x => x.Albums)
                .HasForeignKey(x => x.ArtistId);
            builder.HasMany(x => x.Library_Albums)
                .WithOne(x => x.Album)
                .HasForeignKey(x => x.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
