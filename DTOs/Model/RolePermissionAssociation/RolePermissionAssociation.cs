using JiJiBotApp_Backend.DTOs.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiJiBotApp_Backend.DTOs.Model.RolePermissionAssociation
{
    public class RolePermissionAssociationListModel : BaseCLS
    {
        public int Id { get; set; }               
        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; }          
        [Required]
        [ForeignKey("Permission")]
        public int PermissionId { get; set; }

    }
    public class RolePermissionAssociationReturnModel
    {
        public int Id { get; set; }               
        public int TotalCount { get; set; }
    }
}
