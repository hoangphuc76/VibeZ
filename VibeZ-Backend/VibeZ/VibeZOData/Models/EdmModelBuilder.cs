using BusinessObjects;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace VibeZOData.Models
{
    public class EdmModelBuilder
    {
        public static IEdmModel GetEdmModel()
        {
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Album>("Albums");
            modelBuilder.EntitySet<Artist>("Artists");
            modelBuilder.EntitySet<BlockedArtist>("BlockedArtists");
            modelBuilder.EntitySet<Category>("Categories");
            modelBuilder.EntitySet<Feature>("Features");
            modelBuilder.EntitySet<Follow>("Follows");
            modelBuilder.EntitySet<Library>("Libraries");
            modelBuilder.EntitySet<Like>("Likes");
            modelBuilder.EntitySet<Package>("Packages");
            modelBuilder.EntitySet<Package_features>("PackageFeatures");
            modelBuilder.EntitySet<Payment>("Payments");
            modelBuilder.EntitySet<Playlist>("Playlists");
            modelBuilder.EntitySet<Track>("Tracks");
            modelBuilder.EntitySet<Track_Playlist>("TrackPlaylists");
            //modelBuilder.EntitySet<Tracks_artist>("TracksArtists");
            modelBuilder.EntitySet<User>("Users");
            modelBuilder.EntitySet<User_package>("UserPackages");

            var ba = modelBuilder.EntityType<BlockedArtist>();
            ba.HasKey(ba => new { ba.UserId, ba.ArtistId });

            var follows = modelBuilder.EntityType<Follow>();
            follows.HasKey(fl => new { fl.UserId, fl.ArtistId });

            var likes = modelBuilder.EntityType<Like>();
            likes.HasKey(l => new { l.UserId, l.TrackId });

            var p_feature = modelBuilder.EntityType<Package_features>();
            p_feature.HasKey(p => new { p.PackId, p.FeatureId });

            var tplaylist = modelBuilder.EntityType<Track_Playlist>();
            tplaylist.HasKey(t => new { t.PlaylistId, t.TrackId });

            //var tartist = modelBuilder.EntityType<Tracks_artist>();
            //tartist.HasKey(t => new { t.ArtistId, t.TrackId });

            var u_package = modelBuilder.EntityType<User_package>();
            u_package.HasKey(t => new { t.UserId, t.PackageId });

            return modelBuilder.GetEdmModel();
        }
    }
}
