namespace EnterpriseTemplate.Instrumentation.Logging
{
    using System;
    using System.Collections.Specialized;

    using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

    public class SemanticLoggingExceptionHandler : IExceptionHandler
    {
        public SemanticLoggingExceptionHandler(NameValueCollection attributes)
        {
        }

        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            EnterpriseTemplateEvents.Log.ExceptionHandlerLoggedException(exception.ToString(), handlingInstanceId);
            return exception;
        }
    }
}