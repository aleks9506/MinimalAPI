using Microsoft.Extensions.DependencyInjection;
using ToDo.Application.Services;
using ToDo.Application.Services.Implementations;

namespace ToDo.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services.AddScoped<IToDoTaskService, ToDoTaskService>();
            return services;
        }
    }
}
