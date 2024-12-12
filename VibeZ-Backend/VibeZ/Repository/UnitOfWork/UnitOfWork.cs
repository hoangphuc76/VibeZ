using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.UnitOfWork
{
    using BusinessObjects;
    using global::Repositories.IRepository;
    using global::Repositories.Repository;
    using System;

    namespace Repositories.UnitOfWork
    {
        public class UnitOfWork : IUnitOfWork
        {
            private readonly VibeZDbContext _context;

            public UnitOfWork(VibeZDbContext context)
            {
                _context = context;
                Users = new UserRepository(_context);
                Follows = new FollowRepository(_context);
                BlockedArtists = new BlockedArtistRepository(_context);
                Libraries = new LibraryRepository(_context);
                Payments = new PaymentRepository(_context);
                Likes = new LikeRepository(_context);
                Tracks = new TrackRepository(_context);
                Albums = new AlbumRepository(_context);
                Artists = new ArtistRepository(_context);
                TrackPlaylists = new TrackPlaylistRepository(_context);
                Playlists = new PlaylistRepository(_context);
                LibraryAlbums = new Library_AlbumRepository(_context);
                LibraryPlaylists = new Library_PlaylistRepository(_context);
                LibraryArtists = new Library_ArtistRepository(_context);
                u_Package = new U_PackageRepository(_context);
                ArtistPending = new ArtistPendingRepository(_context);
            }
            public IArtistPendingRepository ArtistPending { get; private set; }
            public ILibraryRepository Library { get; private set; }
            public IUserRepository Users { get; private set; }
            public IFollowRepository Follows { get; private set; }
            public IBlockedArtistRepository BlockedArtists { get; private set; }
            public ILibraryRepository Libraries { get; private set; }
            public IPackageRepository Packages { get; private set; }
            public IPaymentRepository Payments { get; private set; }
            public ILikeRepository Likes { get; private set; }
            public ITrackRepository Tracks { get; private set; }
            public IAlbumRepository Albums { get; private set; }
            public IArtistRepository Artists { get; private set; }
            public ITracksPlaylistRepository TrackPlaylists { get; private set; }
            public IPlaylistRepository Playlists { get; private set; }
            public ILibrary_AlbumRepository LibraryAlbums { get; private set; }
            public ILibrary_PlaylistRepository LibraryPlaylists { get; private set; }
            public ILibrary_ArtistRepository LibraryArtists { get; private set; }
            public IU_PackageRepository u_Package { get; private set; }

            public async Task<int> Complete()
            {
                return await _context.SaveChangesAsync();
            }

            public void Dispose()
            {
                _context.Dispose();
            }
        }
    }
}
