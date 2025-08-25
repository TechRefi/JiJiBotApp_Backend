using JiJiBotApp_Backend.DTOs.Model.Base;

namespace JiJiBotApp_Backend.DTOs.Model.Company
{
    public class CompanyListModel :BaseCLS
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string Description { get; set; }


    }
    public class CopmanyReturnModel { 
        public int CompanyId { get; set; }
        public int totalCount { get; set; }
        
    }
  
}
