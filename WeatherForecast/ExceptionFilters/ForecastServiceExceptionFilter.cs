using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WeatherWatcher.Api.ExceptionFilters
{
    public class ForecastServiceExceptionFilter : IExceptionFilter
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public ForecastServiceExceptionFilter(IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            var result = new ViewResult { ViewName = "ForecastServiceException" };
            result.ViewData = new ViewDataDictionary(_modelMetadataProvider,
                                                        context.ModelState);
            result.ViewData.Add("Exception", context.Exception);
            // TODO: Pass additional detailed data via ViewData
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
