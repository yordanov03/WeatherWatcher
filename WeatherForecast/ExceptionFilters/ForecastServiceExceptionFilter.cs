using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WeatherWatcher.Api.Exceptions;
using WeatherWatcher.Api.Models;

namespace WeatherWatcher.Api.ExceptionFilters
{
    public class ForecastServiceExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var result = new ErrorResponse();

            if(context.Exception is InvalidParameterInputException ||
                context.Exception is WeatherDataProviderException)
            {
                result = new ErrorResponse
                {
                    ErrorCode = "400",
                    ErrorMessage = "Invalid input!"
                };
            }
            else
            {
                result = new ErrorResponse
                {
                    ErrorCode = "500",
                    ErrorMessage = "Something went wrong with the server!"
                };
            }
            context.HttpContext.Response.StatusCode = 200;
            context.Result = new JsonResult(result);
        }
    }
}
