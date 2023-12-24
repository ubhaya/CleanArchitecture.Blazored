using Microsoft.Extensions.Logging;
using Moq;
using CleanArchitecture.Blazored.Application.Common.Behaviours;
using CleanArchitecture.Blazored.Application.Common.Services.Identity;
using CleanArchitecture.Blazored.Application.TodoItems.Commands;
using CleanArchitecture.Blazored.WebUi.Shared.TodoItems;

namespace CleanArchitecture.Blazored.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateTodoItemCommand>> _logger;
    private Mock<ICurrentUser> _currentUser;
    private Mock<IIdentityService> _identityService;

    public RequestLoggerTests()
    {
        _logger = new Mock<ILogger<CreateTodoItemCommand>>();
        _currentUser = new Mock<ICurrentUser>();
        _identityService = new Mock<IIdentityService>();
    }

    [Fact]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUser.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());
        var requestLogger =
            new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _currentUser.Object, _identityService.Object);

        await requestLogger.Process(
            new CreateTodoItemCommand(new CreateTodoItemRequest { ListId = 1, Title = "title" }),
            new CancellationToken());
        
        _identityService.Verify(i=>i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }
    
    [Fact]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _currentUser.Object, _identityService.Object);

        await requestLogger.Process(
            new CreateTodoItemCommand(new CreateTodoItemRequest { ListId = 1, Title = "title" }), new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
