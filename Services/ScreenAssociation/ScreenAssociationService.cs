using JiJiBotApp_Backend.DTOs.Model.Role;
using JiJiBotApp_Backend.DTOs.Model.Screen;
using JiJiBotApp_Backend.DTOs.Model.ScreenAssociation;
using JiJiBotApp_Backend.DTOs.SearchRequests.ScreenAssociation;
using JiJiBotApp_Backend.Repositories.ScreenAssociation;
namespace JiJiBotApp_Backend.Services.ScreenAssociation
{
    public class ScreenAssociationService(IScreenAssociationRepository ScreenAssociationRepository, ILogger<ScreenAssociationService> logger) : IScreenAssociationService
    {
        public async Task<(IEnumerable<ScreenListModel> records, int totalCount)> SearchScreenAssociation(ScreenAssociationSearchRequest request)
        {
            logger.LogInformation("Fetching roles with request: {request}", request);
            return await ScreenAssociationRepository.SearchScreenAssociation(request);
        }

        public async Task<(int id, int totalCount)> AddScreenAssociation(ScreenAssociationAddRequest request)
        {
            logger.LogInformation("Adding role with request: {request}", request);
            return await ScreenAssociationRepository.AddScreenAssociation(request);
        }

        public async Task<(bool result, int totalCount)> DeleteScreenAssociation(ScreenAssociationDeleteRequest request)
        {
            logger.LogInformation("Deleting role with request: {request}", request);
            return await ScreenAssociationRepository.DeleteScreenAssociation(request);
        }

        public async Task<(int id, int totalCount)> EditScreenAssociation(ScreenAssociationUpdateRequest request)
        {
            logger.LogInformation("Updating role with request: {request}", request);
            return await ScreenAssociationRepository.EditScreenAssociation(request);
        }
    }
}
