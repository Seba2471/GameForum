namespace GameForum.Infrastructure.Persistence.EF
{
    public class JSONWebTokensSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double AccessTokenDurationTime { get; set; }
    }
}
