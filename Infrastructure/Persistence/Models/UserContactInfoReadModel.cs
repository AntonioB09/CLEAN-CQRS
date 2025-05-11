
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Infrastructure.Persistence.Models;

public sealed record UserContactInfoReadModel
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required AddressReadModel Address { get; set; }

}
