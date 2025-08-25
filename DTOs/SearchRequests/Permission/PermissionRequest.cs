namespace JiJiBotApp_Backend.DTOs.SearchRequests.Permission
{
    public class PermissionRequest
    {
        public class SearchPermissionRequest
        {
            public int StartIndex { get; set; } = 0;
            public int EndIndex { get; set; } = 10;
            public string Action { get; set; } = "SEARCH";
        }

        public class AddPermissionRequest
        {
            public string Permission_name { get; set; }
            public bool IsActive { get; set; } = true;
            public int created_by { get; set; }
            public string Action { get; set; } = "Insert";
        }

        public class UpdatePermissionRequest
        {
            public int Id { get; set; }
            public string Permission_name { get; set; }
            public bool IsActive { get; set; }
            public int updated_by { get; set; }
            public string Action { get; set; } = "Update";
        }

        public class DeletePermissionRequest
        {
            public int Id { get; set; }
            public string Action { get; set; } = "Delete";

        }

    }
}
