using Application.Data;
using Application.Messaging;
using Application.UseCaseUser.Commands;
using Application.UseCaseUser.Handlers.Commands;
using Application.UseCaseUser.IRepositories;
using FluentAssertions;
using Moq;
using static Domain.Errors.DomainErrors;



namespace UnitTests;

public class CreateUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenEmailIsInvalid()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserWriteRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var eventBusMock = new Mock<IEventBus>();

        var handler = new CreateUserCommandHandler(userRepositoryMock.Object, unitOfWorkMock.Object, eventBusMock.Object);

        var command = new CreateUserCommand
        (
            "invalid-email",
            "John",
            "Doe",
            "1234567890",
            "USA",
            "123 Main St",
            "New York",
            "NY",
            "10001"
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("Email format is invalid", result.Error.Description);
        result.Error.Should().Be(EmailErrors.InvalidFormat);
    }

    
    /*
    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenEmailIsNotUnique()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserWriteRepository>();
        userRepositoryMock
            .Setup(repo => repo.IsEmailUniqueAsync(It.IsAny<Email>()))
            .ReturnsAsync(false);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var eventBusMock = new Mock<IEventBus>();

        var handler = new CreateUserCommandHandler(userRepositoryMock.Object, unitOfWorkMock.Object, eventBusMock.Object);

        var command = new CreateUserCommand
        {
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            Country = "USA",
            Street = "123 Main St",
            City = "New York",
            State = "NY",
            ZipCode = "10001"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.EmailNotUnique, result.Error);
    }

    [Fact]
    public async Task Handle_ShouldCreateUser_WhenAllDataIsValid()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserWriteRepository>();
        userRepositoryMock
            .Setup(repo => repo.IsEmailUniqueAsync(It.IsAny<Email>()))
            .ReturnsAsync(true);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var eventBusMock = new Mock<IEventBus>();
        eventBusMock
            .Setup(bus => bus.PublishAsync(It.IsAny<UserCreatedDomainEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new CreateUserCommandHandler(userRepositoryMock.Object, unitOfWorkMock.Object, eventBusMock.Object);

        var command = new CreateUserCommand
        {
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            Country = "USA",
            Street = "123 Main St",
            City = "New York",
            State = "NY",
            ZipCode = "10001"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        userRepositoryMock.Verify(repo => repo.Insert(It.IsAny<User>()), Times.Once);
        unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        eventBusMock.Verify(bus => bus.PublishAsync(It.IsAny<UserCreatedDomainEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }*/
}
