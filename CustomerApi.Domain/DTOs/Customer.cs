using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Domain.DTOs;


public class Customer
{
    [Key]
    public string Id { get; set; }

    public required string Name { get; set; }

    public string Email { get; set; }
}