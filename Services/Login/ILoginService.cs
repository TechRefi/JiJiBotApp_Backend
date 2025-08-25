using JiJiBotApp_Backend.DTOs.Model.Login;
using JiJiBotApp_Backend.DTOs.SearchRequests.Login;

namespace JiJiBotApp_Backend.Services.Login
{
    public interface ILoginService
    {
        Task<UserModel> ValidateUserAsync(LoginAuthRequestModel request);
    }
}
