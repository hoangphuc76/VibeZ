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
    internal sealed class ArtistConfiguration : BaseConfiguration<Artist>
    {
        public override void Configure(EntityTypeBuilder<Artist> builder)
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
            builder.Property(p => p.ImgBackground)
               .HasMaxLength(DataSchemaLength.SuperLarge)
               .IsRequired();
            builder.Property(p => p.Nation)
               .HasMaxLength(DataSchemaLength.Medium)
               .IsRequired();
            builder.HasMany(x => x.Follow)
                .WithOne(x => x.Artist)
                .HasForeignKey(x => x.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.BlockedArtists)
                .WithOne(x => x.Artist)
                .HasForeignKey(x => x.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Albums)
                .WithOne(x => x.Artist)
                .HasForeignKey(x => x.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Tracks)
                .WithOne(x => x.Artist)
                .HasForeignKey(x => x.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Library_Artist)
            .WithOne(x => x.Artist)
            .HasForeignKey(x => x.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
