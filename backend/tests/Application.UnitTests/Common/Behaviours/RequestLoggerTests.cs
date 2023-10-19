using KwikDeploy.Application.Common.Behaviours;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Targets.Commands;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace KwikDeploy.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<TargetCreate>> _logger = null!;
    private Mock<ICurrentUserService> _currentUserService = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<TargetCreate>>();
        _currentUserService = new Mock<ICurrentUserService>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUserService.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<TargetCreate>(_logger.Object, _currentUserService.Object);

        await requestLogger.Process(new TargetCreate { Body = new TargetCreateBody { Name = "abc" } }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<TargetCreate>(_logger.Object, _currentUserService.Object);

        await requestLogger.Process(new TargetCreate { Body = new TargetCreateBody { Name = "abc" } }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
