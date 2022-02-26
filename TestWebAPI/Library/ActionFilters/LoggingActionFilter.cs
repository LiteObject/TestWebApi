namespace TestWebAPI.Library.ActionFilters
{

    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The logging action filter.
    /// </summary>
    public class LoggingActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingActionFilter"/> class.
        /// </summary>
        /// <param name="loggerFactory">
        /// The logger factory.
        /// </param>
        public LoggingActionFilter(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<LoggingActionFilter>();
        }

        /// <summary>
        /// The on action executing.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' executing.");

            // ToDo: Log request object.
            Microsoft.AspNetCore.Http.HttpRequest request = context.HttpContext.Request;
        }

        /// <summary>
        /// The on action executed.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            this.logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' completed.");
        }
    }
}
