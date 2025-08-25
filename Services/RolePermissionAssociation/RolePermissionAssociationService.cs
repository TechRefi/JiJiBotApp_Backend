using JiJiBotApp_Backend.DTOs.Model.RolePermissionAssociation;
using JiJiBotApp_Backend.Repositories.RolePermissionAssociation;
using static JiJiBotApp_Backend.DTOs.SearchRequests.RolePermissionAssociation.RolePermissionAssociationRequest;
using static JiJiBotApp_Backend.Services.RolePermissionAssociation.RolePermissionAssociationService;

namespace JiJiBotApp_Backend.Services.RolePermissionAssociation
{
    
        public class RolePermissionAssociationService(IRolePermissionAssociationRepository repository, ILogger<RolePermissionAssociationService> logger)
       : IRolePermissionAssociationService
        {
            public async Task<(IEnumerable<RolePermissionAssociationListModel> records, int totalCount)> SearchRolePermissionAssociation(RolePermissionAssociationSearchRequest request)
            {
                logger.LogInformation("Fetching RolePermissionAssociations with request: {request}", request);
                return await repository.SearchRolePermissionAssociation(request);
            }

            public async Task<(int id, int totalCount)> AddRolePermissionAssociation(RolePermissionAssociationAddRequest request)
            {
                logger.LogInformation("Adding RolePermissionAssociation with request: {request}", request);
                return await repository.AddRolePermissionAssociation(request);
            }

            public async Task<(bool result, int totalCount)> DeleteRolePermissionAssociation(RolePermissionAssociationDeleteRequest request)
            {
                logger.LogInformation("Deleting RolePermissionAssociation with request: {request}", request);
                return await repository.DeleteRolePermissionAssociation(request);
            }

            public async Task<(int id, int totalCount)> EditRolePermissionAssociation(RolePermissionAssociationUpdateRequest request)
            {
                logger.LogInformation("Updating RolePermissionAssociation with request: {request}", request);
                return await repository.EditRolePermissionAssociation(request);
            }
        }
    
}
