using Application.Messaging;
using Application.UseCaseUser.ResponseDTos;



namespace Application.UseCaseUser.Queries;

public sealed record GetUserByIdQuery(Guid id): IQuery<UserResponse>
{

}
