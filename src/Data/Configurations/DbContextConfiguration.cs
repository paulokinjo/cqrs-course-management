using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Configurations;

public static class DbContextConfigurations
{
    public static IServiceCollection AddCourseManagementDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CourseManagementDbContext>(dbContextOptions => 
            dbContextOptions.UseSqlite(configuration["ConnectionStrings:CourseManagementDb"]));
        return services;   
    }
}
