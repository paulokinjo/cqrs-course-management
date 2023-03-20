using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Student;
using Service.Students;

namespace Api.Controllers;

public class StudentController : BaseController
{
    private readonly IStudentService studentService;

    public StudentController(IStudentService studentService) =>
        this.studentService = studentService;

    [HttpGet]
    public async Task<IActionResult> GetList(string? enrolled, int? number)
    {
        IReadOnlyList<StudentDto> students = await studentService.GetListAsync(enrolled, number);       
        return Ok(students);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] NewStudentDto newStudentDto)
    {      
        await studentService.RegisterAsync(newStudentDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await studentService.UnregisterByIdAsync(id);
        return NoContent();
    }


    [HttpPost("{id}/enrollment")]
    public async Task<IActionResult> Enroll(long id, [FromBody] StudentEnrollmentDto studentEnrollmentDto)
    {
        var result = await studentService.EnrollStudentAsync(id, studentEnrollmentDto);
        if (result.Type == ResponseType.Success)
        {
            return Ok();
        }

        return Error(result?.ErrorMessage);
    }

    [HttpPut("{id}/enrollment/{enrollmentNumber}")]
    public async Task<IActionResult> Transfer(long id, int enrollmentNumber, StudentTransferDto studentTransferDto)
    {
        var result = await studentService.TransferStudentAsync(id, enrollmentNumber, studentTransferDto);
        if (result.Type == ResponseType.Success)
        {
            return Ok();
        }

        return Error(result?.ErrorMessage);
    }

    [HttpPost("{id}/enrollment/{enrollmentNumber}/deletion")]
    public async Task<IActionResult> Disenroll(long id, int enrollmentNumber, StudentDisenrollmentDto studentDisenrollmentDto)
    {
        var result = await studentService.DisenrollStudentAsync(id, enrollmentNumber, studentDisenrollmentDto);
        if (result.Type == ResponseType.Success)
        {
            return Ok();
        }

        return Error(result?.ErrorMessage);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditPersonalInfo(long id, [FromBody] StudentPersonalInfoDto studentPersonalInfoDto)
    {
        var result = await studentService.EditStudentPersonalInfoAsync(id, studentPersonalInfoDto);
        if (result.Type == ResponseType.Success)
        {
            return Ok();
        }

        return Error(result?.ErrorMessage);
    }
}
