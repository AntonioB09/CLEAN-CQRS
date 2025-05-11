using Application.Messaging;
using Application.UseCaseUser.IRepositories;
using Application.UseCaseUser.Queries;
using Application.UseCaseUser.ResponseDTos;
using Domain.Shared;


namespace Application.UseCaseUser.Handlers.Queries
{
    public class GetAllQueryHandler : IQueryHandler<GetAllUserQuery, IReadOnlyList<UserResponse>>
    {
        private readonly IUserReadRepository _userReadRepository;

        public GetAllQueryHandler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task<Result<IReadOnlyList<UserResponse>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<UserResponse> users = await _userReadRepository.GetAllAsync();

            // Check if the result is null or empty
            return users == null || !users.Any()
                ? Result.Failure<IReadOnlyList<UserResponse>>(Error.NotFound("Users.NotFound", "No users were found."))
                : Result.Success <IReadOnlyList<UserResponse>>(users.ToList());
        }
    }
}
