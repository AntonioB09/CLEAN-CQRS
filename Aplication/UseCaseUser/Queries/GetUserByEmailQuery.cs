using Application.Messaging;
using Application.UseCaseUser.ResponseDTos;

namespace Application.UseCaseUser.Queries;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResponse>;
