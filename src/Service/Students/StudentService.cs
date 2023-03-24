namespace Service.Student;

using Domain.Core;
using Domain.Courses;
using Domain.Repositories;
using Domain.Students;
using Service.Students;
using static Domain.Students.EditPersonalInfoCommand;

internal class StudentService : IStudentService
{
    private readonly IStudentRepository repository;
    private readonly ICourseRepository courseRepository;

    public StudentService(IStudentRepository repository, ICourseRepository courseRepository)
    {
        this.repository = repository;
        this.courseRepository = courseRepository;
    }

    public async Task RegisterAsync(NewStudentDto newStudentDto)
    {
        var student = ConvertFromDto(newStudentDto);
        if (newStudentDto.Course1 != null && newStudentDto.Course1Grade != null)
        {
            Course? course = await courseRepository.GetByNameAsync(newStudentDto.Course1);
            if (course is not null)
            {
                student.Enroll(course, Enum.Parse<Grade>(newStudentDto.Course1Grade));
            }
        }

        if (newStudentDto.Course2 != null && newStudentDto.Course2Grade != null)
        {
            Course? course = await courseRepository.GetByNameAsync(newStudentDto.Course2);
            if (course is not null)
            {
                student.Enroll(course, Enum.Parse<Grade>(newStudentDto.Course2Grade));
            }
        }

        await repository.CreateAsync(student);
        _ = await repository.CommitAsync();
    }

    public async Task<IReadOnlyList<StudentDto>> GetListAsync(string? enrolled, int? number)
    {
        var studentsInDb = await repository.GetListAsync(enrolled, number);
        List<StudentDto> studentsDtoList = studentsInDb.Select(ConvertToDto).ToList();
        return studentsDtoList;
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

    private Student ConvertFromDto(NewStudentDto student)
    {
        return new Student(student.Name, student.Email)
        {
            Name = student.Name,
            Email = student.Email,
        };
    }

    public async Task UnregisterByIdAsync(long id)
    {
        var studentInDb = await repository.GetByIdAsync(id);
        if (studentInDb is not null)
        {
            repository.Delete(studentInDb);
            _ = await repository.CommitAsync();
        }
    }

    public async Task<ResponseResult> EnrollStudentAsync(long id, StudentEnrollmentDto studentEnrollmentDto)
    {
        var student = await repository.GetByIdAsync(id);
        if (student is null)
        {
            return new ResponseResult
            {
                Type = ResponseType.Failure,
                ErrorMessage = $"No student found for Id {id}"
            };
        }

        if (studentEnrollmentDto == null)
        {
            throw new ArgumentNullException(nameof(studentEnrollmentDto));
        }


        Course? course = await courseRepository.GetByNameAsync(studentEnrollmentDto.Course);
        if (course == null)
        {
            return new ResponseResult
            {
                Type = ResponseType.Failure,
                ErrorMessage = $"Course is incorrect: {studentEnrollmentDto.Course}"
            };
        }

        bool success = Enum.TryParse(studentEnrollmentDto.Grade, out Grade grade);
        if (!success)
        {
            return new ResponseResult
            {
                Type = ResponseType.Failure,
                ErrorMessage = $"Grade is incorrect: {studentEnrollmentDto.Grade}"
            };
        }

        student.Enroll(course, grade);

        await repository.CommitAsync();

        return new ResponseResult { Type = ResponseType.Success };
    }

    public async Task<ResponseResult> TransferStudentAsync(long id, int enrollmentNumber, StudentTransferDto studentTransferDto)
    {
        var student = await repository.GetByIdAsync(id);
        if (student is null)
        {
            return new ResponseResult
            {
                Type = ResponseType.Failure,
                ErrorMessage = $"No student found for Id {id}"
            };
        }

        if (studentTransferDto == null)
        {
            throw new ArgumentNullException(nameof(studentTransferDto));
        }


        Course? course = await courseRepository.GetByNameAsync(studentTransferDto.Course);
        if (course == null)
        {
            return new ResponseResult
            {
                Type = ResponseType.Failure,
                ErrorMessage = $"Course is incorrect: {studentTransferDto.Course}"
            };
        }

        bool success = Enum.TryParse(studentTransferDto.Grade, out Grade grade);
        if (!success)
        {
            return new ResponseResult
            {
                Type = ResponseType.Failure,
                ErrorMessage = $"Grade is incorrect: {studentTransferDto.Grade}"
            };
        }

        Enrollment enrollment = student.GetEnrollment(enrollmentNumber);
        if (enrollment == null)
        {
            return new ResponseResult
            {
                Type = ResponseType.Failure,
                ErrorMessage = $"No enrollment found with number: {enrollmentNumber}"
            };
        }

        enrollment.Update(course, grade);
        await repository.CommitAsync();

        return new ResponseResult { Type = ResponseType.Success };
    }

    public async Task<ResponseResult> DisenrollStudentAsync(long id, int enrollmentNumber, StudentDisenrollmentDto studentDisenrollmentDto)
    {
        var student = await repository.GetByIdAsync(id);
        if (student is null)
        {
            return new ResponseResult
            {
                Type = ResponseType.Failure,
                ErrorMessage = $"No student found for Id {id}"
            };
        }

        if (string.IsNullOrWhiteSpace(studentDisenrollmentDto.Comment))
        {
            return new ResponseResult
            {
                Type = ResponseType.Failure,
                ErrorMessage = "Disenrollment comment is required"
            };
        }

        Enrollment enrollment = student.GetEnrollment(enrollmentNumber);
        if (enrollment == null)
        {
            return new ResponseResult
            {
                Type = ResponseType.Failure,
                ErrorMessage = $"No enrollment found with number: {enrollmentNumber}"
            };
        }

        student.RemoveEnrollment(enrollment, studentDisenrollmentDto.Comment);

        await repository.CommitAsync();

        return new ResponseResult { Type = ResponseType.Success };
    }

    public async Task<ResponseResult> EditStudentPersonalInfoAsync(long id, StudentPersonalInfoDto studentPersonalInfoDto)
    {
        var command = new EditPersonalInfoCommand
        {
            Id = id,
            Name = studentPersonalInfoDto.Name,
            Email = studentPersonalInfoDto.Email,
        };
        var handler = new EditPersonalInfoCommandHandler(repository);
        
        return await handler.HandleAsync(command);
    }
}