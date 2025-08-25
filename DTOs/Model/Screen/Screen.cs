using JiJiBotApp_Backend.DTOs.Model.Base;
namespace JiJiBotApp_Backend.DTOs.Model.Screen
{
    public class ScreenListModel: BaseCLS
    {
        public int Id { get; set; }
        public string ScreenName { get; set; }
        public string ScreenCode { get; set; }
        public string Description { get; set; }      
        public int Parent_Id { get; set; }
    }
    public class ScreenReturnModel
    {
        public int Id { get; set; }
        public int TotalCount { get; set; }
    }
}