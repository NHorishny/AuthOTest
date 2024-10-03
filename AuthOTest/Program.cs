using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Configure Auth0 Web App Authentication
builder.Services.AddAuth0WebAppAuthentication(options => {
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

// Add Authentication with Cookie
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = Auth0Constants.AuthenticationScheme; // Challenges Auth0
//})
//.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme); // Add the cookie authentication scheme

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // This should be before app.UseAuthorization()
app.UseAuthorization();

// Define routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
