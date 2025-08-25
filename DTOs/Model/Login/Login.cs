namespace JiJiBotApp_Backend.DTOs.Model.Login
{
    //public class LoginModel
    //{
    //    public string UserName { get; set; } = string.Empty;
    //    public string Password { get; set; } = string.Empty;
    //}

    public class LoginModel
    {
        public string Token { get; set; } = string.Empty;
        public UserModel User { get; set; } = new();
    }

    public class UserModel
    {
        public int UserId { get; set; }
       
        public int RoleId { get; set; }
        
        public string UserName { get; set; } = string.Empty;
     
        public string UserPassword { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
     
        public DateTime EnteredDateTime { get; set; }

    }
}
