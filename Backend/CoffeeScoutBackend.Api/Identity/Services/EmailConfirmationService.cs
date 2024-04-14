using System.Text;
using CoffeeScoutBackend.Bll.Interfaces;
using CoffeeScoutBackend.Dal.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace CoffeeScoutBackend.Api.Identity.Services;

public class EmailConfirmationService(
    UserManager<AppUser> userManager,
    IEmailSender<AppUser> emailSender,
    LinkGenerator linkGenerator,
    IHttpContextAccessor httpContextAccessor
) : IEmailConfirmationService
{
    public async Task SendRegistrationConfirmationEmail(AppUser user)
    {
        if (user.Email is null)
            throw new InvalidOperationException("User email is null");
        
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null)
            throw new InvalidOperationException("Email cannot be sent outside of an HTTP request context");
        
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        
        var values = new RouteValueDictionary()
        {
            ["userId"] = user.Id,
            ["code"] = code
        };
        const string confirmEndpoint = "MapIdentityApi-api/v1/accounts/confirmEmail";
        
        var callbackUrl = linkGenerator.GetUriByName(httpContext,confirmEndpoint, values);
        await emailSender.SendConfirmationLinkAsync(user, user.Email, callbackUrl);
    }
}