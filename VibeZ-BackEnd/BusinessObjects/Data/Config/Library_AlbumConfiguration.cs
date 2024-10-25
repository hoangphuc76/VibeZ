using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Data.Config
{
    internal sealed class Library_AlbumConfiguration : BaseConfiguration<Library_Album>
    {
        public override void Configure(EntityTypeBuilder<Library_Album> builder)
        {
            base.Configure(builder); ;
            builder.HasOne(x => x.Library)
                .WithMany(x => x.Library_Albums)
                .HasForeignKey(x => x.LibraryId);
            builder.HasOne(x => x.Album)
                .WithMany(x => x.Library_Albums)
                .HasForeignKey(x => x.AlbumId);

        }
    }
}
