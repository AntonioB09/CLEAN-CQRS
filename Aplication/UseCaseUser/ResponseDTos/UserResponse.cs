
namespace Application.UseCaseUser.ResponseDTos;

public sealed record UserResponse
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required AddressResponse Address { get; set; }

}


