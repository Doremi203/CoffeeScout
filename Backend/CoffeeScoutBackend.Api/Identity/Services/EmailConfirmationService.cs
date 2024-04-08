using System.Text;
using CoffeeScoutBackend.Bll.Interfaces;
using CoffeeScoutBackend.Dal.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace CoffeeScoutBackend.Api.Identity.Services;

public class EmailConfirmationService(
    UserManager<AppUser> userManager,
    IEmailSender<AppUser> emailSender,
    LinkGenerator linkGenerator
) : IEmailConfirmationService
{
    public async Task SendConfirmationEmail(AppUser user)
    {
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        
        var values = new RouteValueDictionary()
        {
            ["userId"] = user.Id,
            ["code"] = code
        };

        //var callbackUrl = LinkGeneratorEndpointNameAddressExtensions.GetUriByName();
    }

    public Task ConfirmEmail(string email, string token)
    {
        throw new NotImplementedException();
    }
}