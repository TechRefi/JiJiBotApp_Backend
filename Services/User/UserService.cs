using JiJiBotApp_Backend.DTOs.Model.User;
using JiJiBotApp_Backend.DTOs.SearchRequests.User;
using JiJiBotApp_Backend.Repositories.User;

namespace JiJiBotApp_Backend.Services.User
{
   
        public class UserService(IUserRepository userRepository, ILogger<UserService> logger) : IUserService
    {
            public async Task<(IEnumerable<UserListModel> records, int totalCount)> SearchUser(UserSearchRequest request)
            {
                logger.LogInformation("Fetching users with request: {request}", request);
                return await userRepository.SearchUser(request);
            }

            public async Task<(int userId, int totalCount)> AddUser(UserAddRequest request)
            {
                logger.LogInformation("Adding user with request: {request}", request);
                return await userRepository.AddUser(request);
            }

            public async Task<(bool result, int totalCount)> DeleteUser(UserDeleteRequest request)
            {
                logger.LogInformation("Deleting user with request: {request}", request);
                return await userRepository.DeleteUser(request);
            }

            public async Task<(int userId, int totalCount)> EditUser(UserUpdateRequest request)
            {
                logger.LogInformation("Updating user with request: {request}", request);
                return await userRepository.EditUser(request);
            }
        }
}

