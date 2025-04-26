using Application.Messaging;

namespace Application.UseCaseUser.GetByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResponse>;
