using JiJiBotApp_Backend.DTOs.Model.Role;
using JiJiBotApp_Backend.DTOs.SearchRequests.Role;

namespace JiJiBotApp_Backend.Repositories.Role
{
    
    public interface IRoleRepository
    {
        Task<(IEnumerable<RoleListModel> records, int totalCount)> SearchRole(RoleSearchRequest request);
        Task<(int roleId, int totalCount)> AddRole(RoleAddRequest request);
        Task<(bool result, int totalCount)> DeleteRole(RoleDeleteRequest request);
        Task<(int roleId, int totalCount)> EditRole(RoleUpdateRequest request);
    }
}

