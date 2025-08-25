using JiJiBotApp_Backend.DTOs.Model.Login;
using JiJiBotApp_Backend.DTOs.SearchRequests.Login;
using JiJiBotApp_Backend.Repositories.Login;

namespace JiJiBotApp_Backend.Services.Login
{
    public class LoginService(ILoginRepository loginRepository, ILogger<LoginService> logger) : ILoginService
    {
        public async Task<UserModel> ValidateUserAsync(LoginAuthRequestModel request)
        {
            logger.LogInformation("Validating user with  UserLogId: {UserLogId}, Password: {Password}",
                request.UserName, request.Password);

            return await loginRepository.ValidateUserAsync(request);
        }
    }
}
