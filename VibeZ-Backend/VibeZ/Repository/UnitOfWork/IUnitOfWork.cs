using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Repositories.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IFollowRepository Follows { get; }
    IBlockedArtistRepository BlockedArtists { get; }
    ILibraryRepository Libraries { get; }
    IPackageRepository Packages { get; }
    IPaymentRepository Payments { get; }
    ILikeRepository Likes { get; }
    ITrackRepository Tracks { get; }
    IAlbumRepository Albums { get; }
    IArtistRepository Artists { get; }
    ITracksPlaylistRepository TrackPlaylists { get; }
    IPlaylistRepository Playlists { get; }
    ILibraryRepository Library { get; }
    ILibrary_AlbumRepository LibraryAlbums { get; }
    ILibrary_PlaylistRepository LibraryPlaylists { get; }
    ILibrary_ArtistRepository LibraryArtists { get; }
    IU_PackageRepository u_Package { get; }
    IArtistPendingRepository ArtistPending { get; }
    Task<int> Complete();
}
