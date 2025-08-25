using JiJiBotApp_Backend.DTOs.Model.Base;

namespace JiJiBotApp_Backend.DTOs.Model.Permission
{
    public class PermissionListModel:BaseCLS
    {
        public int Id { get; set; }  

        public string Permission_name { get; set; } 

        
    }
    public class PermissionReturnModel
    {
        public int PermissionId { get; set; }
        public int totalCount { get; set; }
    }
}
