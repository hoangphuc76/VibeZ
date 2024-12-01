using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Data.Config
{
    internal class Library_PlaylistConfiguration : BaseConfiguration<Library_Playlist>
    {
        public override void Configure(EntityTypeBuilder<Library_Playlist> builder)
        {
            base.Configure(builder); ;
            builder.HasOne(x => x.Library)
                .WithMany(x => x.Library_Playlists)
                .HasForeignKey(x => x.LibraryId)
                .OnDelete(DeleteBehavior.Cascade);
            ;
            builder.HasOne(x => x.Playlist)
                .WithMany(x => x.Library_Playlists)
                .HasForeignKey(x => x.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
