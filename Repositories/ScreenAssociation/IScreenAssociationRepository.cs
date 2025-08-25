using JiJiBotApp_Backend.DTOs.Model.RolePermissionAssociation;
using JiJiBotApp_Backend.DTOs.Model.Screen;
using JiJiBotApp_Backend.DTOs.Model.ScreenAssociation;
using JiJiBotApp_Backend.DTOs.SearchRequests.ScreenAssociation;
namespace JiJiBotApp_Backend.Repositories.ScreenAssociation
{
    public interface IScreenAssociationRepository
    {
        Task<(IEnumerable<ScreenListModel> records, int totalCount)> SearchScreenAssociation(ScreenAssociationSearchRequest request);
        Task<(int Id, int totalCount)> AddScreenAssociation(ScreenAssociationAddRequest request);
        Task<(bool result, int totalCount)> DeleteScreenAssociation(ScreenAssociationDeleteRequest request);
        Task<(int Id, int totalCount)> EditScreenAssociation(ScreenAssociationUpdateRequest request);

    }
}