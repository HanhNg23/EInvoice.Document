using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace EInvoice.Document.Application.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityUser> GetIdentityAccountAsync(string email);
        Task<string> GetUserAccessTokenAsync(string userId);
        AuthenticationProperties ConfigureExternalLoginPropertiesForRedirect(string loginProvider, string? redirectUrl);
        Task ExternalLoginRetrieveInfo();
    }
}
