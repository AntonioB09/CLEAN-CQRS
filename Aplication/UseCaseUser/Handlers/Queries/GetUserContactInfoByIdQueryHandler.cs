using Application.Messaging;
using Application.UseCaseUser.IRepositories;
using Application.UseCaseUser.Queries;
using Application.UseCaseUser.ResponseDTos;
using Domain.Shared;
using Domain.User;


namespace Application.UseCaseUser.Handlers.Queries;
public class GetUserContactInfoByIdQueryHandler : IQueryHandler<GetUserContactInfoByIdQuery, UserContactInfoResponse>
{
    private readonly IUserReadRepository _userReadRepository;

    public GetUserContactInfoByIdQueryHandler(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task<Result<UserContactInfoResponse>> Handle(GetUserContactInfoByIdQuery request, CancellationToken cancellationToken)
    {
        UserContactInfoResponse? user = await _userReadRepository.GetContactInfoAsync(request.id, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserContactInfoResponse>(UserErrors.NotFound(new UserId(request.id)));
        }

        return Result.Success(user);
    }
}


