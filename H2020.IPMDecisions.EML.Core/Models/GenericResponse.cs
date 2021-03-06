namespace H2020.IPMDecisions.EML.Core.Models
{
    public class GenericResponse
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class GenericResponse<T> : GenericResponse
    {
        public T Result { get; set; }
    }

    public static class GenericResponseBuilder
    {
        public static GenericResponse<T> Success<T>(T result)
        {
            return new GenericResponse<T>()
            {
                IsSuccessful = true,
                Result = result
            };
        }

        public static GenericResponse Success()
        {
            return new GenericResponse()
            {
                IsSuccessful = true
            };
        }
        
        public static GenericResponse<T> NoSuccess<T>(T result, string errorMessage = "")
        {
            return new GenericResponse<T>()
            {
                IsSuccessful = false,
                Result = result,
                ErrorMessage = errorMessage
            };
        }

        public static GenericResponse NoSuccess(string errorMessage = "")
        {
            return new GenericResponse()
            {
                IsSuccessful = false,
                ErrorMessage = errorMessage
            };
        }
    }
}