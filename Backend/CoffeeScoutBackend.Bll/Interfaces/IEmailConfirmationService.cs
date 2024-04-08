using CoffeeScoutBackend.Dal.Entities;

namespace CoffeeScoutBackend.Bll.Interfaces;

public interface IEmailConfirmationService
{
    Task SendConfirmationEmail(AppUser user);
    Task ConfirmEmail(string email, string token);
}