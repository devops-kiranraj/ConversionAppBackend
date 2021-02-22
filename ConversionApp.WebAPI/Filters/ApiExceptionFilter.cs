using ConversionApp.Core.Constants;
using ConversionApp.WebAPI.Factory;
using ConversionApp.WebAPI.Models.Error;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ConversionApp.WebAPI.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = ModelConversion.CreateResponse(new ErrorResponse() { StatusCode = MessageConstants.STATUSCODE_ERRORMSG_GENERIC, StatusMessage = MessageConstants.STATUSMESSAGE_ERRORMSG_GENERIC });
            base.OnException(context);
        }
    }
}
