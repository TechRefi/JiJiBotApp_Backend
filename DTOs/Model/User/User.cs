using JiJiBotApp_Backend.DTOs.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiJiBotApp_Backend.DTOs.Model.User
{
    public class UserListModel : BaseCLS
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        [Required]
        [ForeignKey("Role")]
        public string RoleId { get; set; }
    }
    public class UserReturnModel
    {
        public int UserId { get; set; }
        public int totalCount { get; set; }

    }
}
