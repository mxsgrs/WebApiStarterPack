using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Starter.Store.WebApi.BuildingBlocks.Domain;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        ProblemDetails problemDetails = CreateProblemDetailFromException(exception);

        httpContext.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.BadRequest;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static ProblemDetails CreateProblemDetailFromException(Exception exception)
    {
        ProblemDetails problemDetails = new()
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server error",
            Detail = exception.Message
        };

        switch (exception)
        {
            // 400
            case BadRequestException:
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Bad request";
                break;

            // 404
            case NotFoundException:
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Title = "Not found";
                break;
        }

        return problemDetails;
    }
}
