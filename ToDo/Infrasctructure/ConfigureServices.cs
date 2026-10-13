using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ToDo.Infrasctructure.Data;
using Microsoft.Extensions.Configuration;
using ToDo.Application.Data.Repositories;
using ToDo.Infrasctructure.Data.Repositories;

namespace ToDo.Infrasctructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddToDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ToDoDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ToDoDbContext")));
            return services;
        }

        public static IServiceCollection AddDataRepositories(
            this IServiceCollection services)
        {
            services.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();
            return services;
        }
    }
}
