using JiJiBotApp_Backend.DTOs.Model.UserRoleAssociation;
using JiJiBotApp_Backend.DTOs.SearchRequests.UserRoleAssociation;

namespace JiJiBotApp_Backend.Services.UserRoleAssociation
{
    public interface IUserRoleAssociationService
    {
        Task<(IEnumerable<UserRoleAssociationListModel> records, int totalCount)> SearchUserRoleAssociation(UserRoleAssociationSearchRequest request);
        Task<(int id, int totalCount)> AddUserRoleAssociation(UserRoleAssociationAddRequest request);
        Task<(bool result, int totalCount)> DeleteUserRoleAssociation(UserRoleAssociationDeleteRequest request);
        Task<(int id, int totalCount)> EditUserRoleAssociation(UserRoleAssociationUpdateRequest request);
    }
}
