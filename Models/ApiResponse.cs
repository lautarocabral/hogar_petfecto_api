namespace hogar_petfecto_api.Models
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }

        public ApiResponse(int statusCode, string message, T result = default)
        {
            StatusCode = statusCode;
            Message = message;
            Result = result;
        }

        // Método estático para crear una respuesta de éxito
        public static ApiResponse<T> Success(T result, string message = "Operación exitosa", int statusCode = 200)
        {
            return new ApiResponse<T>(statusCode, message, result);
        }

        // Método estático para crear una respuesta de error
        public static ApiResponse<T> Error(string message, int statusCode = 400)
        {
            return new ApiResponse<T>(statusCode, message, default);
        }
        public static ApiResponse<T> UnAuthorizedToken(string message, int statusCode = 400)
        {
            return new ApiResponse<T>(statusCode, message, default);
        }
    }
}
