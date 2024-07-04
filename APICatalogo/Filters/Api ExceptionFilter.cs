using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters
{
    public class Api_ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<Api_ExceptionFilter> _logger;

        public Api_ExceptionFilter(ILogger<Api_ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public Api_ExceptionFilter()
        {
            
        }
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Ocorreu um exceção não tratada: Status Code 500");
            context.Result = new ObjectResult("Ocorreu um problema ao tratar a sua solicitação: Status Code 500")
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        }
    }
}
