

using Application.Data;
using Application.Messaging;
using Domain.Errors;
using Domain.Shared;
using Domain.User;
using Domain.User.ValueObjects;





namespace Application.UseCaseUser.Create;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserId>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserId>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        Result<Email> emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<UserId>(emailResult.Error);
        }

        Email email = emailResult.Value;
        if (!await _userRepository.IsEmailUniqueAsync(email))
        {
            return Result.Failure<UserId>(DomainErrors.UserErrors.EmailNotUnique);
        }

        Result<FirstName> firstname = FirstName.Create(command.FirstName);
        if (firstname.IsFailure)
        {
            return Result.Failure<UserId>(firstname.Error);
        }

        Result<LastName> lastname = LastName.Create(command.LastName);              
        if (firstname.IsFailure)
        {
            return Result.Failure<UserId>(firstname.Error);
        }

        Result<PhoneNumber> phonenumber = PhoneNumber.Create(command.PhoneNumber);
        if (phonenumber.IsFailure)
        {
            return Result.Failure<UserId>(phonenumber.Error);
        }
        Result<Address> address = Address.Create(command.Country, command.Street, command.City, command.State, command.ZipCode);
        if (phonenumber.IsFailure)
        {
            return Result.Failure<UserId>(address.Error);
        }

        
        var user = User.Create( firstname.Value, lastname.Value, email, phonenumber.Value, address.Value);

        _userRepository.Insert(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
