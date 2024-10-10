namespace Starter.Account.WebApi.Controllers.Abstracts;

[ApiController]
[Authorize]
[Route("api/account/[controller]")]
public class AccountControllerBase(IMapper mapper) : ControllerBase
{
    protected readonly IMapper _mapper = mapper;

    #region Corresponding status
    /// <summary>
    /// Return HTTP status corresponding to result
    /// </summary>
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
    /// Return HTTP status corresponding to a typed result
    /// </summary>
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

    /// <summary>
    /// Apply mapping profile 
    /// and return HTTP status corresponding to result
    /// </summary>
    [NonAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult CorrespondingStatus<T, Y>(Result<T> result)
    {
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        Y? mappedValue = _mapper.Map<Y>(result.Value);

        return Ok(mappedValue);
    }

    /// <summary>
    /// Apply mapping profile to a list 
    /// and return HTTP status corresponding to result
    /// </summary>
    [NonAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult CorrespondingStatus<T, Y>(Result<List<T>> result)
    {
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        List<Y>? mappedValue = _mapper.Map<List<Y>>(result.Value);

        return Ok(mappedValue);
    }
    #endregion
}
