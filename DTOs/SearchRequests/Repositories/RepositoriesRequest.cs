namespace JiJiBotApp_Backend.DTOs.SearchRequests.Repositories
{
    public class RepositoryRequest
    {
        public class SearchRepositoryRequest
        {
            public int StartIndex { get; set; } = 0;
            public int EndIndex { get; set; } = 10;
            public string Action { get; set; } = "Search";
        }

        public class AddRepositoryRequest
        {
            public string repo_name { get; set; }
            public string? Description { get; set; }
            public bool IsActive { get; set; } = true;
            public int CreatedBy { get; set; }
            public string Action { get; set; } = "Insert";
        }

        public class UpdateRepositoryRequest
        {
            public int Id { get; set; }
            public string repo_name { get; set; }
            public string? Description { get; set; }
            public bool IsActive { get; set; }
            public int UpdatedBy { get; set; }
            public string Action { get; set; } = "Update";
        }

        public class DeleteRepositoryRequest
        {
            public int Id { get; set; }
            public string Action { get; set; } = "Delete";
        }
    }
}
