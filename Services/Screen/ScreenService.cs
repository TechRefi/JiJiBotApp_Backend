using JiJiBotApp_Backend.DTOs.Model.Role;
using JiJiBotApp_Backend.DTOs.Model.Screen;
using JiJiBotApp_Backend.DTOs.SearchRequests.Role;
using JiJiBotApp_Backend.DTOs.SearchRequests.Screen;
using JiJiBotApp_Backend.Repositories.Role;
using JiJiBotApp_Backend.Repositories.Screen;
using JiJiBotApp_Backend.Services.Role;
using static JiJiBotApp_Backend.DTOs.SearchRequests.Repositories.RepositoryRequest;


namespace JiJiBotApp_Backend.Services.Screen
{
    public class ScreenService(IScreenRepository ScreenRepository, ILogger<ScreenService> logger) : IScreenService
    {
        public async Task<(IEnumerable<ScreenListModel> records, int totalCount)> SearchScreen(SearchScreenRequest request)
        {
            logger.LogInformation("Fetching Screen with request: {request}", request);
            return await ScreenRepository.SearchScreen(request);
        }
            public async Task<(int Id, int totalCount)> AddScreen(AddScreenRequest request)
        {
            logger.LogInformation("Adding Screen with request: {@request}", request);
            return await ScreenRepository.AddScreen(request);
        }

        public async Task<(bool result, int totalCount)> DeleteScreen(DeleteScreenRequest request)
        {
            logger.LogInformation("Deleting Screen with request: {@request}", request);
            return await ScreenRepository.DeleteScreen(request);
        }

        public async Task<(int Id, int totalCount)> EditScreen(UpdateScreenRequest request)
        {
            logger.LogInformation("Updating Screen with request: {@request}", request);
            return await ScreenRepository.EditScreen(request);
        }
    }
    
}
