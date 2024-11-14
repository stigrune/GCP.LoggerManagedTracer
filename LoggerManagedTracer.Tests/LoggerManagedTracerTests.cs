using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace GCP.LoggerManagedTracer.Tests;

public class LoggerManagedTracerTests
{
    [Fact]
    public void StartSpan_ShouldWriteToLog()
    {
        var expectedSpanName = Guid.NewGuid().ToString();
        var loggerMock = Substitute.For<ILogger<LoggerManagedTracer>>();
        var sut = new LoggerManagedTracer(loggerMock);

        using var span = sut.StartSpan(expectedSpanName);
        span.Dispose();

        loggerMock.VerifyLoggerWasCalled(expectedSpanName, 1);
    }

    [Fact]
    public void RunInSpan_ShouldWriteToLog()
    {
        var expectedSpanName = Guid.NewGuid().ToString();
        var loggerMock = Substitute.For<ILogger<LoggerManagedTracer>>();
        var sut = new LoggerManagedTracer(loggerMock);

        var flipMe = false;
        sut.RunInSpan(
            () =>
            {
                flipMe = true;
            },
            expectedSpanName
        );

        loggerMock.VerifyLoggerWasCalled(expectedSpanName, 1);
        Assert.True(flipMe);
    }

    [Fact]
    public async Task RunInSpanAsync_ShouldWriteToLog()
    {
        var expectedSpanName = Guid.NewGuid().ToString();
        var loggerMock = Substitute.For<ILogger<LoggerManagedTracer>>();
        var sut = new LoggerManagedTracer(loggerMock);

        var result = await sut.RunInSpanAsync(
            async () => await Task.FromResult(true),
            expectedSpanName
        );

        loggerMock.VerifyLoggerWasCalled(expectedSpanName, 1);
        Assert.True(result);
    }
}
