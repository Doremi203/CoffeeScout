using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface ICafeAdminService
{
    Task AddCafeAdmin(CafeAdminRegistrationData registrationData);
}