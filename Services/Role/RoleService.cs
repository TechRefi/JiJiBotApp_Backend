using JiJiBotApp_Backend.DTOs.Model.Role;
using JiJiBotApp_Backend.DTOs.SearchRequests.Role;
using JiJiBotApp_Backend.Repositories.Role;
using static JiJiBotApp_Backend.Services.Role.RoleService;

namespace JiJiBotApp_Backend.Services.Role
{

        public class RoleService(IRoleRepository roleRepository, ILogger<RoleService> logger) : IRoleService
        {
            public async Task<(IEnumerable<RoleListModel> records, int totalCount)> SearchRole(RoleSearchRequest request)
            {
                logger.LogInformation("Fetching roles with request: {request}", request);
                return await roleRepository.SearchRole(request);
            }

            public async Task<(int roleId, int totalCount)> AddRole(RoleAddRequest request)
            {
                logger.LogInformation("Adding role with request: {request}", request);
                return await roleRepository.AddRole(request);
            }

            public async Task<(bool result, int totalCount)> DeleteRole(RoleDeleteRequest request)
            {
                logger.LogInformation("Deleting role with request: {request}", request);
                return await roleRepository.DeleteRole(request);
            }

            public async Task<(int roleId, int totalCount)> EditRole(RoleUpdateRequest request)
            {
                logger.LogInformation("Updating role with request: {request}", request);
                return await roleRepository.EditRole(request);
            }
        }
    
}
