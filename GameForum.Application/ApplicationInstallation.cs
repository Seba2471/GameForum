using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace GameForum.Application
{
    public static class ApplicationInstallation
    {
        public static IServiceCollection AddGameForumApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());


            return services;
        }
    }
}
