using JiJiBotApp_Backend.DTOs.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiJiBotApp_Backend.DTOs.Model.UserRoleAssociation
{
    public class UserRoleAssociationListModel : BaseCLS
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; }
    }
}
