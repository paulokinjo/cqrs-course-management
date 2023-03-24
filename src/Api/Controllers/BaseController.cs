namespace Api.Controllers;

using Api.Utils;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected new IActionResult Ok() => base.Ok(Envelop.Ok("200Ok"));

    protected IActionResult Ok<T>(T result) => base.Ok(Envelop.Ok(result));

    protected IActionResult Error(string? errorMessage, int statusCode = 400) => statusCode switch
    {
        401 => Unauthorized(Envelop.Error("401 Unauthorized",errorMessage)),
        500 => StatusCode(statusCode, Envelop.Error("500 InternalServerError", errorMessage)),
        _ => BadRequest(Envelop.Error("400 BadRequest", errorMessage))
    };
}
