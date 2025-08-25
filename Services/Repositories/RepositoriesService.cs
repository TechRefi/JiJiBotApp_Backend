using JiJiBotApp_Backend.DTOs.Model.Repositories;
using JiJiBotApp_Backend.Repositories.Repositories;
using JiJiBotApp_Backend.Services.Repositories;
using static JiJiBotApp_Backend.DTOs.SearchRequests.Repositories.RepositoryRequest;

namespace JiJiBotApp_Backend.Services.Repository
{
    public class RepositoryService(IRepositoriesRepository repository, ILogger<RepositoryService> logger) : IRepositoriesService
    {
        public async Task<(IEnumerable<RepositoriesListModel> records, int totalCount)> SearchRepository(SearchRepositoryRequest request)
        {
            logger.LogInformation("Fetching Repositories with request: {@request}", request);
            return await repository.SearchRepository(request);
        }

        public async Task<(int repositoryId, int totalCount)> AddRepository(AddRepositoryRequest request)
        {
            logger.LogInformation("Adding Repository with request: {@request}", request);
            return await repository.AddRepository(request);
        }

        public async Task<(bool result, int totalCount)> DeleteRepository(DeleteRepositoryRequest request)
        {
            logger.LogInformation("Deleting Repository with request: {@request}", request);
            return await repository.DeleteRepository(request);
        }

        public async Task<(int repositoryId, int totalCount)> EditRepository(UpdateRepositoryRequest request)
        {
            logger.LogInformation("Updating Repository with request: {@request}", request);
            return await repository.EditRepository(request);
        }
    }
}
