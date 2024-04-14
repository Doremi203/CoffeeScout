using CoffeeScoutBackend.Dal.Entities;

namespace CoffeeScoutBackend.Bll.Interfaces;

public interface IEmailConfirmationService
{
    Task SendRegistrationConfirmationEmail(AppUser user);
}