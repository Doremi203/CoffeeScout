using Microsoft.AspNetCore.Identity;

namespace CoffeeScoutBackend.Dal.Entities;

public class AppUser : IdentityUser
{
    public CustomerEntity? Customer { get; set; }
}