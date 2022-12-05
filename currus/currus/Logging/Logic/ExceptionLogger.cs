using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace currus.Logging.Logic
{
    public class ExceptionLogger : IAsyncInterceptor
    {
        public void InterceptSynchronous(IInvocation invocation)
        {
            try
            {
                Logger.LogInfo($"Method {invocation.Method.Name} called:\n" +
                    $"  Parameters: {JsonConvert.SerializeObject(invocation.Arguments)}");

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

        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
        }

        private async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            try
            {
                Logger.LogInfo($"Method {invocation.Method.Name} called:\n" +
                    $"  Parameters: {JsonConvert.SerializeObject(invocation.Arguments)}");

                invocation.Proceed();
                var task = (Task)invocation.ReturnValue;
                await task;

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

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }

        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            // Step 1. Do something prior to invocation.

            invocation.Proceed();
            var task = (Task<TResult>)invocation.ReturnValue;
            TResult result = await task;

            // Step 2. Do something after invocation.

            return result;
        }
    }
}
