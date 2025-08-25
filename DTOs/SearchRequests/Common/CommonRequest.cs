namespace JiJiBotApp_Backend.DTOs.SearchRequests.Common
{
    public class DropdownRequest
    {
        public string? DropdownType { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = [];
    }
}