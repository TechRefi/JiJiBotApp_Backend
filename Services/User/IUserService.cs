using JiJiBotApp_Backend.DTOs.Model.User;
using JiJiBotApp_Backend.DTOs.SearchRequests.User;

namespace JiJiBotApp_Backend.Services.User
{
    public interface IUserService
    {
        Task<(IEnumerable<UserListModel> records, int totalCount)> SearchUser(UserSearchRequest request);
        Task<(int userId, int totalCount)> AddUser(UserAddRequest request);
        Task<(bool result, int totalCount)> DeleteUser(UserDeleteRequest request);
        Task<(int userId, int totalCount)> EditUser(UserUpdateRequest request);
    }
}

