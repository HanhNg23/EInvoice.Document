using EInvoice.Document.Application.Common.Interfaces;
using EInvoice.Document.Application.Interfaces;
using EInvoice.Document.Infrastructure.ApiClients.Common;
using EInvoice.Document.Infrastructure.ApiClients.EInvoiceDocumentClient;
using EInvoice.Document.Infrastructure.Data;
using EInvoice.Document.Infrastructure.Identity;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;



namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //set up variables
        var connectionString = configuration.GetConnectionString("EInvoiceDocumentAPIContextConnection") ?? throw new InvalidOperationException("Connection string 'EInvoiceDocumentAPIContextConnection' not found.");
        var googleClientId = configuration["Authentication:Google:ClientId"] ?? throw new ArgumentNullException("Google Client Id Not Found !");
        var googleClientSecret = configuration["Authentication:Google:ClientSecret"] ?? throw new ArgumentNullException("Google Client Secret Not Found !");
        Console.WriteLine("==== GOOGLE CLIENT ID " + googleClientId + " GOOGLE CLIENT SECRET " + googleClientSecret + " DB CONNECTION STRING" + connectionString);


        //set up DB services
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        //set up Identity services
        services.AddIdentityCore<IdentityUser>()
                //.AddRoles<IdentityRole>() //no role not setup add in here
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<IdentityUser>>()
                .AddApiEndpoints();
        Console.WriteLine("==== IDENTITY SERVICES ADDED ====");

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = GoogleDefaults.AuthenticationScheme;
        })
            // position for application scheme above external scheme because the configuration
            // of the cookie handler for the application scheme will be used for the external scheme
            .AddCookie(IdentityConstants.ApplicationScheme, options => // Assign the Cookie handler for IdentityCOnstants.ApplicationScheme with additional main config
            {
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.Cookie.Name = "EInvoiceDocumentAPI";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.IsEssential = true;
            })
            .AddCookie(IdentityConstants.ExternalScheme, options => //Assign the Cookie handler for IdentityCOnstants.ExternalScheme
            {
                options.Cookie.Name = "EInvoiceDocumentAPI";
            }) 
             //.AddCookie(IdentityConstants.ExternalScheme) //Assign the Cookie handler for IdentityCOnstants.ExternalScheme
            .AddGoogle(options =>
            {
                options.ClientId = googleClientId;
                options.ClientSecret = googleClientSecret;
                // to said when google sign in success, it will genereate the cookie for the user
                // by using the IdentityConstants.ExternalScheme below. Next request, server will check the cookie for authentication
                options.SignInScheme = IdentityConstants.ExternalScheme; 
                options.SaveTokens = true;
                //add another scope
                options.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");
                options.CallbackPath = "/api/account/signin-google";
                //options.ClaimActions.MapAll();

                options.Events.OnCreatingTicket = (context) =>
                {
                    Console.WriteLine("\n==== EVENT DONE GOOGLE REDIRECTION - GOOGLE ON CREATING TICKET ====");
                    Console.WriteLine("\n- Token Google Endpoint: " + options.TokenEndpoint //The google handler will automatically request the token from the token endpoint by using the code, state, clientId, ClientSecret, RedirectUri
                                    + "\n- Authorization Endpoint: " + options.AuthorizationEndpoint
                                    + "\n- UserInformation Endpoint: " + options.UserInformationEndpoint);
                    Console.WriteLine("\n- ClaimActions");
                    foreach (var s in options.ClaimActions)
                    {
                        Console.WriteLine(" + Claim action type: " + s.ClaimType);
                    }
                    Console.WriteLine("\n- AccessToken: " + context.AccessToken);
                    Console.WriteLine("\n- RefreshToken: " + context.RefreshToken);
                    Console.WriteLine("\n- TokenResponse: " + context.TokenResponse + " Context Token Type: " + context.TokenType);
                    Console.WriteLine("\n- Context User: " + context.User.GetRawText());
                    Console.WriteLine("\n- Request Query Values: ");
                    foreach (var s in context.HttpContext.Request.Query)
                    {
                        Console.WriteLine(" + Query Key: " + s.Key + " - Value: " + s.Value);
                    }
                    Console.WriteLine("\n- User Claims");

                    foreach (var s in context.User.ToClaims())
                    {
                        Console.WriteLine(" + Claim type: " + s.Type + " - Value: " + s.Value);
                    }
                    List<AuthenticationToken> tokes = context.Properties.GetTokens().ToList();
                    Console.WriteLine("\n- Tokens");
                    foreach (var s in tokes)
                    {
                        Console.WriteLine(" + Token Name: " + s.Name + " - Value: " + s.Value);
                    }
                    tokes.Add(new AuthenticationToken() { Name = "TicketCreated", Value = DateTime.UtcNow.ToString()});
                    context.Properties.StoreTokens(tokes);
                    
                    return Task.CompletedTask;
                };
            });
        Console.WriteLine("==== AUTHENTICATION SERVICES ADDED ====");
        // set up authorization service
        services.AddAuthorization(options =>
        {
            // the fallback authorization policy is applied to all request that don't have any
            // authorization policy --> requires all users to be authenticated
            // excapt for Razor Pages, which have their own default policy, controllers, or actions
            // methods with an authorization attribute like [AllowAnonymous] or [Authorize(PolicyName="MyPolicy")]
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });
        Console.WriteLine("==== AUTHORIZATION SERVICES ADDED ====");

        // Decale dependency injection for IOC
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddHttpClient<IHttpClientService, HttpClientService>();
        services.AddTransient<IHttpClientService, HttpClientService>();
        services.AddTransient<IEInvoiceDocumentClientService, EInvoiceDocumentClientService>();
        services.AddTransient<IAccountService, AccountService>();

        return services;
    }
}
