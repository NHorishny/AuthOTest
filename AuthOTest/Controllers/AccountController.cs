using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;


public class AccountController : Controller
{
    // Login action to redirect to Auth0
    public async Task Login(string returnUrl = "/")
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri("/Home/Index") // Redirect back after login
            .Build();

        await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);

        //var token = await HttpContext.GetTokenAsync("id_token");
        //Debug.WriteLine(token);
        //Console.WriteLine("THIS IS THE TOKEN", token);

        //// Store the token in an HttpOnly cookie
        //if (token != null)
        //{
        //    HttpContext.Response.Cookies.Append("auth_token", token, new CookieOptions
        //    {
        //        HttpOnly = true,  // Ensures cookie is not accessible via JavaScript
        //        Secure = true,    // Ensures cookie is only sent over HTTPS
        //        SameSite = SameSiteMode.Strict  // Controls the scope of where the cookie is sent
        //    });
        //}
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
