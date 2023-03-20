using Microsoft.Extensions.DependencyInjection;
using Service.Courses;
using Service.Student;

namespace Service.Configurations;

public static class ServicesConfigurations
{
    public static IServiceCollection AddCourseManagementServicesConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ICourseService, CourseService>();
        return services;
    }
}
