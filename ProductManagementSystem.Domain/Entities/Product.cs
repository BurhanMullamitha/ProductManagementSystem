using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Domain.Entities;

public class Product
{
    [BsonId]
    [Required]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public decimal Price { get; set; } 
    [Required]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CategoryId { get; set; }
    public string? Color { get; set; }
}
