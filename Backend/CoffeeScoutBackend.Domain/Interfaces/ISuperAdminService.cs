using CoffeeScoutBackend.Domain.Models;

namespace CoffeeScoutBackend.Domain.Interfaces;

public interface ISuperAdminService
{
    Task AddBeverageTypeAsync(BeverageType beverageType);
    Task AddCafeAdminAsync(string requestEmail, string requestPassword);
}