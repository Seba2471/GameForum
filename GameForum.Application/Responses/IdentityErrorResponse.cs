using Microsoft.AspNetCore.Identity;

namespace GameForum.Application.Responses
{
    public class IdentityErrorResponse
    {
        public Dictionary<string, string[]> IdentityErrors { get; set; } = new Dictionary<string, string[]>();

        public IdentityErrorResponse(List<IdentityError> identityErrors)
        {
            IdentityErrors = identityErrors
                .Where(x => x != null)
                .GroupBy(
                    x => x.Code,
                    x => x.Description,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);
        }

        public IdentityErrorResponse(IdentityError identityError)
        {
            IdentityErrors.Add(identityError.Code, new[] { identityError.Description });
        }

        public IdentityErrorResponse(string key, string value)
        {
            IdentityErrors.Add(key, new[] { value });
        }
    }
}
