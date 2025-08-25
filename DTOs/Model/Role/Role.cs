using JiJiBotApp_Backend.DTOs.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiJiBotApp_Backend.DTOs.Model.Role
{
    public class RoleListModel : BaseCLS
    {
            public int RoleId { get; set; }
            public string RoleName { get; set; }
            public string RoleCode { get; set; }
            [Required]
            [ForeignKey("Company")]
            public int CompanyId { get; set; }
            public string Description { get; set; }
        
    }
   
    
    public class RoleReturnModel
    {
        public int RoleId { get; set; }
        public int TotalCount { get; set; }
    }

 
}
