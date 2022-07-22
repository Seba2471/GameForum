namespace GameForum.Infrastructure.Persistence.EF
{
    public class JSONWebTokensSettings
    {
        public string AccessKey { get; set; }
        public string RefreshKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double AccessTokenDurationTimeInMinutes { get; set; }
        public double RefreshTokenDurationTimeInDay { get; set; }
    }
}
