using Domain.User;


namespace Application.UseCaseUser
{
    public interface IUserReadRepository
    {   
        Task<UserResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<UserResponse>> GetAllAsync(CancellationToken cancellationToken = default);

        //Task<UserResponse> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    }
}
