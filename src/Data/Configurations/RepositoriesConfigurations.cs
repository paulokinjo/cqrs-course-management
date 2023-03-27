using Data.Courses;
using Data.Students;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Configurations;

public static class RepositoriesConfigurations
{
    public static IServiceCollection AddCourseManagementRepositoriesConfigurations(this IServiceCollection services)
    {
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();

        return services;
    }
}
