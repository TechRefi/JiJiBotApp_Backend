using JiJiBotApp_Backend.DTOs.Model.Repositories;
using JiJiBotApp_Backend.DTOs.SearchRequests.Repositories;
using static JiJiBotApp_Backend.DTOs.SearchRequests.Repositories.RepositoryRequest;


namespace JiJiBotApp_Backend.Services.Repositories
{
    public interface IRepositoriesService
    {
        Task<(IEnumerable<RepositoriesListModel> records, int totalCount)> SearchRepository(SearchRepositoryRequest request);
        Task<(int repositoryId, int totalCount)> AddRepository(AddRepositoryRequest request);
        Task<(bool result, int totalCount)> DeleteRepository(DeleteRepositoryRequest request);
        Task<(int repositoryId, int totalCount)> EditRepository(UpdateRepositoryRequest request);
    }
}
