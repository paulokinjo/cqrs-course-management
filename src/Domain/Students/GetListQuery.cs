using Domain.Core;
using Domain.Dtos;
using Domain.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Students;

public sealed class GetListQuery : IQuery<List<StudentDto>>
{
    public string? EnrolledIn { get;  }
    public int? NumberOfCourses { get; }


    public GetListQuery(string? enrolledIn, int? numberOfCourses)
    {
        EnrolledIn = enrolledIn;
        NumberOfCourses = numberOfCourses;
    }


    public sealed class GetListQueryHandler : IQueryHandler<GetListQuery, List<StudentDto>>
    {
        private IStudentRepository studentRepository;
        public GetListQueryHandler(IStudentRepository studentRepository)
        {
            if (studentRepository is null)
            {
                throw new ArgumentNullException(nameof(studentRepository));
            }

            this.studentRepository = studentRepository;
        }

        public async Task<List<StudentDto>> HandleAsync(GetListQuery query)
        {
            var studentsInDb = await studentRepository.GetListAsync(query.EnrolledIn, query.NumberOfCourses);
            return studentsInDb.Select(ConvertToDto).ToList();
        }

        private StudentDto ConvertToDto(Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Course1 = student.FirstEnrollment?.Course?.Name,
                Course1Grade = student.FirstEnrollment?.Grade.ToString(),
                Course1Credits = student.FirstEnrollment?.Course?.Credits,
                Course2 = student.SecondEnrollment?.Course?.Name,
                Course2Grade = student.SecondEnrollment?.Grade.ToString(),
                Course2Credits = student.SecondEnrollment?.Course?.Credits,
            };
        }
    }
}
