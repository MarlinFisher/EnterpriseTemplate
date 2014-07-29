namespace EnterpriseTemplate.Instrumentation.Logging
{
    using System;
    using System.Collections.Specialized;
    using System.Diagnostics;

    using Microsoft.Practices.Unity.InterceptionExtension;

    public class SemanticLogCallHandler : ICallHandler
    {
        public SemanticLogCallHandler(NameValueCollection attributes)
        {
        }

        public int Order { get; set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            if (getNext == null)
            {
                throw new ArgumentNullException("getNext");
            }

            if (input.MethodBase.DeclaringType != null)
            {
                EnterpriseTemplateEvents.Log.LogCallHandlerPreInvoke(
                    input.MethodBase.DeclaringType.FullName, 
                    input.MethodBase.Name);
            }

            var sw = Stopwatch.StartNew();
            var result = getNext()(input, getNext);

            if (input.MethodBase.DeclaringType != null)
            {
                EnterpriseTemplateEvents.Log.LogCallHandlerPostInvoke(
                    input.MethodBase.DeclaringType.FullName, 
                    input.MethodBase.Name, 
                    sw.ElapsedMilliseconds);
            }

            return result;
        }
    }
}