

using Application.Messaging;
using Domain.User;


namespace Application.UseCaseUser.GetById;

public sealed record GetUserByIdQuery(UserId id) : IQuery<UserResponse>
{

}
