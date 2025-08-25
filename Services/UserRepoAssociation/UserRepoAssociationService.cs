using JiJiBotApp_Backend.DTOs.Model.UserRepoAssociation;
using JiJiBotApp_Backend.DTOs.SearchRequests.UserRepoAssociation;
using JiJiBotApp_Backend.Repositories.UserRepoAssociation;
using JiJiBotApp_Backend.Services.UserRepoAssociation;

namespace JiJiBotApp_Backend.Services.UserRepoAssociation
{
    public class UserRepoAssociationService : IUserRepoAssociationService
    {
        private readonly IUserRepoAssociationRepository _repository;
        private readonly ILogger<UserRepoAssociationService> _logger;

        public UserRepoAssociationService(
            IUserRepoAssociationRepository repository,
            ILogger<UserRepoAssociationService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<(IEnumerable<UserRepoAssociationListModel> records, int totalCount)> SearchUser(UserRepoAssociationSearchRequest request)
        {
            _logger.LogInformation("Fetching UserRepoAssociations with request: {@Request}", request);
            return await _repository.SearchUser(request);
        }

        public async Task<(int Id, int totalCount)> AddUser(UserRepoAssociationAddRequest request)
        {
            _logger.LogInformation("Adding UserRepoAssociation with request: {@Request}", request);
            return await _repository.AddUser(request);
        }

        public async Task<(bool result, int totalCount)> DeleteUser(UserRepoAssociationDeleteRequest request)
        {
            _logger.LogInformation("Deleting UserRepoAssociation with request: {@Request}", request);
            return await _repository.DeleteUser(request);
        }

        public async Task<(int Id, int totalCount)> EditUser(UserRepoAssociationUpdateRequest request)
        {
            _logger.LogInformation("Updating UserRepoAssociation with request: {@Request}", request);
            return await _repository.EditUser(request);
        }
    }
}
