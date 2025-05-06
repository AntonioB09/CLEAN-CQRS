

using Application.Messaging;

namespace Application.UseCaseUser.GetAll;

public sealed record GetAllUserQuery() : IQuery<IReadOnlyList<UserResponse>>
{
    
}

