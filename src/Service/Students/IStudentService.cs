namespace Service.Student;

using Domain.Core;
using Domain.Dtos;

public interface IStudentService
{
    Task<ResponseResult> RegisterAsync(NewStudentDto student);
    Task<ResponseResult> UnregisterByIdAsync(long id);
    Task<ResponseResult> DisenrollStudentAsync(long id, int enrollmentNumber, StudentDisenrollmentDto studentDisenrollmentDto);
    Task<ResponseResult> EditStudentPersonalInfoAsync(long id, StudentPersonalInfoDto studentPersonalInfoDto);
    Task<ResponseResult> EnrollStudentAsync(long id, StudentEnrollmentDto studentEnrollmentDto);
    Task<IReadOnlyList<StudentDto>?> GetListAsync(string? enrolled, int? number);
    Task<ResponseResult> TransferStudentAsync(long id, int enrollmentNumber, StudentTransferDto studentTransferDto);
}
