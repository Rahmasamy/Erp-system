using Microsoft.AspNetCore.Mvc;

namespace Aspire.Utilities;
public class ValidatedResult<T> where T : class
{
    public required bool IsValid { get; set; }
    public T? Result { get; set; }
    public string? Message { get; set; }
    public string[]? Errors { get; set; } = [];
}

public static class GlobalErrorHandling
{
    public static ValidatedResult<T> SetValidResult<T>(T result) where T : class
    {
        return new ValidatedResult<T> { IsValid = true, Result = result };
    }

    public static ProblemDetails SetProblemDetails(string title, string? message, string[]? errors)
    {
        if (errors is null || errors.Length == 0)
        {
            return new ProblemDetails
            {
                Title = title,
                Detail = message,
                Status = 400
            };
        }

        return new ProblemDetails
        {
            Title = title,
            Detail = message,
            Status = 400,
            Extensions = { { "errors", errors } }
        };
    }
}
