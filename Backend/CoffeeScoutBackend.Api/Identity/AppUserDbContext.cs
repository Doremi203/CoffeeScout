using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeScoutBackend.Api.Identity;

public class AppUserDbContext(
    DbContextOptions<AppUserDbContext> options
) : IdentityDbContext<AppUser>(options);