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
    internal sealed class PlaylistConfiguration : BaseConfiguration<Playlist>
    {
        public override void Configure(EntityTypeBuilder<Playlist> builder)
        {
            base.Configure(builder);
            builder.HasKey(e => e.PlaylistId);
            builder.Property(e => e.PlaylistId)
                .HasDefaultValueSql(UniqueType.Algorithm)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasMaxLength(DataSchemaLength.Medium)
                .IsRequired();
             builder.Property(p => p.Description)
                .HasMaxLength(DataSchemaLength.Large)
                .IsRequired();
            builder.Property(p => p.createBy)
                .HasMaxLength(DataSchemaLength.Medium)
                .IsRequired();
            builder.Property(p => p.Image)
                .HasMaxLength(DataSchemaLength.SuperLarge)
                .IsRequired();
            builder.HasOne(x => x.User)
                .WithMany(x => x.Playlists)
                .HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.TrackPlayLists)
                .WithOne(x => x.Playlist)
                .HasForeignKey(x => x.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Library_Playlists)
                .WithOne(x => x.Playlist)
                .HasPrincipalKey(x => x.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
