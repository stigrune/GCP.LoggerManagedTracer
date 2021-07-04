using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace GCP.LoggerManagedTracer.Tests
{
    internal static class MockExtensions
    {
        internal static Mock<ILogger<T>> VerifyLoggerWasCalled<T>(this Mock<ILogger<T>> logger, string contains, Times times)
        {
            logger.Verify(l => l.Log(
                           It.IsAny<LogLevel>(),
                           It.IsAny<EventId>(),
                           It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(contains)),
                           It.IsAny<Exception>(),
                           It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), times);

            return logger;
        }
    }
}
