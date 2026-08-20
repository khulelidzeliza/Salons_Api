using FluentValidation;
using HotChocolate;
using HotChocolate.Execution;

namespace Salons_Project.Api.Filters;

public sealed class GraphQlErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        var ex = error.Exception;

        // 1) FluentValidation → structured validation errors
        if (ex is ValidationException vex)
        {
            var failures = vex.Errors.Select(f => new
            {
                field = f.PropertyName,
                code = string.IsNullOrWhiteSpace(f.ErrorCode) ? "VALIDATION_ERROR" : f.ErrorCode,
                message = f.ErrorMessage,
                attemptedValue = f.AttemptedValue
            }).ToArray();

            var firstCode = failures.FirstOrDefault()?.code ?? "VALIDATION_ERROR";

            return ErrorBuilder.New()
                .SetCode(firstCode)
                .SetMessage("One or more validation errors occurred.")
                .SetExtension("validationErrors", failures)
                .SetPath(error.Path)
                .Build();
        }

        // 2) Authorization errors → normalize
        if (IsAuthError(error))
        {
            var code = MapAuthCode(error.Code);

            var message = code switch
            {
                "AUTH_NOT_AUTHENTICATED" => "You must be authenticated to access this resource.",
                "AUTH_NOT_AUTHORIZED" => "You do not have permission to perform this action.",
                _ => "Authorization failed."
            };

            return ErrorBuilder.New()
                .SetCode(code)
                .SetMessage(message)
                .SetPath(error.Path)
                .Build();
        }

        // 3) Error already has a code → keep it
        if (!string.IsNullOrWhiteSpace(error.Code))
        {
            return error;
        }

        // 4) Promote code from extensions if present
        if (error.Extensions is { } ext &&
            ext.TryGetValue("code", out var codeObj) &&
            codeObj is string _code)
        {
            return error.WithCode(_code);
        }

        // 5) Fallback → generic server error
        return error
            .WithCode("public_SERVER_ERROR")
            .WithMessage("An unexpected error occurred.");
    }

    private static bool IsAuthError(IError error)
    {
        // Check explicit auth codes
        if (!string.IsNullOrWhiteSpace(error.Code))
        {
            return error.Code is "AUTH_NOT_AUTHENTICATED" or "AUTH_NOT_AUTHORIZED";
        }

        // Check exception type
        var ex = error.Exception;

        return ex != null &&
               ex.GetType().Name.Contains("Authorization", StringComparison.OrdinalIgnoreCase);
    }

    private static string MapAuthCode(string? code) => code switch
    {
        "AUTH_NOT_AUTHENTICATED" => "AUTH_NOT_AUTHENTICATED",
        "AUTH_NOT_AUTHORIZED" => "AUTH_NOT_AUTHORIZED",
        _ => "AUTH_UNAUTHORIZED"
    };
}