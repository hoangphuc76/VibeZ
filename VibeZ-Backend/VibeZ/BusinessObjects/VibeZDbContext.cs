﻿using BusinessObjects.Data.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BusinessObjects
{
    public class VibeZDbContext : DbContext
    {

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }
        public virtual DbSet<BlockedArtist> BlockedArtists { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<Library> Libraries { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Package_features> P_features { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<User_package> U_packages { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Track_Playlist> TrackPlayLists { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<Library_Album> Library_Albums { get; set; }
        public virtual DbSet<Library_Playlist> Library_Playlists { get; set; }
        public virtual DbSet<Library_Artist> Library_Artists { get; set; }
        public virtual DbSet<TrackListener> TrackListeners { get; set; }
        public virtual DbSet<ArtistFollow> ArtistFollows { get; set; }
        public virtual DbSet<ArtistPending> ArtistPendings{ get; set; }


        public VibeZDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlaylistConfiguration());
            modelBuilder.ApplyConfiguration(new TrackConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new LibraryConfiguration());
            modelBuilder.ApplyConfiguration(new Library_AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new Library_PlaylistConfiguration());
            modelBuilder.ApplyConfiguration(new Library_ArtistConfiguration());
            modelBuilder.ApplyConfiguration(new TrackPlaylistConfiguration());

        }
    }
}
