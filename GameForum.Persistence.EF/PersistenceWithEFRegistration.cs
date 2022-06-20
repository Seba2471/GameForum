using GameForum.Application.Contracts.Persistence;
using GameForum.Persistence.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameForum.Persistence.EF
{
    public static class PersistenceWithEFRegistration
    {
        public static IServiceCollection AddGameForumPersistenceEFServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GameForumContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("GameForumConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            return services;
        }
    }
}
