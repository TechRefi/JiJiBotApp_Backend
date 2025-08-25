using JiJiBotApp_Backend.DTOs.Model.Role;
using JiJiBotApp_Backend.DTOs.Model.RolePermissionAssociation;
using static JiJiBotApp_Backend.DTOs.SearchRequests.RolePermissionAssociation.RolePermissionAssociationRequest;

namespace JiJiBotApp_Backend.Repositories.RolePermissionAssociation
{
    public interface IRolePermissionAssociationRepository
    {
        // Existing methods
        Task<(IEnumerable<RolePermissionAssociationListModel> records, int totalCount)> SearchRolePermissionAssociation(RolePermissionAssociationSearchRequest request);
        Task<(int id, int totalCount)> AddRolePermissionAssociation(RolePermissionAssociationAddRequest request);
        Task<(bool result, int totalCount)> DeleteRolePermissionAssociation(RolePermissionAssociationDeleteRequest request);
        Task<(int id, int totalCount)> EditRolePermissionAssociation(RolePermissionAssociationUpdateRequest request);

        // New methods for role-centric permission management
        Task<(List<RolePermissionModel> permissions, int totalCount)> GetRolePermissions(int roleId);
        Task<(IEnumerable<RolePermissionModel> availablePermissions, int totalCount)> GetAvailablePermissions();
        Task<bool> DeleteRolePermissionsByRole(int roleId);
        Task<bool> AddRolePermissions(int roleId, List<int> permissionIds, int enteredBy);
    }
}