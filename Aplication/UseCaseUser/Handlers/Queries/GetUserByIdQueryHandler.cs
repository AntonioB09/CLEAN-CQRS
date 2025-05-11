using Application.Messaging;
using Application.UseCaseUser.IRepositories;
using Application.UseCaseUser.Queries;
using Application.UseCaseUser.ResponseDTos;
using Domain.Shared;
using Domain.User;





namespace Application.UseCaseUser.Handlers.Queries;
public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserReadRepository _userReadRepository;

    public GetUserByIdQueryHandler(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        UserResponse? user = await _userReadRepository.GetByIdAsync(request.id, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(new UserId(request.id)));
        }

        return Result.Success(user);
    }
}


