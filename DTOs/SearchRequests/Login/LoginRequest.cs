namespace JiJiBotApp_Backend.DTOs.SearchRequests.Login
{
    public class LoginAuthRequestModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Action { get; set; } = "LOGIN"; // Default action is Login
    }
}
