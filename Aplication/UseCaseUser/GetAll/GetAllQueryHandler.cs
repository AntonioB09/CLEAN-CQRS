

using Application.Messaging;
using Domain.Shared;


namespace Application.UseCaseUser.GetAll
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
            if (users == null || !users.Any())
            {
                return Result.Failure<IReadOnlyList<UserResponse>>(Error.NotFound("Users.NotFound", "No users were found."));
            }

            return users.ToList();
        }
    }
}
