using FluentValidation;
using FluentValidation.Results;

namespace GameForum.Application.Responses
{
    public class NotValidateResponse
    {
        public Dictionary<string, string[]> ValidationErrors { get; set; }
        public NotValidateResponse(List<ValidationFailure> validationErrors)
        {
            ValidationErrors = validationErrors
                .Where(x => x != null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);
        }
    }
}
