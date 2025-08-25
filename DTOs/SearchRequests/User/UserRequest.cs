namespace JiJiBotApp_Backend.DTOs.SearchRequests.User
{
  
        public class UserSearchRequest
        {
            public int StartIndex { get; set; }
            public int EndIndex { get; set; }
            public string Action { get; set; } = "SEARCH";
    }

        public class UserAddRequest
        {
            public string UserName { get; set; }
            public string UserEmail { get; set; }
            public string UserPassword { get; set; }
            public string RoleId { get; set; }
            public int CreatedBy { get; set; }
            public string Action { get; set; } = "Insert";
    }

        public class UserUpdateRequest
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string UserEmail { get; set; }
            public string UserPassword { get; set; }
            public string RoleId { get; set; }
            public int UpdatedBy { get; set; }
            public string Action { get; set; } = "Update"; 

    }

        public class UserDeleteRequest
        {
            public int UserId { get; set; }
            public string Action { get; set; } = "Delete"; 
    }

    
}
