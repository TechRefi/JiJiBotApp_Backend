namespace JiJiBotApp_Backend.DTOs.Model.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public required string Message { get; set; }
        public int TotalCount { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Success", int totalCount = 0)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                TotalCount = totalCount,
                Data = data
            };
        }

        public static ApiResponse<T> ErrorResponse(string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                TotalCount = 0,
                Data = default
            };
        }
    }
}