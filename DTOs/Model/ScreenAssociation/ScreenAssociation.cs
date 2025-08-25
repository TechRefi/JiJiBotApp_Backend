using JiJiBotApp_Backend.DTOs.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JiJiBotApp_Backend.DTOs.Model.ScreenAssociation
{
    public class ScreenAssociationListModel : BaseCLS
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey ("Screen")]
        public int ScreenId { get; set; }
        [Required]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }

    }
     public class ScreenAssociationReturnModel
    {
        public int Id { get; set; }
        public int TotalCount { get; set; }
    }
}
