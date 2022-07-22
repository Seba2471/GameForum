using FluentValidation.Results;

namespace GameForum.Application.Responses
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public List<string> ValidationErrors { get; set; }


        public BaseResponse()
        {
            Success = true;
            ValidationErrors = new List<string>();
        }

        public BaseResponse(string message = null)
        {
            Success = true;
            Message = message;
            ValidationErrors = new List<string>();
        }

        public BaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
            ValidationErrors = new List<string>();
        }

        public BaseResponse(ValidationResult validationResult)
        {
            ValidationErrors = new List<string>();
            Success = validationResult.Errors.Count < 0;
            foreach (var item in validationResult.Errors)
            {
                ValidationErrors.Add(item.ErrorMessage);
            }
        }



    }
}
