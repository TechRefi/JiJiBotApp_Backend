using JiJiBotApp_Backend.DTOs.Model.UserRepoAssociation;
using JiJiBotApp_Backend.DTOs.SearchRequests.UserRepoAssociation;

namespace JiJiBotApp_Backend.Services.UserRepoAssociation
{
    public interface IUserRepoAssociationService
    {
        Task<(IEnumerable<UserRepoAssociationListModel> records, int totalCount)> SearchUser(UserRepoAssociationSearchRequest request);
        Task<(int Id, int totalCount)> AddUser(UserRepoAssociationAddRequest request);
        Task<(bool result, int totalCount)> DeleteUser(UserRepoAssociationDeleteRequest request);
        Task<(int Id, int totalCount)> EditUser(UserRepoAssociationUpdateRequest request);
    }
}
