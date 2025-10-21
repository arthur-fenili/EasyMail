using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Commom;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; } // Remover nullable para auto-geração
}

