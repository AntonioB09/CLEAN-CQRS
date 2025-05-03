using Domain.User;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Infrastructure.Data.Models;

public sealed class UserReadModel
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }

    public required string FirstName { get; set; }

    


}
