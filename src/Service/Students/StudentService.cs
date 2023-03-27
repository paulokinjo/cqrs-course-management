namespace Service.Student;

using Domain.Core;
using Domain.Dtos;
using Domain.Repositories;
using Domain.Students;

internal class StudentService : IStudentService
{
    private readonly Messages messages;

    public StudentService(Messages messages)
    {
        this.messages = messages;
        if (messages == null)
        {
            throw new ArgumentNullException(nameof(messages));
        }
    }

    public async Task<ResponseResult> RegisterAsync(NewStudentDto newStudentDto)
    {
        var command = new RegisterCommand(
            newStudentDto.Name, 
            newStudentDto.Email, 
            newStudentDto.Course1, 
            newStudentDto.Course1Grade, 
            newStudentDto.Course2, 
            newStudentDto.Course2Grade);


        var result = await messages.DispatchAsync(command);
        return result;
    }

    public async Task<IReadOnlyList<StudentDto>?> GetListAsync(string? enrolled, int? number) =>
        await messages.DispatchAsync(new GetListQuery(enrolled, number));    

    public async Task<ResponseResult> UnregisterByIdAsync(long id)
    {
        var command = new UnregisterCommand(id);
        var result = await messages.DispatchAsync(command);
        return result;
    }

    public async Task<ResponseResult> EnrollStudentAsync(long id, StudentEnrollmentDto studentEnrollmentDto)
    {
        var command = new EnrollCommand(id, studentEnrollmentDto.Course, studentEnrollmentDto.Grade);
        return await messages.DispatchAsync(command);
    }

    public async Task<ResponseResult> TransferStudentAsync(long id, int enrollmentNumber, StudentTransferDto studentTransferDto)
    {
        var command = new TransferCommand(id, enrollmentNumber, studentTransferDto.Course, studentTransferDto.Grade);
        return await messages.DispatchAsync(command);
    }

    public async Task<ResponseResult> DisenrollStudentAsync(long id, int enrollmentNumber, StudentDisenrollmentDto studentDisenrollmentDto)
    {
        var command = new DisenrollCommand(id, enrollmentNumber, studentDisenrollmentDto.Comment);
        return await messages.DispatchAsync(command);
    }

    public async Task<ResponseResult> EditStudentPersonalInfoAsync(long id, StudentPersonalInfoDto studentPersonalInfoDto)
    {
        var command = new EditPersonalInfoCommand(id, studentPersonalInfoDto.Name, studentPersonalInfoDto.Email);
        var result = await messages.DispatchAsync(command);
        return result;
    }
}