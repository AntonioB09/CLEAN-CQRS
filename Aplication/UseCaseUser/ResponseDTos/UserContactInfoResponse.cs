
namespace Application.UseCaseUser.ResponseDTos;

public sealed record UserContactInfoResponse
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required AddressResponse Address { get; set; }

}
