using AutoMapper;
using BusinessObjects;
using VibeZDTO;

namespace VibeZOData.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<User, UserDTO>().ForMember(d => d.RoleName, o => o.MapFrom(src => src.Role.RoleName));
            CreateMap<Track, TrackDTO>()
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist != null ? src.Artist.Name : string.Empty))  // Map Artist.Name -> ArtistName
                .ForMember(dest => dest.AlbumName, opt => opt.MapFrom(src => src.Album != null ? src.Album.Name : string.Empty))    // Map Album.Name -> AlbumName
                .ReverseMap()  // Ánh xạ ngược từ TrackDTO về Track
                .ForPath(dest => dest.Artist.Name, opt => opt.MapFrom(src => src.ArtistName))  // Map ArtistName -> Artist.Name
                .ForPath(dest => dest.Album.Name, opt => opt.MapFrom(src => src.AlbumName));   // Map AlbumName -> Album.Name
                                                                                   // Map ArtistName -> Artist.Name
            CreateMap<Playlist, PlaylistDTO>().ReverseMap();
            CreateMap<Album, AlbumDTO>().ReverseMap();
            CreateMap<Artist, ArtistDTO>().ReverseMap();
            CreateMap<BlockedArtist, BlockedArtistDTO>().ReverseMap();
            CreateMap<Track_Playlist, TrackPlayListDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
