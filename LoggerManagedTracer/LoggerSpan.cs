using System.Collections.Generic;
using System.Diagnostics;
using Google.Cloud.Diagnostics.Common;
using Microsoft.Extensions.Logging;

namespace GCP.LoggerManagedTracer;

internal class LoggerSpan(ILogger<LoggerManagedTracer> logger, string name) : ISpan
{
    private readonly ILogger<LoggerManagedTracer> logger = logger;
    private readonly string name = name;
    private readonly Stopwatch stopwatch = Stopwatch.StartNew();

    public void Dispose()
    {
        logger.LogDebug(
            "[{name}] [{stopwatchMilliseconds} ms]",
            name,
            stopwatch.ElapsedMilliseconds
        );
    }

    public bool Disposed() => !stopwatch.IsRunning;

    public void AnnotateSpan(Dictionary<string, string> labels) { }

    public void SetStackTrace(StackTrace stackTrace) { }

    public ulong SpanId() => default;
}
