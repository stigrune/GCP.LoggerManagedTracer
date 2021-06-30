using Google.Cloud.Diagnostics.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LoggerManagedTracer
{
    public class LoggerManagedTracer : IManagedTracer
    {
        private readonly ILogger<LoggerManagedTracer> logger;

        public LoggerManagedTracer(ILogger<LoggerManagedTracer> logger)
        {
            this.logger = logger;
        }

        public ISpan StartSpan(string name, StartSpanOptions options = null)
        {
            return new LoggerSpan(logger, name);
        }


        public T RunInSpan<T>(Func<T> func, string name, StartSpanOptions options = null)
        {
            using (StartSpan(name, options))
            {
                return func();
            }
        }

        public async Task<T> RunInSpanAsync<T>(Func<Task<T>> func, string name, StartSpanOptions options = null)
        {
            using (StartSpan(name, options))
            {
                return await func().ConfigureAwait(false);
            }
        }

        public void RunInSpan(Action action, string name, StartSpanOptions options = null) 
        {
            using (StartSpan(name, options))
            {
                action();
            }
        }

        public void SetStackTrace(StackTrace stackTrace) { }
        public void AnnotateSpan(Dictionary<string, string> labels) { }

        public ulong? GetCurrentSpanId() => null;
        public string GetCurrentTraceId() => null;
    }
}
