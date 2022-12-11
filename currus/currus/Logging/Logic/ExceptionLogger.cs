using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace currus.Logging.Logic
{
    public class ExceptionLogger : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                Logger.LogInfo($"Method {invocation.Method.Name} called:\n");

                invocation.Proceed();

                Logger.LogInfo($"Method {invocation.Method.Name} response: {JsonConvert.SerializeObject(invocation.ReturnValue)}");
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
