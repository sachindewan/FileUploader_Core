using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiCore.Helpers
{
    public class RetryHelper
    {
        public static async Task<T> RetryOnExceptionAsync<T>(int maxAttemptCount, TimeSpan retryInterval, Func<Task<T>> operation)
        {
            var exceptions = new List<Exception>();

            for (int attempted = 0; attempted < maxAttemptCount; attempted++)
            {
                try
                {
                    if (attempted > 0)
                    {
                        Thread.Sleep(retryInterval);
                    }
                    var data = await operation();
                    return data;
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }
             return default(T);
        }
    }
}
