 namespace JiJiBotApp_Backend.DTOs.SearchRequests.UserRoleAssociation
 {
        public class UserRoleAssociationSearchRequest
        {
            public int StartIndex { get; set; }
            public int EndIndex { get; set; }
            public string Action { get; set; } = "SEARCH"; 
    }

        public class UserRoleAssociationAddRequest
        {
            public int UserId { get; set; }
            public int RoleId { get; set; }
            public int CreatedBy { get; set; }
            public bool IsActive { get; set; } = true;
        public string Action { get; set; } = "Insert"; 
    }

        public class UserRoleAssociationUpdateRequest
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public int RoleId { get; set; }
            public int UpdatedBy { get; set; }
            public bool IsActive { get; set; } 
        public string Action { get; set; } = "Update"; 
    }

        public class UserRoleAssociationDeleteRequest
        {
            public int Id { get; set; }
            public string Action { get; set; } = "Delete";

    }
 }



