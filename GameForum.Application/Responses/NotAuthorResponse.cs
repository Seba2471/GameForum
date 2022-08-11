namespace GameForum.Application.Responses
{
    public class NotAuthorResponse
    {
        public string Message = "Only Author can edit";

        public NotAuthorResponse(string propertyName)
        {
            Message += " " + propertyName.ToLower();
        }

        public NotAuthorResponse()
        { }
    }
}
