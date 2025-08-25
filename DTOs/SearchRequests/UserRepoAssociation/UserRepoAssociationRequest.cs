namespace JiJiBotApp_Backend.DTOs.SearchRequests.UserRepoAssociation
{
    public class UserRepoAssociationSearchRequest
    {
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public string Action { get; set; } = "SEARCH"; 
    }

    public class UserRepoAssociationAddRequest
    {
        public int UserId { get; set; }
        public int RepoId { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedBy { get; set; }
        public string Action { get; set; } = "Insert";

    }

    public class UserRepoAssociationUpdateRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RepoId { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedBy { get; set; }
        public string Action { get; set; } = "Update";

    }

    public class UserRepoAssociationDeleteRequest
    {
        public int Id { get; set; }
        public string Action { get; set; } = "Delete";

    }
}
