using JiJiBotApp_Backend.DTOs.Model.Screen;
using JiJiBotApp_Backend.DTOs.SearchRequests.Screen;

namespace JiJiBotApp_Backend.Services.Screen
{
    public interface IScreenService
    {
        Task<(IEnumerable<ScreenListModel> records, int totalCount)> SearchScreen(SearchScreenRequest request);
        Task<(int Id, int totalCount)> AddScreen(AddScreenRequest request);
        Task<(bool result, int totalCount)> DeleteScreen(DeleteScreenRequest request);
        Task<(int Id, int totalCount)> EditScreen(UpdateScreenRequest request);
    }
}
