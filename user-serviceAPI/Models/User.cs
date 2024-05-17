using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace userServiceAPI.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? _id { get; set; }

    public int UserID { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }

    public string Email { get; set; }
    public string Address { get; set; }
}