using JiJiBotApp_Backend.DTOs.Model.Permission;
using JiJiBotApp_Backend.Repositories.Permission;
using static JiJiBotApp_Backend.DTOs.SearchRequests.Permission.PermissionRequest;

namespace JiJiBotApp_Backend.Services.Permission
{
    public class PermissionService(IPermissionRepository repository, ILogger<PermissionService> logger) : IPermissionService
    {
        public async Task<(IEnumerable<PermissionListModel> records, int totalCount)> SearchPermission(SearchPermissionRequest request)
        {
            logger.LogInformation("Fetching permissions for request: {request}", request);
            return await repository.SearchPermission(request);
        }

        public async Task<(int permissionId, int totalCount)> AddPermission(AddPermissionRequest request)
        {
            logger.LogInformation("Adding Permission {request}", request);
            return await repository.AddPermission(request);
        }

        public async Task<(bool result, int totalCount)> DeletePermission(DeletePermissionRequest request)
        {
            logger.LogInformation("Deleting Permission {request}", request);
            return await repository.DeletePermission(request);
        }

        public async Task<(int permissionId, int totalCount)> EditPermission(UpdatePermissionRequest request)
        {
            logger.LogInformation("Updating Permission {request}", request);
            return await repository.EditPermission(request);
        }
    }
}
