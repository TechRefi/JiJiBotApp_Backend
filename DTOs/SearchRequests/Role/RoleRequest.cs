namespace JiJiBotApp_Backend.DTOs.SearchRequests.Role
{
    public class RoleSearchRequest
    {
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public string Action { get; set; } = "SEARCH";

        // Optional: Include permissions in search results
        public bool IncludePermissions { get; set; } = false;
    }

    public class RoleAddRequest
    {
        public string RoleName { get; set; }
        public string RoleCode { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public int EnteredBy { get; set; }
        public string Action { get; set; } = "Insert";

        // Added property for permissions
        public List<int> PermissionIds { get; set; } = new List<int>();
    }

    public class RoleUpdateRequest
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleCode { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public int EditedBy { get; set; }
        public string Action { get; set; } = "Update";

        // Added property for permissions
        public List<int> PermissionIds { get; set; } = new List<int>();
    }

    public class RoleDeleteRequest
    {
        public int RoleId { get; set; }
        public string Action { get; set; } = "Delete";
    }

    // New request for managing role permissions specifically
    public class RolePermissionsUpdateRequest
    {
        public int RoleId { get; set; }
        public List<int> PermissionIds { get; set; } = new List<int>();
        public int UpdatedBy { get; set; }
        public string Action { get; set; } = "UpdatePermissions";
    }
}