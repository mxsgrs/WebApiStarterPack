using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Starter.WebApi.Controllers.Abstracts;

/// <summary>
/// Abstraction for application controllers
/// </summary>
[ApiController]
[Authorize]
[Route("[controller]/[action]")]
public class StarterControllerBase : ControllerBase
{
    /// <summary>
    /// Return fluent result with corresponding HTTP status
    /// </summary>
    /// <param name="result">CRUD operation result for example</param>
    /// <returns>API response</returns>
    [NonAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult CorrespondingStatus(Result result)
    {
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    /// <summary>
    /// Return fluent result with corresponding HTTP status
    /// </summary>
    /// <typeparam name="T">Result's type</typeparam>
    /// <param name="result">CRUD operation result for example</param>
    /// <returns>API response</returns>
    [NonAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult CorrespondingStatus<T>(Result<T> result)
    {
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Value);
    }
}
