using JiJiBotApp_Backend.DTOs.Model.Base;

namespace JiJiBotApp_Backend.DTOs.Model.Repositories
{
    public class RepositoriesListModel : BaseCLS
    {
        public int Id { get; set; }                          
        public string repo_name { get; set; }                 
        public string Description { get; set; }
    }
    public class RepositoriesReturnModel
    {
        
            public int RepositoriesId { get; set; }
            public int TotalCount { get; set; }
        
    }
}
