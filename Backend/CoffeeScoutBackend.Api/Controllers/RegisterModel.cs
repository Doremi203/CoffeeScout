using System.ComponentModel.DataAnnotations;

namespace CoffeeScoutBackend.Api.Controllers;

public record RegisterModel(
    [Required] 
    [EmailAddress] 
    string Email,
    [Required] 
    string Password
);