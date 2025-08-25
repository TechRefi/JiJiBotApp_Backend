namespace JiJiBotApp_Backend.DTOs.SearchRequests.Company
{
    public class SearchCompanyRequest
    {
        public int StartIndex { get; set; } = 0;
        public int EndIndex { get; set; } = 10;
        public string Action { get; set; } = "SEARCH";
    }
    public class AddCompanyRequest
    {
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }  
        public bool isActive { get; set; }
        public string Action { get; set; } = "INSERT"; 
    }
    public class DeleteCompanyRequest
    { 
        public int CompanyId { get; set; }
        public string Action { get; set; } = "DELETE";
    }
    public class UpdateCompanyRequest
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public bool isActive { get; set; }
        public string Description { get; set; } 
        public int UpdatedBy { get; set; }
        public string Action { get; set; } = "UPDATE"; 
    }
    //public class GetCompanyByIdRequest
    //{
    //    public int CompanyId { get; set; }
    //    public string Action { get; set; } = "GETBYID"; // Default action is GetById
    //}
    //public class GetCompanyAllRequest
    //{
    //    public string Action { get; set; } = "GETALL"; // Default action is GetByName
    //}
}
