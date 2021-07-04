using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GCP.LoggerManagedTracer.Tests
{
    public class LoggerManagedTracerTests
    {
        [Fact]
        public void StartSpan_ShouldWriteToLog()
        {
            var expectedSpanName = Guid.NewGuid().ToString();
            var loggerMock = new Mock<ILogger<LoggerManagedTracer>>();
            var sut = new LoggerManagedTracer(loggerMock.Object);
            
            using var span = sut.StartSpan(expectedSpanName);
            span.Dispose();

            loggerMock.VerifyLoggerWasCalled(expectedSpanName, Times.Once());
        }

        [Fact]
        public void RunInSpan_ShouldWriteToLog()
        {
            var expectedSpanName = Guid.NewGuid().ToString();
            var loggerMock = new Mock<ILogger<LoggerManagedTracer>>();
            var sut = new LoggerManagedTracer(loggerMock.Object);

            var flipMe = false;
            sut.RunInSpan(() => { flipMe = true; }, expectedSpanName);

            loggerMock.VerifyLoggerWasCalled(expectedSpanName, Times.Once());
            Assert.True(flipMe);
        }

        [Fact]
        public async Task RunInSpanAsync_ShouldWriteToLog()
        {
            var expectedSpanName = Guid.NewGuid().ToString();
            var loggerMock = new Mock<ILogger<LoggerManagedTracer>>();
            var sut = new LoggerManagedTracer(loggerMock.Object);

            var result = await sut.RunInSpanAsync(async () => await Task.FromResult(true), expectedSpanName);

            loggerMock.VerifyLoggerWasCalled(expectedSpanName, Times.Once());
            Assert.True(result);
        }
    }
}
