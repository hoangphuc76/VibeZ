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
    internal sealed class UserConfiguration : BaseConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasDefaultValueSql(UniqueType.Algorithm)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasMaxLength(DataSchemaLength.Medium)
                .IsRequired();
            builder.Property(p => p.Email)
                .HasMaxLength(256)  // Giới hạn tối đa 256 ký tự
                .IsRequired();
            builder.Property(u => u.Password)
                 .HasMaxLength(100)
                 .IsRequired();

            // Cấu hình Gender (không bắt buộc)
            builder.Property(u => u.Gender)
                  .HasMaxLength(10)
                  .IsRequired(false);

            // Cấu hình Role (bắt buộc với giá trị mặc định là "User")
            builder.Property(u => u.Role)
                  .HasMaxLength(20)
                  .HasDefaultValue("User")
                  .IsRequired();

            // Cấu hình UserName (bắt buộc và duy nhất)
            builder.Property(u => u.UserName)
                  .HasMaxLength(100)
                  .IsRequired();
            builder.HasIndex(u => u.UserName)  // Đảm bảo UserName là duy nhất
                  .IsUnique();

            // Cấu hình DOB (không bắt buộc)
            builder.Property(u => u.DOB)
                  .IsRequired(false);

            // Cấu hình Premium với giá trị mặc định là "Free"
            builder.Property(u => u.Premium)
                  .HasMaxLength(50)
                  .HasDefaultValue("Free")
                  .IsRequired();

            builder.HasMany(x => x.Playlists)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Follow)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.BlockedArtists)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Payment)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.User_package)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Likes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Library)
                .WithOne(x => x.User)
                .HasForeignKey<Library>(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
