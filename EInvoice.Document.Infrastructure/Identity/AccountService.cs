using Duende.IdentityServer.Extensions;
using EInvoice.Document.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace EInvoice.Document.Infrastructure.Identity
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager; 
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public AuthenticationProperties ConfigureExternalLoginPropertiesForRedirect(string loginProvider, string? redirectUrl)
        {
            return  _signInManager.ConfigureExternalAuthenticationProperties(loginProvider, redirectUrl);
        }

        public async Task ExternalLoginRetrieveInfo()
        {
            ExternalLoginInfo externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                throw new ArgumentNullException("External Login Info is null !");
            }
            Console.WriteLine(
              "\n==== External Login Information ==== "
              + "\n- ProviderKey: " + externalLoginInfo.ProviderKey + "\n"
              + "\n- ProviderDisplayName: " + externalLoginInfo.ProviderDisplayName + "\n"
              + "\n- LoginProvider: " + externalLoginInfo.LoginProvider + "\n"
              + "\n- Principal.Identity.Name: " + externalLoginInfo.Principal.Identity.Name + "\n"
              + "\n- Principal.Identity.AuthenticationType: " + externalLoginInfo.Principal.Identity.AuthenticationType + "\n"
              + "\n- Identity.IsAuthenticated: " + externalLoginInfo.Principal.Identity.IsAuthenticated + "\n"
              + "\n- AuthenticationProperties.IsPersistent: " + externalLoginInfo.AuthenticationProperties.IsPersistent + "\n"
              + "\n- AuthenticationProperties.AllowRefresh: " + externalLoginInfo.AuthenticationProperties.AllowRefresh + "\n"
              + "\n- AuthenticationProperties.ExpiresUtc: " + externalLoginInfo.AuthenticationProperties.ExpiresUtc + "\n"
              + "\n- AuthenticationProperties.IssuedUtc: " + externalLoginInfo.AuthenticationProperties.IssuedUtc + "\n"
              + "\n- AuthenticationProperties.RedirectUri: " + externalLoginInfo.AuthenticationProperties.RedirectUri + "\n"
              );

            Console.WriteLine("\n==== External Login Info Principal Claims \n");
            foreach (var claim in externalLoginInfo.Principal.Claims)
            {
                Console.WriteLine("- Claim Type: " + claim.Type + " - Claim Value: " + claim.Value);
            }
            Console.WriteLine("\n==== External Login Info Authentication Tokens \n");
            foreach (var token in externalLoginInfo.AuthenticationTokens)
            {
                Console.WriteLine("- Token Name: " + token.Name + " - Token Value: " + token.Value);
            }
            Console.WriteLine("\n==== External Login Info Items \n");
            foreach (var item in externalLoginInfo.AuthenticationProperties.Items)
            {
                Console.WriteLine("- Item: " + item.Key + " - " + item.Value);
            }
            Console.WriteLine("\n==== External Login Info Client List \n");
            foreach (var client in externalLoginInfo.AuthenticationProperties.GetClientList())
            {
                Console.WriteLine("- Client: " + client);
            }
            Console.WriteLine("\n==== External Login Info Token List \n");
            foreach (var token in externalLoginInfo.AuthenticationProperties.GetTokens())
            {
                Console.WriteLine("- Token: " + token.Name + " - " + token.Value);
            }

            var accessToken = externalLoginInfo.AuthenticationTokens.FirstOrDefault(x => x.Name.Equals("access_token"));

            var info = new UserLoginInfo(externalLoginInfo.LoginProvider, externalLoginInfo.ProviderKey, externalLoginInfo.ProviderDisplayName);
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user != null)
            {
                if (!await _signInManager.CanSignInAsync(user))
                {
                    throw new ApplicationException("User is not allowed to sign in !");
                }
                if (await _userManager.IsLockedOutAsync(user))
                {
                    throw new ApplicationException("User is locked out !");
                }
            }
            if (user == null)
            {
                var email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);
                var userName = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Name);
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new IdentityUser
                    {
                        Email = email,
                        UserName = email,
                        EmailConfirmed = true,
                        LockoutEnabled = true

                    };
                    var result = await _userManager.CreateAsync(user);
                    Console.WriteLine("==== User Created Result: " + result);
                    //await _userManager.AddToRoleAsync(user, "User");
                    await _userManager.AddLoginAsync(user, info);
                }
                else //when user not found login with specific login provider but found in existed accounts
                {
                    await _userManager.AddLoginAsync(user, info);
                }
            }
            _userManager.SetAuthenticationTokenAsync(user, externalLoginInfo.LoginProvider, "access_token", accessToken.Value);
            if (user == null) //check current user is null
            {
                throw new ApplicationException("Invalid External Authentication");
            }
            Console.WriteLine("==== User Name: " + user.UserName);
        }

        public async Task<IdentityUser> GetIdentityAccountAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<string> GetUserAccessTokenAsync(string userId)
        {
            Console.WriteLine("==== Current User : " + " User Id: " + userId);
            IdentityUser? user = await _userManager.FindByEmailAsync(userId);
            string? loginProvider = GoogleDefaults.DisplayName;
            Console.WriteLine("=============> LoginProvider: " + loginProvider + "==================================================");
            string? userAccessToken = await _userManager.GetAuthenticationTokenAsync(user, loginProvider, "access_token");
            Console.WriteLine("==== Current User : " + " User Id: " + userId + " LoginProvider: " + loginProvider + " AccessToken: " + userAccessToken);
            return userAccessToken;
        }
    }
}
