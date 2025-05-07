using Application.Messaging;
using Domain.Errors;
using Domain.Shared;
using Domain.User;
using Domain.User.ValueObjects;
using static Domain.Errors.DomainErrors;




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

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(new UserId(request.id)));
        }

        return user;
    }
}


