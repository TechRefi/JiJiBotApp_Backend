using JiJiBotApp_Backend.DTOs.Model.Screen;
using JiJiBotApp_Backend.DTOs.Model.ScreenAssociation;
using JiJiBotApp_Backend.DTOs.SearchRequests.ScreenAssociation;

namespace JiJiBotApp_Backend.Services.ScreenAssociation
{
    public interface IScreenAssociationService
    {
        Task<(IEnumerable<ScreenListModel> records, int totalCount)> SearchScreenAssociation(ScreenAssociationSearchRequest request);
        Task<(int id, int totalCount)> AddScreenAssociation(ScreenAssociationAddRequest request);
        Task<(bool result, int totalCount)> DeleteScreenAssociation(ScreenAssociationDeleteRequest request);
        Task<(int id, int totalCount)> EditScreenAssociation(ScreenAssociationUpdateRequest request);
    }
}
