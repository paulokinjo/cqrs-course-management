namespace Api.Controllers;

using Api.Utils;
using Domain.Core;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult FromResult(ResponseResult result)
    {
        if (result == null)
        {
            return Error("Internal Server Error.", StatusCodes.Status500InternalServerError);
        }

        if (result.Type == ResponseType.Success)
        {
            return Ok(result);
        }

        return Error(result.ErrorMessage);
    }

    private new IActionResult Ok() => base.Ok(Envelop.Ok("200Ok"));
    private IActionResult Ok<T>(T result) => base.Ok(Envelop.Ok(result));

    private IActionResult Error(string? errorMessage, int statusCode = 400) => statusCode switch
    {
        401 => Unauthorized(Envelop.Error("401 Unauthorized",errorMessage)),
        500 => StatusCode(statusCode, Envelop.Error("500 InternalServerError", errorMessage)),
        _ => BadRequest(Envelop.Error("400 BadRequest", errorMessage))
    };
}
