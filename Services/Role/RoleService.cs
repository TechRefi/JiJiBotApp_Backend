using JiJiBotApp_Backend.DTOs.Model.Role;
using JiJiBotApp_Backend.DTOs.SearchRequests.Role;
using JiJiBotApp_Backend.Repositories.Role;
using JiJiBotApp_Backend.Repositories.RolePermissionAssociation;

namespace JiJiBotApp_Backend.Services.Role
{
    public class RoleService(
        IRoleRepository roleRepository,
        IRolePermissionAssociationRepository rolePermissionRepository,
        ILogger<RoleService> logger) : IRoleService
    {
        public async Task<(IEnumerable<RoleListModel> records, int totalCount)> SearchRole(RoleSearchRequest request)
        {
            logger.LogInformation("Fetching roles with request: {request}", request);
            var (roles, totalCount) = await roleRepository.SearchRole(request);

            // If permissions are requested, fetch them for each role
            if (request.IncludePermissions)
            {
                foreach (var role in roles)
                {
                    var (permissions, _) = await GetRolePermissions(role.RoleId);
                    role.Permissions = permissions.ToList();
                }
            }

            return (roles, totalCount);
        }

        public async Task<(int roleId, int totalCount)> AddRole(RoleAddRequest request)
        {
            logger.LogInformation("Adding role with request: {request}", request);

            // Add the role first
            var (roleId, totalCount) = await roleRepository.AddRole(request);

            // If role was successfully created and permissions are provided, add permissions
            if (roleId > 0 && request.PermissionIds?.Any() == true)
            {
                await UpdateRolePermissionsInternal(roleId, request.PermissionIds, request.EnteredBy);
            }

            return (roleId, totalCount);
        }

        public async Task<(bool result, int totalCount)> DeleteRole(RoleDeleteRequest request)
        {
            logger.LogInformation("Deleting role with request: {request}", request);

            // Delete role permissions first (foreign key constraint)
            await rolePermissionRepository.DeleteRolePermissionsByRole(request.RoleId);

            // Then delete the role
            return await roleRepository.DeleteRole(request);
        }

        public async Task<(int roleId, int totalCount)> EditRole(RoleUpdateRequest request)
        {
            logger.LogInformation("Updating role with request: {request}", request);

            // Update the role first
            var (roleId, totalCount) = await roleRepository.EditRole(request);

            // If role was successfully updated and permissions are provided, update permissions
            if (roleId > 0 && request.PermissionIds != null)
            {
                await UpdateRolePermissionsInternal(roleId, request.PermissionIds, request.EditedBy);
            }

            return (roleId, totalCount);
        }

        public async Task<(bool result, int totalCount)> UpdateRolePermissions(RolePermissionsUpdateRequest request)
        {
            logger.LogInformation("Updating role permissions with request: {request}", request);
            return await UpdateRolePermissionsInternal(request.RoleId, request.PermissionIds, request.UpdatedBy);
        }

        public async Task<(List<RolePermissionModel> permissions, int totalCount)> GetRolePermissions(int roleId)
        {
            logger.LogInformation("Fetching permissions for role: {roleId}", roleId);
            return await rolePermissionRepository.GetRolePermissions(roleId);
        }

        public async Task<(IEnumerable<RolePermissionModel> availablePermissions, int totalCount)> GetAvailablePermissions()
        {
            logger.LogInformation("Fetching all available permissions");
            return await rolePermissionRepository.GetAvailablePermissions();
        }

        // Internal helper method for updating role permissions
        private async Task<(bool result, int totalCount)> UpdateRolePermissionsInternal(int roleId, List<int> permissionIds, int updatedBy)
        {
            try
            {
                // Remove existing permissions for this role
                await rolePermissionRepository.DeleteRolePermissionsByRole(roleId);

                // Add new permissions
                if (permissionIds?.Any() == true)
                {
                    await rolePermissionRepository.AddRolePermissions(roleId, permissionIds, updatedBy);
                }

                return (true, permissionIds?.Count ?? 0);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating permissions for role: {roleId}", roleId);
                return (false, 0);
            }
        }
    }
}