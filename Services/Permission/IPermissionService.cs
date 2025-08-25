using JiJiBotApp_Backend.DTOs.Model.Permission;
using JiJiBotApp_Backend.DTOs.SearchRequests.Permission;
using static JiJiBotApp_Backend.DTOs.SearchRequests.Permission.PermissionRequest;

namespace JiJiBotApp_Backend.Services.Permission
{
    public interface IPermissionService
    {
        Task<(IEnumerable<PermissionListModel> records, int totalCount)> SearchPermission(SearchPermissionRequest request);
        Task<(int permissionId, int totalCount)> AddPermission(AddPermissionRequest request);
        Task<(bool result, int totalCount)> DeletePermission(DeletePermissionRequest request);
        Task<(int permissionId, int totalCount)> EditPermission(UpdatePermissionRequest request);
    }
}
