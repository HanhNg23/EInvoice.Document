
using EInvoice.Document.Infrastructure;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EInvoice.Document.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // The initialized WebApplicationBuilder (builder) provides default
            // configuration and calls AddUserSecrets when the EnvironmentName
            // is Development:
            var builder = WebApplication.CreateBuilder(args); // initialize a new instance of WebApplicationBuilder with preconfigured defaults
            
            var googleClientId = builder.Configuration["Authentication:Google:ClientId"] ?? throw new ArgumentNullException("Google Client Id Not Found !");
            var googleClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? throw new ArgumentNullException("Google Client Secret Not Found !");




            // Add services to the container.  
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddWebServices();

          builder.Services.AddAuthentication(options =>
          {
              options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
              options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
              options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
              options.DefaultSignInScheme = GoogleDefaults.AuthenticationScheme;
          })
         .AddGoogle(options =>
         {

             options.ClientId = googleClientId;
             options.ClientSecret = googleClientSecret;
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
                 Console.WriteLine("\n- User Claims" + context.User.GetRawText);

                 //foreach (var s in )
                 //{
                 //    Console.WriteLine("Claim type: " + s.Type + " - Value: " + s.Value);
                 //}

                 return Task.CompletedTask;
             };
             options.AccessDeniedPath = "/api/account/access-denied";
         });
            Console.WriteLine("==== AUTHENTICATION SERVICES ADDED ====");
            // set up authorization service
            //builder.Services.AddAuthorization(options =>
            //{
            //    // the fallback authorization policy is applied to all request that don't have any
            //    // authorization policy --> requires all users to be authenticated
            //    // excapt for Razor Pages, which have their own default policy, controllers, or actions
            //    // methods with an authorization attribute like [AllowAnonymous] or [Authorize(PolicyName="MyPolicy")]
            //    options.FallbackPolicy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

    }
}
