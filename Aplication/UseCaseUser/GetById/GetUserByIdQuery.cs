

using Application.Messaging;



namespace Application.UseCaseUser.GetById;

public sealed record GetUserByIdQuery(Guid id): IQuery<UserResponse>
{

}
