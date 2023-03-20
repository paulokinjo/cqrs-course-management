namespace Service.Student;

using Service.Students;

public interface IStudentService
{
    Task RegisterAsync(NewStudentDto student);
    Task UnregisterByIdAsync(long id);
    Task<ResponseResult> DisenrollStudentAsync(long id, int enrollmentNumber, StudentDisenrollmentDto studentDisenrollmentDto);
    Task<ResponseResult> EditStudentPersonalInfoAsync(long id, StudentPersonalInfoDto studentPersonalInfoDto);
    Task<ResponseResult> EnrollStudentAsync(long id, StudentEnrollmentDto studentEnrollmentDto);
    Task<IReadOnlyList<StudentDto>> GetListAsync(string? enrolled, int? number);
    Task<ResponseResult> TransferStudentAsync(long id, int enrollmentNumber, StudentTransferDto studentTransferDto);
}
