using Application.Messaging;
using Application.UseCaseUser.ResponseDTos;



namespace Application.UseCaseUser.Queries;

public sealed record GetUserContactInfoByIdQuery(Guid id) : IQuery<UserContactInfoResponse>
{

}
