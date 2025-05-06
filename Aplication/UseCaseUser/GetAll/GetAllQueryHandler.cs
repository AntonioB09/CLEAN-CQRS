

using Application.Messaging;
using Application.UseCaseUser;
using Domain.Shared;
using Domain.User;
using MassTransit;
using MediatR;
using System.Threading;
using static Domain.Errors.DomainErrors;

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

            return users.ToList();
        }
    }
}
