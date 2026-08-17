using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Salons_Project.Domain.Common.Results;

namespace Salons_Project.Api.Filters;

public sealed class ResultFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Nothing to do before action execution
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            // Check if the result is a Result<T> type
            var resultType = objectResult.Value?.GetType();
            if (resultType != null && resultType.IsGenericType)
            {
                var genericTypeDef = resultType.GetGenericTypeDefinition();

                // Handle Result<T>
                if (genericTypeDef == typeof(Domain.Common.Results.Result<>))
                {
                    dynamic result = objectResult.Value!;
                    if (!result.IsSuccess)
                    {
                        var errorsResult = result.Error;

                        ValidationProblemDetails details = new ValidationProblemDetails()
                        {
                            Detail = errorsResult.Message,
                            Status = errorsResult.Status,
                        };

                        context.Result = new ObjectResult(details)
                        {
                            StatusCode = errorsResult.Status
                        };
                    }
                    else
                    {
                        // Return only the value for successful results
                        context.Result = new OkObjectResult(result.Value);
                    }
                }
            }
            // Handle non-generic Result
            else if (resultType == typeof(Result))
            {
                var result = (Result)objectResult.Value!;
                if (!result.IsSuccess)
                {
                    var errorsResult = result.Error!;

                    ValidationProblemDetails details = new ValidationProblemDetails()
                    {
                        Detail = errorsResult.Message,
                        Status = errorsResult.Status,
                    };

                    context.Result = new ObjectResult(details)
                    {
                        StatusCode = errorsResult.Status
                    };
                }
                else
                {
                    context.Result = new NoContentResult();
                }
            }
        }
    }
}