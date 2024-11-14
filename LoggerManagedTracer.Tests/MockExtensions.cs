using System;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace GCP.LoggerManagedTracer.Tests;

internal static class MockExtensions
{
    internal static void VerifyLoggerWasCalled<T>(
        this ILogger<T> logger,
        string contains,
        int times
    )
    {
        logger
            .Received(times)
            .Log(
                LogLevel.Debug,
                0,
                Arg.Is<object>(x => x.ToString().Contains(contains)),
                null,
                Arg.Any<Func<object, Exception, string>>()
            );
    }
}
