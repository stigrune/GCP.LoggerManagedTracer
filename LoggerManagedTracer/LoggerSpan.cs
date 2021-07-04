using Google.Cloud.Diagnostics.Common;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

namespace GCP.LoggerManagedTracer
{
    internal class LoggerSpan : ISpan
    {
        private readonly ILogger<LoggerManagedTracer> logger;
        private readonly string name;
        private readonly Stopwatch stopwatch;

        public LoggerSpan(ILogger<LoggerManagedTracer> logger, string name)
        {
            this.logger = logger;
            this.name = name;
            stopwatch = Stopwatch.StartNew();
        }


        public void Dispose()
        {
            logger.LogDebug("[{name}] [{stopwatchMilliseconds} ms]", name, stopwatch.ElapsedMilliseconds);
        }

        public bool Disposed() => !stopwatch.IsRunning;

        public void AnnotateSpan(Dictionary<string, string> labels) { }

        public void SetStackTrace(StackTrace stackTrace) { }

        public ulong SpanId() => default;
    }
}
