using JiJiBotApp_Backend.DTOs.Model.UserRoleAssociation;
using JiJiBotApp_Backend.DTOs.SearchRequests.UserRoleAssociation;
using JiJiBotApp_Backend.Repositories.UserRoleAssociation;

namespace JiJiBotApp_Backend.Services.UserRoleAssociation
{
    public class UserRoleAssociationService(IUserRoleAssociationRepository repository, ILogger<UserRoleAssociationService> logger) : IUserRoleAssociationService
    {
        public async Task<(IEnumerable<UserRoleAssociationListModel> records, int totalCount)> SearchUserRoleAssociation(UserRoleAssociationSearchRequest request)
        {
            logger.LogInformation("Fetching UserRoleAssociations with request: {request}", request);
            return await repository.SearchUserRoleAssociation(request);
        }

        public async Task<(int id, int totalCount)> AddUserRoleAssociation(UserRoleAssociationAddRequest request)
        {
            logger.LogInformation("Adding UserRoleAssociation with request: {request}", request);
            return await repository.AddUserRoleAssociation(request);
        }

        public async Task<(bool result, int totalCount)> DeleteUserRoleAssociation(UserRoleAssociationDeleteRequest request)
        {
            logger.LogInformation("Deleting UserRoleAssociation with request: {request}", request);
            return await repository.DeleteUserRoleAssociation(request);
        }

        public async Task<(int id, int totalCount)> EditUserRoleAssociation(UserRoleAssociationUpdateRequest request)
        {
            logger.LogInformation("Updating UserRoleAssociation with request: {request}", request);
            return await repository.EditUserRoleAssociation(request);
        }
    }
}
