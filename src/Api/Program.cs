using Data.Configurations;
using Service.Configurations;
using Service.Courses;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCourseManagementDbContext(builder.Configuration);
builder.Services.AddCourseManagementRepositoriesConfigurations();
builder.Services.AddCourseManagementServicesConfiguration();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var courseService = serviceScope.ServiceProvider.GetService<ICourseService>();
        if (courseService is not null)
        {
            await Task.Run(async () =>
            {
                CourseDto[] courses =
                {
                    new CourseDto() {Name = "Calculus", Credits = 3},
                    new CourseDto() {Name = "Chemistry", Credits = 3},
                    new CourseDto() {Name = "Composition", Credits = 3},
                    new CourseDto() {Name = "Literature", Credits = 4},
                    new CourseDto() {Name = "Trigonometry", Credits = 4},
                    new CourseDto() {Name = "Microeconomics", Credits = 3},
                    new CourseDto() {Name = "Macroeconomics", Credits = 3}
                };

                foreach (var course in courses)
                {
                    var courseInDb = await courseService.GetByNameAsync(course.Name);
                    if (courseInDb is null)
                    {
                        await courseService.CreateAsync(new CourseDto() { Name = course.Name, Credits = course.Credits });
                    }
                }
            });
        }
    }
}

app.Run();
