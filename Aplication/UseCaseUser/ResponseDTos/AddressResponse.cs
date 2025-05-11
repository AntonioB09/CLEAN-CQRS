
namespace Application.UseCaseUser.ResponseDTos;

public sealed record AddressResponse
{
    public required string Country { get; set; }
    public required string State { get; set; }
    public required string City { get; set; }
    public required string Street { get; set; }
    public required string ZipCode { get; set; }


}