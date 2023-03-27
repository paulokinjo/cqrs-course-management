using Domain.Core;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Student;

namespace Api.Controllers;

public class StudentController : BaseController
{
    private readonly IStudentService studentService;

    public StudentController(IStudentService studentService) =>
        this.studentService = studentService;

    [HttpGet]
    public async Task<IActionResult> GetList(string? enrolled, int? number)
    {
        IReadOnlyList<StudentDto>? students = await studentService.GetListAsync(enrolled, number);
        return Ok(students);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] NewStudentDto newStudentDto)
    {
        var result = await studentService.RegisterAsync(newStudentDto);
        return FromResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await studentService.UnregisterByIdAsync(id);
        return FromResult(result);
    }


    [HttpPost("{id}/enrollment")]
    public async Task<IActionResult> Enroll(long id, [FromBody] StudentEnrollmentDto studentEnrollmentDto)
    {
        var result = await studentService.EnrollStudentAsync(id, studentEnrollmentDto);
        return FromResult(result);
    }

    [HttpPut("{id}/enrollment/{enrollmentNumber}")]
    public async Task<IActionResult> Transfer(long id, int enrollmentNumber, StudentTransferDto studentTransferDto)
    {
        var result = await studentService.TransferStudentAsync(id, enrollmentNumber, studentTransferDto);
        return FromResult(result);
    }

    [HttpPost("{id}/enrollment/{enrollmentNumber}/deletion")]
    public async Task<IActionResult> Disenroll(long id, int enrollmentNumber, StudentDisenrollmentDto studentDisenrollmentDto)
    {
        var result = await studentService.DisenrollStudentAsync(id, enrollmentNumber, studentDisenrollmentDto);
        return FromResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditPersonalInfo(long id, [FromBody] StudentPersonalInfoDto studentPersonalInfoDto)
    {
        var result = await studentService.EditStudentPersonalInfoAsync(id, studentPersonalInfoDto);
        return FromResult(result);
    }
}
