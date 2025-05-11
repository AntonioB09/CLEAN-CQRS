using Application.Messaging;
using Domain.User;


namespace Application.UseCaseUser.Commands;
public sealed record UpdateUserCommand(
    UserId Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Country,
    string State,
    string City,
    string Street,
    string ZipCode)
    : ICommand<User>;
public record UpdateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Country,
    string State,
    string City,
    string Street,
    string ZipCode);