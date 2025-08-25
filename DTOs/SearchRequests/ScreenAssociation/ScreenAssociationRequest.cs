namespace JiJiBotApp_Backend.DTOs.SearchRequests.ScreenAssociation
{
   
        public class ScreenAssociationSearchRequest
        {
            public int StartIndex { get; set; }
            public int EndIndex { get; set; }
           public int CompanyId { get; set; }
        //public string Action { get; set; } = "SEARCH"; 
    }

        public class ScreenAssociationAddRequest
        {
            public int CompanyId { get; set; }          
            public int ScreenId { get; set; }            
            public bool IsActive { get; set; } = true;   
            public int CreatedBy { get; set; }          
            public string Action { get; set; } = "Insert";
        }

        public class ScreenAssociationUpdateRequest
        {
            public int Id { get; set; }               
            public int CompanyId { get; set; }
            public int ScreenId { get; set; }
            public bool IsActive { get; set; }
            public int UpdatedBy { get; set; }           
            public string Action { get; set; } = "Update";
        }

        public class ScreenAssociationDeleteRequest
        {
            public int Id { get; set; }                
            public string Action { get; set; } = "Delete";
        }
}