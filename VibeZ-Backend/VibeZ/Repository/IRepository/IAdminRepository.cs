using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VibeZDTO;

namespace Repositories.IRepository
{
    public interface IAdminRepository
    {
        Task<IEnumerable<AdminArtistDTO>> GetAdminArtists();

        Task<TotalDataDTO> GetTotalData();
        Task<AdminHomeDTO> GetAdminHome();
        Task<IEnumerable<User>> GetAdminBan();
        Task<IEnumerable<AdminApprovalDTO>> GetAdminApporval();
        Task ChangeStatusApporval(Guid trackId);

    }
}