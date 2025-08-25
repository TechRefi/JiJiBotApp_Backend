namespace JiJiBotApp_Backend.DTOs.SearchRequests.RolePermissionAssociation
{
    public class RolePermissionAssociationRequest
    {
        public class RolePermissionAssociationSearchRequest
        {
            public int StartIndex { get; set; }
            public int EndIndex { get; set; }
            public string Action { get; set; } = "SEARCH";
        }

        public class RolePermissionAssociationAddRequest
        {
            public int RoleId { get; set; }          
            public int PermissionId { get; set; }    
            public int CreatedBy { get; set; }       
            public bool IsActive { get; set; } = true;
            public string Action { get; set; } = "Insert";
        }

        public class RolePermissionAssociationUpdateRequest
        {
            public int Id { get; set; }              
            public int RoleId { get; set; }          
            public int PermissionId { get; set; }    
            public int UpdatedBy { get; set; }      
            public bool IsActive { get; set; }
            public string Action { get; set; } = "Update";
        }

        public class RolePermissionAssociationDeleteRequest
        {
            public int Id { get; set; }
            public string Action { get; set; } = "Delete";

        }
    }
}
