using BusinessObjects;

namespace VibeZDTO
{
    public class AlbumDTO : BaseEntity, INameable
    {
        public Guid Id { get; set; }
        public Guid? ArtistId { get; set; }
        public string Name { get; set; }
        public string ArtistName { get; set; }
        public DateOnly DateOfRelease { get; set; }
        public string Image { get; set; }
        public string Nation { get; set; }
    }
}
