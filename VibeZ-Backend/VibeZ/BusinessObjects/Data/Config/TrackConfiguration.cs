using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BusinessObjects.Shared.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BusinessObjects.Data.Config
{
    internal sealed class TrackConfiguration : BaseConfiguration<Track>
    {
        public override void Configure(EntityTypeBuilder<Track> builder)
        {
            base.Configure(builder);
            builder.HasKey(e => e.TrackId);
            builder.Property(e => e.TrackId)
                .HasDefaultValueSql(UniqueType.Algorithm)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasMaxLength(DataSchemaLength.Medium)
                .IsRequired();
            builder.Property(p => p.Path)
                .HasMaxLength(DataSchemaLength.SuperLarge)
                .IsRequired();
            builder.Property(p => p.Image)
                .HasMaxLength(DataSchemaLength.SuperLarge)
                .IsRequired();
            builder.Property(p => p.Genre)
                .HasMaxLength(DataSchemaLength.Large)
                .IsRequired();
            builder.HasOne(x => x.Album)
                .WithMany(x => x.Tracks)
                .HasForeignKey(x => x.AlbumId);
            builder.HasOne(x => x.Category)
                .WithMany(x => x.Tracks)
                .HasForeignKey(x =>x.CategoryId);
            builder.HasMany(x => x.Likes)
                .WithOne(x => x.Track)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(x => x.TrackId);
            builder.HasOne(x => x.Artist)
                .WithMany(x => x.Tracks)
                .HasForeignKey(x => x.TrackId);
        }
    }
}
