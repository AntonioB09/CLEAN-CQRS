using Application.Data;
using Application.Messaging;
using Application.UseCaseUser.Commands;
using Application.UseCaseUser.IRepositories;
using Domain.Shared;
using Domain.User;
using Domain.User.ValueObjects;
using Domain.UserEvent;


namespace Application.UseCaseUser.Handlers.Commands;

internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, User>
{
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventBus _eventBus;

    public UpdateUserCommandHandler(IUserWriteRepository userRepository, IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _userWriteRepository = userRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task<Result<User>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {

        // Retrieve the existing user
        User? user = await _userWriteRepository.GetByIdAsync(command.Id, cancellationToken);

        if (user == null) // Ensure user is not null
        {
            return Result.Failure<User>(UserErrors.NotFound(command.Id));
        }

        // Validate and create value objects
        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<User>(emailResult.Error);
        }

        Email email = emailResult.Value;
        if (email != user.Email)
        {
            if (!await _userWriteRepository.IsEmailUniqueAsync(email))
            {
                return Result.Failure<User>(UserErrors.EmailNotUnique);
            }
        }

        var firstnameResult = FirstName.Create(command.FirstName);
        if (firstnameResult.IsFailure)
        {
            return Result.Failure<User>(firstnameResult.Error);
        }

        var lastnameResult = LastName.Create(command.LastName);
        if (lastnameResult.IsFailure)
        {
            return Result.Failure<User>(lastnameResult.Error);
        }

        var phoneNumberResult = PhoneNumber.Create(command.PhoneNumber);
        if (phoneNumberResult.IsFailure)
        {
            return Result.Failure<User>(phoneNumberResult.Error);
        }

        var addressResult = Address.Create(command.Country, command.Street, command.City, 
        command.State, command.ZipCode);

        if (addressResult.IsFailure)
        {
            return Result.Failure<User>(addressResult.Error);
        }

        // Update the user
        user.UpdateUser(
            command.Id,
            firstnameResult.Value,
            lastnameResult.Value,
            emailResult.Value,
            phoneNumberResult.Value,
            addressResult.Value
        );

        // Save changes
        _userWriteRepository.UpdateUser(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //Publish the Event
        await _eventBus.PublishAsync(new UserUpdatedDomainEvent(
                                                          user.Id,
                                                          user.FirstName,
                                                          user.LastName,
                                                          user.Email,
                                                          user.PhoneNumber,
                                                          user.Address),
                                                          cancellationToken);

        return user;
    }
}
