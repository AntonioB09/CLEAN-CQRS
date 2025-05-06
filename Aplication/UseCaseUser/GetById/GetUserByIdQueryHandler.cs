using Application.Messaging;
using Domain.Errors;
using Domain.Shared;




namespace Application.UseCaseUser.GetById;
public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserReadRepository _userReadRepository;

    public GetUserByIdQueryHandler(IUserReadRepository userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetByIdAsync(request.id);

        return user == null ? Result.Failure<UserResponse>(DomainErrors.UserErrors.NotFound(request.id)) : (Result<UserResponse>)user;
    }
}


