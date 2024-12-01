using Repositories.IRepository;
using VibeZDTO;
using VibeZOData.Services.ElasticSearch;

public class DataSyncService
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly ITrackRepository _trackRepository;
    private readonly IAlbumRepository _albumRepository;
    private readonly IArtistRepository _artistRepository;
    private readonly IElasticSearchService<PlaylistDTO> _playlistElasticService;
    private readonly IElasticSearchService<TrackDTO> _trackElasticService;
    private readonly IElasticSearchService<AlbumDTO> _albumElasticService;
    private readonly IElasticSearchService<ArtistDTO> _artistElasticService;

    public DataSyncService(
        IPlaylistRepository playlistRepository,
        ITrackRepository trackRepository,
        IAlbumRepository albumRepository,
        IArtistRepository artistRepository,
        IElasticSearchService<PlaylistDTO> playlistElasticService,
        IElasticSearchService<TrackDTO> trackElasticService,
        IElasticSearchService<AlbumDTO> albumElasticService,
        IElasticSearchService<ArtistDTO> artistElasticService)
    {
        _playlistRepository = playlistRepository;
        _trackRepository = trackRepository;
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
        _playlistElasticService = playlistElasticService;
        _trackElasticService = trackElasticService;
        _albumElasticService = albumElasticService;
        _artistElasticService = artistElasticService;
    }

    public async Task SyncDataToElasticsearch()
    {
        var playlists = await _playlistRepository.GetAllPlaylists();
        var tracks = await _trackRepository.GetAllTracks();
        var albums = await _albumRepository.GetAllAlbums();
        var artists = await _artistRepository.GetAllArtists();

        foreach (var playlist in playlists)
        {
            var playlistDto = new PlaylistDTO
            {
                PlaylistId = playlist.PlaylistId,
                Name = playlist.Name,
                Description = playlist.Description,
                CreateBy = playlist.createBy,
                Image = playlist.Image,
                UserId = playlist.UserId
            };
            await _playlistElasticService.IndexDocumentAsync(playlistDto.PlaylistId.ToString(), playlistDto);
        }

        foreach (var track in tracks)
        {
            var trackDto = new TrackDTO
            {
                TrackId = track.TrackId,
                AlbumId = track.AlbumId,
                CategoryId = track.CategoryId,
                artistId = track.ArtistId,
                AlbumName = track.Album.Name,
                ArtistName = track.Artist.Name,
                Name = track.Name,
                Lyrics = track.Lyrics,
                Genre = track.Genre,
                Path = track.Path,
                Image = track.Image,
                Listener = track.Listener,
                Time = track.Time,
                CreateDate = track.CreateDate
            };
            await _trackElasticService.IndexDocumentAsync(trackDto.TrackId.ToString(), trackDto);
        }

        foreach (var album in albums)
        {
            var albumDto = new AlbumDTO
            {
                Id = album.Id,
                ArtistId = album.ArtistId,
                Name = album.Name,
                DateOfRelease = album.DateOfRelease,
                Image = album.Image,
                Nation = album.Nation
            };
            await _albumElasticService.IndexDocumentAsync(albumDto.Id.ToString(), albumDto);
        }

        foreach (var artist in artists)
        {
            var artistDto = new ArtistDTO
            {
                Id = artist.Id,
                Name = artist.Name,
                Genre = artist.Genre,
                Image = artist.Image,
                ImgBackground = artist.ImgBackground,
                Nation = artist.Nation
            };
            await _artistElasticService.IndexDocumentAsync(artistDto.Id.ToString(), artistDto);
        }
    }
}
