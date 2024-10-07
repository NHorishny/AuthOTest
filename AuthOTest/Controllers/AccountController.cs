using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


public class AccountController : Controller
{
    // Login action to redirect to Auth0
    public async Task Login(string returnUrl = "/")
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri("/Home/Index") // Redirect back after login
            .Build();


        await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);

    }

    [Authorize]
    public IActionResult Profile()
    {
        return View(new
        {
            Name = User?.Identity?.Name,
            EmailAddress = User?.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
            ProfileImage = User?.Claims
            .FirstOrDefault(c => c.Type == "picture")?.Value
        });
    }


    // Logout action to clear authentication
    [Authorize]
    public async Task Logout()
    {
        var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            .Build();

        // Logout from Auth0
        await HttpContext.SignOutAsync(
          Auth0Constants.AuthenticationScheme,
          authenticationProperties
        );
        // Logout from the application
        await HttpContext.SignOutAsync(
          CookieAuthenticationDefaults.AuthenticationScheme
        );
    }

}
