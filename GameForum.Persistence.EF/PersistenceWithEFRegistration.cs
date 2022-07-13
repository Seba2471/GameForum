using GameForum.Application.Contracts.Persistence;
using GameForum.Infrastructure.Persistence.EF;
using GameForum.Infrastructure.Persistence.EF.Repositories;
using GameForum.Persistence.EF.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameForum.Persistence.EF
{
    public static class PersistenceWithEFRegistration
    {
        public static IServiceCollection AddGameForumPersistenceEFServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Authentication settings
            var jsonWebTokensSettings = new JSONWebTokensSettings();

            configuration.GetSection("JSONWebTokensSettings").Bind(jsonWebTokensSettings);
            services.AddSingleton(jsonWebTokensSettings);

            services.AddDbContext<GameForumContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("GameForumConnectionString")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<GameForumContext>()
                .AddDefaultTokenProviders();

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped(typeof(ITokenRepository<>), typeof(TokenRepository<>));

            return services;
        }
    }
}
