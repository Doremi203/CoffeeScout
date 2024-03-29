using CoffeeScoutBackend.Dal.Entities;
using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Bll.Interfaces;

public interface IRoleRegistrationService
{
    Task<AppUser> RegisterUserAsync(AppUser user, string password, Roles role);
}