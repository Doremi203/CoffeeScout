using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface ISuperAdminService
{
    Task AddCafeAdmin(CafeAdminRegistrationData registrationData);
}