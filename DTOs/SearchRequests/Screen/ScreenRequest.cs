namespace JiJiBotApp_Backend.DTOs.SearchRequests.Screen
{
   
     public class SearchScreenRequest
     {
          public int StartIndex { get; set; } = 0;
          public int EndIndex { get; set; } = 10;
          public int CompanyId { get; set; } = 0;
           public string Action { get; set; } = "SEARCH";
     }

    public class AddScreenRequest
    {
        public string ScreenName { get; set; }
        public string ScreenCode { get; set; }
        public string Description { get; set; }
        public int Parent_Id { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedBy { get; set; }
        public string Action { get; set; } = "Insert";
        
    }
    public class UpdateScreenRequest
    {
        public int Id { get; set; }
        public string ScreenName { get; set; }
        public string ScreenCode { get; set; }
        public string Description { get; set; }
        public int Parent_Id { get; set; }
        public bool IsActive { get; set; }
        public int UpdatedBy { get; set; }
        public string Action { get; set; } = "Update";
    }
    public class DeleteScreenRequest
    {
        public int Id { get; set; }
        public string Action { get; set; } = "Delete";
    }
}