using BusinessObjects;
using VibeZDTO;

namespace Service.IServices
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminArtistDTO>> GetAdminArtists();
        Task<TotalDataDTO> GetTotalData();
        Task<AdminHomeDTO> GetAdminHome();
        Task<IEnumerable<User>> GetAdminBan();
        Task<IEnumerable<AdminApprovalDTO>> GetAdminApproval();
        Task ChangeStatusApproval(Guid trackId);
    }
}
