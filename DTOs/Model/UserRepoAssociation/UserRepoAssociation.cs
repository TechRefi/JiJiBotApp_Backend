using JiJiBotApp_Backend.DTOs.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiJiBotApp_Backend.DTOs.Model.UserRepoAssociation
{
    public class UserRepoAssociationListModel : BaseCLS
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        [ForeignKey("Repo")]
        public int RepoId { get; set; }

    }
    public class UserRepoAssociationReturnModel
    {
        public int Id { get; set; }
        public int TotalCount { get; set; }
    }
}
