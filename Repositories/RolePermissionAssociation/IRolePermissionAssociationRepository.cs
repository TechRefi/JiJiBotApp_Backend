using JiJiBotApp_Backend.DTOs.Model.RolePermissionAssociation;
using JiJiBotApp_Backend.DTOs.SearchRequests.RolePermissionAssociation;
using static JiJiBotApp_Backend.DTOs.SearchRequests.RolePermissionAssociation.RolePermissionAssociationRequest;

namespace JiJiBotApp_Backend.Repositories.RolePermissionAssociation
{
    public interface IRolePermissionAssociationRepository
    {
        Task<(IEnumerable<RolePermissionAssociationListModel> records, int totalCount)> SearchRolePermissionAssociation(RolePermissionAssociationSearchRequest request);
        Task<(int id, int totalCount)> AddRolePermissionAssociation(RolePermissionAssociationAddRequest request);
        Task<(bool result, int totalCount)> DeleteRolePermissionAssociation(RolePermissionAssociationDeleteRequest request);
        Task<(int id, int totalCount)> EditRolePermissionAssociation(RolePermissionAssociationUpdateRequest request);
    }
}
