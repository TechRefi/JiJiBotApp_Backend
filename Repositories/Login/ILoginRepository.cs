using JiJiBotApp_Backend.DTOs.Model.Login;
using JiJiBotApp_Backend.DTOs.SearchRequests.Login;

namespace JiJiBotApp_Backend.Repositories.Login
{
    public interface ILoginRepository
    {
        Task<UserModel> ValidateUserAsync(LoginAuthRequestModel request);
    }
}
