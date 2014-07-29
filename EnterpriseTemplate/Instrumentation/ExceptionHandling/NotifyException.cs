namespace EnterpriseTemplate.Instrumentation.ExceptionHandling
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class NotifyException : Exception
    {
        public NotifyException()
        {
        }

        public NotifyException(string message)
            : base(message)
        {
        }

        public NotifyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        // This constructor is needed for serialization.
        protected NotifyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}