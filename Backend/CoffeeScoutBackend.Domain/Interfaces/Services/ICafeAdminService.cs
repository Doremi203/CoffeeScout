using CoffeeScoutBackend.Domain.Models;
using CoffeeScoutBackend.Domain.ServiceModels;

namespace CoffeeScoutBackend.Domain.Interfaces.Services;

public interface ICafeAdminService
{
    Task AddCafeAdmin(CafeAdminRegistrationData registrationData);
}