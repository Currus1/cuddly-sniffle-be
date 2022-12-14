using Castle.DynamicProxy;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace currus.Logging.Logic
{
    public class ExceptionLogger : IInterceptor
    {
        [ExcludeFromCodeCoverage]
        public void Intercept(IInvocation invocation)
        {
            try
            {
                Logger.LogInfo($"Method {invocation.Method.Name} called:\n");

                invocation.Proceed();

                Logger.LogInfo($"Method {invocation.Method.Name} response: {invocation.ReturnValue}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Catched in {invocation.Method} method:\n" +
                    $"Error: {ex.Message}\n" +
                    $"Trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
