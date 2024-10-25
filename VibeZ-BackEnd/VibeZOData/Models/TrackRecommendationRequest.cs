namespace VibeZOData.Models
{
    public class TrackRecommendationRequest
    {
        public List<Guid> RecentlyPlayedIds { get; set; }  // Danh sách các bài hát đã nghe gần đây
        public Guid ClickedTrackId { get; set; }            // Bài hát người dùng vừa click
        public int TopN { get; set; } = 10;                 // Số lượng bài hát gợi ý (mặc định là 10)
    }
}
