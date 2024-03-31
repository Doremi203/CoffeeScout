using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface ISuperAdminService
{
    Task AddCafeAdminAsync(CafeAdminRegistrationData registrationData);
}