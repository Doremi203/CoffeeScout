using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces;

public interface ISuperAdminService
{
    Task AddCafeAdminAsync(CafeAdminRegistrationData registrationData);
}