namespace JiJiBotApp_Backend.DTOs.SearchRequests.Role
{ 
       public class RoleSearchRequest
        {
            public int StartIndex { get; set; }
            public int EndIndex { get; set; }
        public string Action { get; set; } = "SEARCH";

    }

        public class RoleAddRequest
        {
            public string RoleName { get; set; }
            public string RoleCode { get; set; }
            public int CompanyId { get; set; }
            public string Description { get; set; }
            public int CreatedBy { get; set; }
        public string Action { get; set; } = "Insert";
    }

        public class RoleUpdateRequest
        {
            public int RoleId { get; set; }
            public string RoleName { get; set; }
            public string RoleCode { get; set; }
            public int CompanyId { get; set; }
            public string Description { get; set; }
            public int UpdatedBy { get; set; }
           public string Action { get; set; } = "Update";
    }

        public class RoleDeleteRequest
        {
            public int RoleId { get; set; }
           public string Action { get; set; } = "Delete";
    }
}



