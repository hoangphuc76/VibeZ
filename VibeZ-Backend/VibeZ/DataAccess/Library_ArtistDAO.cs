using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Library_ArtistDAO : SingletonBase<Library_ArtistDAO>
    {

        // Lấy tất cả Library_Artist
        public async Task<IEnumerable<Library_Artist>> GetAll()
        {
            return await _context.Library_Artists
                                 .AsNoTracking()  // Không cần tracking để tối ưu hiệu suất
                                 .ToListAsync();
        }

        // Lấy Library_Artist theo ArtistId và LibraryId
        public async Task<Library_Artist> GetArtistById(Guid artistId, Guid libraryId)
        {
            var libraryArtist = await _context.Library_Artists
                                              .AsNoTracking()  // Không tracking vì chỉ đọc dữ liệu
                                              .FirstOrDefaultAsync(f => f.ArtistId == artistId && f.LibraryId == libraryId);

            return libraryArtist;
        }

        // Thêm một Library_Artist mới
        public async Task Add(Library_Artist libraryArtist)
        {
            var existingItem = await _context.Library_Artists
                                      .AsNoTracking() // Nếu chỉ đọc và không muốn theo dõi
                                      .FirstOrDefaultAsync(f => f.ArtistId == libraryArtist.ArtistId && f.LibraryId == libraryArtist.LibraryId);
            if (existingItem != null)
            {
                throw new InvalidOperationException("The Library_Artist relationship already exists.");
            }

            await _context.Library_Artists.AddAsync(libraryArtist);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin Library_Artist
        public async Task Update(Library_Artist libraryArtist)
        {
            var existingItem = await _context.Library_Artists
                                             .FirstOrDefaultAsync(f => f.ArtistId == libraryArtist.ArtistId && f.LibraryId == libraryArtist.LibraryId);

            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(libraryArtist); // Cập nhật giá trị mới
            }
            else
            {
                // Nếu không tìm thấy, có thể ném ngoại lệ hoặc log cảnh báo
                throw new InvalidOperationException($"Library_Artist with ArtistId {libraryArtist.ArtistId} and LibraryId {libraryArtist.LibraryId} not found.");
            }

            await _context.SaveChangesAsync();
        }


        // Xóa một Library_Artist theo ArtistId và LibraryId
        // Xóa một Library_Artist theo ArtistId và LibraryId
        public async Task Delete(Guid artistId, Guid libraryId)
        {
            var libraryArtist = await _context.Library_Artists
                                              .FirstOrDefaultAsync(f => f.ArtistId == artistId && f.LibraryId == libraryId);

            if (libraryArtist != null)
            {
                _context.Library_Artists.Attach(libraryArtist);
                _context.Library_Artists.Remove(libraryArtist);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Nếu không tìm thấy, ném ngoại lệ
                throw new InvalidOperationException($"Library_Artist relationship with ArtistId {artistId} and LibraryId {libraryId} not found.");
            }
        }

    }
}
