using Application.Messaging;
using Domain.User;

namespace Application.UseCaseUser.Commands;

public sealed record CreateUserCommand( 
    string FirstName, 
    string LastName, 
    string Email, 
    string PhoneNumber, 
    string Country, 
    string Street, 
    string City, 
    string State, 
    string ZipCode)
    : ICommand<UserId>;


